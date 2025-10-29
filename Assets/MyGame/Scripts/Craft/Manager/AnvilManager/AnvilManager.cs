using System.Collections.Generic;
using UnityEngine;

public class AnvilManager : MonoBehaviour
{
    private List<AnvilSlot> _anvilSlots;

    public void Initialize(InfoAnvilManager infoAnvilManager)
    {
        _anvilSlots = new List<AnvilSlot>(gameObject.GetComponentsInChildren<AnvilSlot>(true));
        int count = infoAnvilManager.ValueAnvil >= _anvilSlots.Count + 1 ? _anvilSlots.Count : infoAnvilManager.ValueAnvil;
        List<AnvilSlot> templist = new();

        for (int i = 0; i < count; i++)
        {
            templist.Add(_anvilSlots[i]);
        }
        _anvilSlots = templist;
        foreach (AnvilSlot slot in _anvilSlots)
        {
            slot.Initialize(infoAnvilManager.WaitCreateIron, infoAnvilManager.WaitFireIron);
        }
    }

    public void AddIron()
    {
        if (_anvilSlots == null) return;
        foreach (AnvilSlot slot in _anvilSlots)
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
        if (_anvilSlots == null) return null;
        foreach (AnvilSlot slot in _anvilSlots)
        {
            if (slot.IsIronReady && !slot.IsFire)
            {
                return slot;
            }
        }
        return null;
    }
}