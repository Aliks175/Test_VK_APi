using System.Collections.Generic;
using UnityEngine;

public class MannequinManager : MonoBehaviour
{
    [SerializeField] private OrderManager _orderManager;
    [SerializeField] private GameObject _buttonCreateMetallArmor;
    [SerializeField] private GameObject _buttonCreateLeatherArmor;
    private List<MannequinSlots> _mannequinSlots;

    public void Initialize(InfoArmorManager infoArmorManager)
    {
        if (infoArmorManager.ValueArmorSlot == 0) return;
        _mannequinSlots = new List<MannequinSlots>(gameObject.GetComponentsInChildren<MannequinSlots>(true));
        int count = infoArmorManager.ValueArmorSlot > _mannequinSlots.Count + 1 ? _mannequinSlots.Count : infoArmorManager.ValueArmorSlot;
        List<MannequinSlots> templist = new();
        for (int i = 0; i < count; i++)
        {
            templist.Add(_mannequinSlots[i]);
        }
        _mannequinSlots = templist;
        foreach (MannequinSlots slot in _mannequinSlots)
        {
            slot.Initialize(_orderManager, infoArmorManager.WaitCreateLeather, infoArmorManager.WaitCreateMetall);
        }
        _buttonCreateMetallArmor.SetActive(infoArmorManager.IsOpenMetallArmor);
        _buttonCreateLeatherArmor.SetActive(true);
    }

    public MannequinSlots AddMetallArmor()
    {
        if (_mannequinSlots == null) return null;
        foreach (MannequinSlots slot in _mannequinSlots)
        {
            if (slot.IsFree)
            {
                return slot;
            }
        }
        return null;
    }

    public void AddLeatherArmor()
    {
        if (_mannequinSlots == null) return;
        foreach (MannequinSlots slot in _mannequinSlots)
        {
            if (slot.IsFree)
            {
                slot.AddArmor(TypeArmor.LeatherArmor);
                break;
            }
        }
    }

    private void OnValidate()
    {
        if (_orderManager == null)
        {
            _orderManager = GameObject.FindFirstObjectByType<OrderManager>();
        }
    }
}