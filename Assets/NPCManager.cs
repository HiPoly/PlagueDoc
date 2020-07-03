using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NPCManager : MonoBehaviour
{
    public List<GameObject> Queue = new List<GameObject>();
    public Text money;
    public float moneyvalue;
    public float timer;
    public Text timertext;

    private void Start()
    {
        timer = 200;
    }
    public void AddNPC(GameObject NPC)
    {
        Queue.Add(NPC);
    }

    public void RemoveNPC(GameObject NPC)
    {
        Queue.Remove(NPC);
    }

    public int CheckPlace(GameObject NPC)
    {
        int placeonlist = Queue.IndexOf(NPC);
        return placeonlist;
    }

    private int AmIOnList;
    public int CheckifOnlist(GameObject NPC)
    {
        if (Queue.Contains(NPC))
        {
            AmIOnList = 1;
        }
        if (!Queue.Contains(NPC))
        {
            AmIOnList = 0;
        }
        return AmIOnList;
    }

    private float spawnTimer;
    public float spawnRate;
    public GameObject NPC;
    public float scatter;
    public int howfullisqueue;
    public void Update()
    {
        timer -= Time.deltaTime;
        money.text = moneyvalue.ToString();
        timertext.text = timer.ToString();
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {

            transform.position = new Vector3(startPos[0] + Random.Range(-scatter, scatter), 0f, startPos[2] + Random.Range(-scatter, scatter));
            spawnTimer = 0f;
            Instantiate(NPC, transform.position, Quaternion.identity);
        }
        howfullisqueue = Queue.Count;

        if (timer < 0f)
        {
            SceneManager.LoadScene("Start");
        }
    }

    private Vector3 startPos;
    private void Awake()
    {
        startPos = transform.position;
    }

    public void addmoney()
    {
        moneyvalue += Random.Range(0, 6);
    } 
}
