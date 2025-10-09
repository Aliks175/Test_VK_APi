using System.Collections.Generic;
using UnityEngine;

public class AnvilManager : MonoBehaviour
{
    private List<AnvilSlot> list;

    private void Awake()
    {
        list = new List<AnvilSlot>(gameObject.GetComponentsInChildren<AnvilSlot>());
    }

    public void Initialize()
    {
        //Заполнить список листов 
        // list = new List<AnvilSlot>(gameObject.GetComponentsInChildren<AnvilSlot>());
        foreach (AnvilSlot slot in list)
        {
            slot.Initialize();
        }

    }

    private void Start() // Когда будет единая точка входа удалить 
    {
        Initialize();
        if (list != null)
        {
            //Debug.Log($"Count = {list.Count}");
        }
    }

    public void AddIron()
    {
        if (list == null) return;
        foreach (AnvilSlot slot in list)
        {
            if (slot.IsFree)
            {
                slot.Play();
                break;
            }
        }
    }

    public AnvilSlot CheckFreeAiron()
    {
        if (list == null) return null;
        foreach (AnvilSlot slot in list)
        {
            if (slot.IsIronReady && !slot.IsFire)
            {
                //slot.PickUpIron();
                return slot;
            }
        }
        return null;
    }
}
