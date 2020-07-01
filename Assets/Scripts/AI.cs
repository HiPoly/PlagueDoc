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
    public enum State
    {
        WALKINGPAST,
        QUEUING,
        FLEEING,
        PANICKING
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Tilt Timer resets after two movements
        timer += Time.deltaTime;
        if (timer > (tiltinterval * 2))
        {
            timer = 0f;
        }
    }

    //List of other objects in trigger coliders radius
    List<GameObject> NPCsInRange = new List<GameObject>();
    public Collider other;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
            NPCsInRange.Add(other.attachedRigidbody.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
            NPCsInRange.Remove(other.attachedRigidbody.gameObject);
    }

    void FixedUpdate()
    {
        //Manual Control
        rb.AddForce(-transform.forward * moveleftright);
        rb.AddForce(-transform.right * movefowardback);

        //Tilt Physics
        Quaternion rotation = (transform.rotation);
        if (timer < tiltinterval)
        {
            transform.rotation = Quaternion.Euler(rotation[0], rotation[1] + 0f, rotation[2] + tiltlimit);
        }

        if (timer > tiltinterval)
        {
            transform.rotation = Quaternion.Euler(rotation[0], rotation[1] + 0f, rotation[2] - tiltlimit);
        }

        //personal bubble
        foreach (GameObject AI in NPCsInRange)
        {
            if (AI != this)
            {
                rb.AddForce((AI.gameObject.transform.position - transform.position).normalized * -2f);
            }
        }

        //AI States
        switch (state)
        {
            case State.WALKINGPAST:
                Walkingpast();
                break;
            case State.QUEUING:
                Queuing();
                break;
            case State.FLEEING:
                Fleeing();
                break;
            case State.PANICKING:
                Panicking();
                break;

        }
    }

    void Walkingpast()
    {

    }

    void Queuing()
    {

    }

    void Fleeing()
    {

    }

    void Panicking()
    {

    }
}