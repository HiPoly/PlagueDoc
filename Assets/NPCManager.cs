using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public List<GameObject> Queue = new List<GameObject>();

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
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {

            transform.position = new Vector3(startPos[0] + Random.Range(-scatter, scatter), 0f, startPos[2] + Random.Range(-scatter, scatter));
            spawnTimer = 0f;
            Instantiate(NPC, transform.position, Quaternion.identity);
        }
        howfullisqueue = Queue.Count;
    }

    private Vector3 startPos;
    private void Awake()
    {
        startPos = transform.position;
    }
}
