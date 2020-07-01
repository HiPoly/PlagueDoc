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
}
