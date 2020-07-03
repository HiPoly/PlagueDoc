using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private Rigidbody rb;

    //Manual Movement System
    [Range(-15f, 15f)]
    public float moveleftright;
    [Range(-15f, 15f)]
    public float movefowardback;
    [Range(0f, 1f)]
    public float tiltinterval;
    [Range(0f, 20f)]
    public float tiltlimit;
    private float timer;

    public State state = State.WALKINGPAST;
    private NPCManager NPCM;
    private Actions Player;
    public float wanttobuysomething;
    int placeOnList;

    public float fleeingspeed;
    public Animator animator;
    public SpriteRenderer Speechbubble;
    public SpriteRenderer ExclamationMark;
    public enum State
    {
        WALKINGPAST,
        QUEUING,
        FLEEING,
        PANICKING,
        BEINGSERVED,
        LEAVING
    }
    float buyrange;
    public float hatrandomizer;
    public bool VikingMode;
    public SpriteRenderer Viking;
    public SpriteRenderer Wizard;
    public SpriteRenderer Paper;
    public SpriteRenderer Fireman;
    public SpriteRenderer Cowboy;
    void OnEnable()
    {
        buyrange = Random.Range(9f, -5f);
        state = State.WALKINGPAST;
        NPCM = GameObject.FindObjectOfType<NPCManager>();
        Player = GameObject.FindObjectOfType<Actions>();
        rb = GetComponent<Rigidbody>();
        wanttobuysomething = Random.Range(0f,10f);
        haveiqueuedalready = true;
        hatrandomizer = Random.Range(0, 15);

        if (hatrandomizer == 10)
        {
            Cowboy.enabled = true;
        }

        if (hatrandomizer == 11) 
        {
            Viking.enabled = true;
            VikingMode = true;
        }

        if (hatrandomizer == 12) 
        {
            Wizard.enabled = true;
        }

        if (hatrandomizer == 13)
        {
            Paper.enabled = true;
        }

        if (hatrandomizer == 14)
        {
            Fireman.enabled = true;
        }
    }
    public bool queue;
    public bool haveiqueuedalready;
    public float chancetobuy;
    void Update()
    {
        // Tilt Timer resets after two movements
        timer += Time.deltaTime;
        if (timer > (tiltinterval * 2))
        {
            timer = 0f;
            ExclamationMark.enabled = false;
        }

        if ((transform.position.x < buyrange) && (wanttobuysomething > chancetobuy) && (haveiqueuedalready == true) & (state == State.WALKINGPAST))
        {
            ExclamationMark.enabled = true;
            queue = true;
            state = State.QUEUING;
            haveiqueuedalready = false;

            movefowardback = 0f;
            moveleftright = 0f;

        }
        
    }

    //List of other objects in trigger coliders radius
    public List<GameObject> PanicCausersInRange = new List<GameObject>();
    public Collider other;


    void OnTriggerStay(Collider other)
    {
        if (state != State.PANICKING)
        {
            if (other.tag == ("DANGEROUSNPC"))
            {
                PanicCausersInRange.Add(other.attachedRigidbody.gameObject);
                state = State.FLEEING;
            }
        }
    }

    private State previousState;
    void OnTriggerExit(Collider other)
    {
        if (state == State.FLEEING)
        {
            if (other.attachedRigidbody == null)
            {
                state = previousState;
                rb.velocity = new Vector3(0f, 0f, 0f);
                return;
            }
            PanicCausersInRange.Remove(other.attachedRigidbody.gameObject);
            if (PanicCausersInRange.Count == 0)
            {
                state = previousState;
                rb.velocity = new Vector3(0f,0f,0f);
            }
        }
        if (other.tag == "ground")
        {
            touchingGround = false;
        }
    }
    private void OnTriggerEnter(Collider ground)
    {
        if (ground.tag == "ground")
        {
            touchingGround = true;
        }
    }

    private bool touchingGround;

    void FixedUpdate()
    {
        //Manual Control
        rb.AddForce(transform.right * moveleftright);
        rb.AddForce(transform.forward * movefowardback);

        //Tilt Physics
        Quaternion rotation = (transform.rotation);


            if (touchingGround == true)
            {
                if (timer < tiltinterval)
                {
                    transform.rotation = Quaternion.Euler(rotation[0], rotation[1] + 0f, rotation[2] + tiltlimit);
                }

                if (timer > tiltinterval)
                {
                    transform.rotation = Quaternion.Euler(rotation[0], rotation[1] + 0f, rotation[2] - tiltlimit);
                }
            }

        //AI States
        switch (state)
        {
            case State.WALKINGPAST:
                Walkingpast();
                gameObject.tag = "NPC";
                previousState = state;
                animator.ResetTrigger("Panic");
                break;
            case State.QUEUING:
                Queuing();
                gameObject.tag = "NPC";
                previousState = state;
                animator.ResetTrigger("Panic");
                break;
            case State.FLEEING:
                Fleeing();
                gameObject.tag = "NPC";
                animator.ResetTrigger("Panic");
                break;
            case State.PANICKING:
                Panicking();
                gameObject.tag = "DANGEROUSNPC";
                animator.SetTrigger("Panic");
                break;
            case State.BEINGSERVED:
                Beingserved();
                gameObject.tag = "NPC";
                previousState = state;
                animator.ResetTrigger("Panic");
                break;
            case State.LEAVING:
                Leaving();
                gameObject.tag = "NPC";
                previousState = state;
                animator.ResetTrigger("Panic");
                break;

        }
    }

    private float timer2;
    private bool flipflop;
    void Walkingpast()
    {
        if (NPCM.howfullisqueue > 12)
        {
            chancetobuy = 10f;
        }

        timer2 += Time.deltaTime;

        if (timer2 > 4f)
        {
            timer2 = 0f;
        }
        moveleftright = -2.5f;
        tiltlimit = 6f;
        if (timer2 < 2f)
        {
            if (flipflop == true)
            {
                movefowardback = Random.Range(0f, -4f);
            }
            flipflop = false;
        }

        moveleftright = -2.5f;
        if (timer2 > 2f)
        {
            if (flipflop == false)
            {
                movefowardback = Random.Range(0f, 4f);
                flipflop = true;
            }
        }
    }

    void Leaving()
    {
        tiltlimit = 6f;
        moveleftright = -4f;
        if (transform.position.z > -6)
        {
            movefowardback = -12f;
        }
        if (transform.position.z < -6)
        {
            movefowardback = 0f;
            state = State.WALKINGPAST;
        }
    }

    private GameObject queueposition;
    void Queuing()
    {
        tiltinterval = 0.52f;
        tiltlimit = 1.8f;

        if (queue == true)
        {
            NPCM.AddNPC(gameObject);
            queue = false;
        }
        placeOnList = NPCM.CheckPlace(gameObject);

        if (placeOnList == 0)
        {
            state = State.BEINGSERVED;
        }

        if (placeOnList == 1)
        {
            queueposition = GameObject.Find("Q1");
        }

        if (placeOnList == 2)
        {
            queueposition = GameObject.Find("Q2");
        }

        if (placeOnList == 3)
        {
            queueposition = GameObject.Find("Q3");
        }

        if (placeOnList == 4)
        {
            queueposition = GameObject.Find("Q4");
        }

        if (placeOnList == 5)
        {
            queueposition = GameObject.Find("Q5");
        }

        if (placeOnList == 6)
        {
            queueposition = GameObject.Find("Q6");
        }

        if (placeOnList == 7)
        {
            queueposition = GameObject.Find("Q7");
        }

        if (placeOnList == 8)
        {
            queueposition = GameObject.Find("Q8");
        }

        if (placeOnList == 9)
        {
            queueposition = GameObject.Find("Q9");
        }

        if (placeOnList == 10)
        {
            queueposition = GameObject.Find("Q10");
        }

        if (placeOnList == 11)
        {
            queueposition = GameObject.Find("Q11");
        }

        if (placeOnList == 12)
        {
            queueposition = GameObject.Find("Q12");
        }

        if (placeOnList > 12)
        {
            queue = false;
            NPCM.RemoveNPC(gameObject);
            state = State.LEAVING;
        }

        if (queueposition == null)
        {
            return;
        }

        if (transform.position.x > queueposition.transform.position.x)
        {
            moveleftright = -queueSpeed;
        }

        if (transform.position.x < queueposition.transform.position.x)
        {
            moveleftright = queueSpeed;
        }

        if (transform.position.z < queueposition.transform.position.z)
        {
            movefowardback = queueSpeed;
        }

        if (transform.position.z > queueposition.transform.position.z)
        {
            movefowardback = -queueSpeed;
        }
    }

    void Fleeing()
    {
        //personal bubble
        foreach (GameObject AI in PanicCausersInRange)
        {
            if (AI != this)
            {
                rb.AddForce((AI.gameObject.transform.position - transform.position).normalized * -fleeingspeed, ForceMode.Impulse);
            }
        }
    }

    private float panicktimer;
    private bool panickbool;
    private bool startpanick;
    public float panickedrunspeed;
    void Panicking()
    {
        tiltlimit = 12;
        tiltinterval = 0.1f;

        if (startpanick == false)
        {
            panicktimer = 6f;
            startpanick = true;
        }

        panicktimer += Time.deltaTime;
        if (panicktimer > 2f)
        {
            panicktimer = 0f;
            panickbool = true;
        }
        
       

        if  (panickbool == true)
        {
            float direction = Random.Range(0f, 10f);
            float direction2 = Random.Range(0f, 10f);
            
            if(direction < 5f)
            {
                movefowardback = panickedrunspeed;
            }

            if (direction > 5f)
            {
                movefowardback = -panickedrunspeed;
            }

            if (direction2 < 5f)
            {
                moveleftright = panickedrunspeed;
            }

            if (direction2 > 5f)
            {
                moveleftright = -panickedrunspeed;
            }
            panickbool = false;
        }
    }

    private GameObject serveposition;
    public float queueSpeed;
    public bool finishedserving;
    public int[] order;
    public int ordersize;
    public bool ordermade;
    public float dist;
    public float fails;
    public Renderer colourchange;
    public float littletimer;
    void Beingserved()
    {
        Speechbubble.enabled = true;
        littletimer += Time.deltaTime;
        if (littletimer > 2f)
        {
            Speechbubble.enabled = false;
        }
        tiltlimit = 3f;
        tiltinterval = 0.4f;
        serveposition = GameObject.Find("ServePos");
        if (transform.position.x > serveposition.transform.position.x)
        {
            moveleftright = -queueSpeed;
        }

        if (transform.position.x < serveposition.transform.position.x)
        {
            moveleftright = queueSpeed;
        }

        if (transform.position.z < serveposition.transform.position.z)
        {
            movefowardback = queueSpeed;
        }

        if (transform.position.z > serveposition.transform.position.z)
        {
            movefowardback = -queueSpeed;
        }

        if (finishedserving == true)
        {
            NPCM.RemoveNPC(gameObject);
            state = State.LEAVING;
        }
        dist = Vector3.Distance(serveposition.transform.position, transform.position);

        if ((dist < 1.5f) && (ordermade == false))
        {
            order = new int[ordersize];

            float randomizer = Random.Range(0f, 3f);
            if ((randomizer > 0f) && (randomizer <= 1f))
            {
                order[0] = 1;
            }
            if ((randomizer > 1f) && (randomizer <= 2f))
            {
                order[0] = 2;
            }
            if ((randomizer > 2f) && (randomizer <= 3f))
            {
                order[0] = 3;
            }

            randomizer = Random.Range(0f, 3f);
            if ((randomizer > 0f) && (randomizer <= 1f))
            {
                order[1] = 1;
            }
            if ((randomizer > 1f) && (randomizer <= 2f))
            {
                order[1] = 2;
            }
            if ((randomizer > 2f) && (randomizer <= 3f))
            {
                order[1] = 3;
            }
            randomizer = Random.Range(0f, 3f);
            if ((randomizer > 0f) && (randomizer <= 1f))
            {
                order[2] = 1;
            }
            if ((randomizer > 1f) && (randomizer <= 2f))
            {
                order[2] = 2;
            }
            if ((randomizer > 2f) && (randomizer <= 3f))
            {
                order[2] = 3;
            }
            ordermade = true;
            Player.RecieveOrder(order[0],order[1],order[2]);
        }

        fails = Player.drugfails;
        if(fails == 1)
        {
            colourchange = GetComponent<Renderer>();
            colourchange.material.SetColor("_Color",Color.green);
            finishedserving = true;
            Player.drugfails = 0;
        }
        if (fails > 1)
        {
            colourchange = GetComponent<Renderer>();
            colourchange.material.SetColor("_Color", Color.red);
            finishedserving = true;
            Player.drugfails = 0;
        }
        if (fails == 0f)
        {
            finishedserving = false;
            Player.drugfails = 0;
        }
    }

    public void RecieveDrug(int drugfails)
    {
        if (drugfails > 0)
        {
            finishedserving = true;
        }

        if (drugfails == 0)
        {
            finishedserving = true;
        }
    }
}