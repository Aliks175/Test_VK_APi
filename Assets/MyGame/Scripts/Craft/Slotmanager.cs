using System.Collections.Generic;
using UnityEngine;

public class Slotmanager : MonoBehaviour
{
    private List<Slot> list;

    private void Awake()
    {
        list = new List<Slot>(gameObject.GetComponentsInChildren<Slot>());
    }

    private void Start()
    {
        if (list != null)
        {
            Debug.Log($"Count = {list.Count}");
        }
    }


    public void AddDetal(Resept resept)
    {
        foreach (Slot slot in list)
        {
            if (slot.CheckDetal(resept))
            {
                slot.AddDetals(resept);
                break;
            } 
        }
    }
}
