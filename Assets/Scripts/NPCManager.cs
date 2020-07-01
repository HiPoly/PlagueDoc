using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;

    public List<AI> NpcList;
    public List<int> listInt;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void AddNPC(AI p)
    {
        NpcList.Add(p);
        UpdateNPCRelationships();
    }

    public void RemoveNPC(AI p)
    {
        NpcList.Remove(p);
        UpdateNPCRelationships();
    }

    private void UpdateNPCRelationships()
    {
        for (int i = 0; i < NpcList.Count; i++)
        {
           
        }
    }

}

