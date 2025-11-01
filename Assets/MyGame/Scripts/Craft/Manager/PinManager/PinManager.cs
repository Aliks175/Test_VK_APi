using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    //public bool IsCreate { get; private set; }
    [SerializeField] private OrderManager _orderManager;
    //[SerializeField] private MySpinner _spinner;
    private TimerInfo _timerCreate;
    private List<SlotPin> _pinSlots;

    public void Initialize(InfoPinManager infoPinManager)
    {
        ////_spinner.Initialize();
        //_timerCreate = new TimerInfo() { Color = new Color(0, 1, 1, 1), StartTime = infoPinManager.WaitCreatePin };
        _pinSlots = new List<SlotPin>(gameObject.GetComponentsInChildren<SlotPin>(true));
        int count = infoPinManager.ValuePinSlot > _pinSlots.Count + 1 ? _pinSlots.Count : infoPinManager.ValuePinSlot;
        List<SlotPin> templist = new();
        for (int i = 0; i < count; i++)
        {
            templist.Add(_pinSlots[i]);
        }
        _pinSlots = templist;
        foreach (SlotPin slot in _pinSlots)
        {
            slot.Initialize(_orderManager);
        }
    }

    public SlotPin CheckFreeSlot()
    {
        if (_pinSlots == null) return null;
        foreach (SlotPin slot in _pinSlots)
        {
            if (slot.IsFree)
            {
                return slot;
            }
        }
        return null;
    }

    public void AddPin()
    {
        CreatePin();
        //_spinner.Play(_timerCreate, CreatePin);
        //IsCreate = true;
    }

    private void CreatePin()
    {
        foreach (SlotPin slot in _pinSlots)
        {
            slot.AddPin();
        }
        //IsCreate = false;
    }

    private void OnValidate()
    {
        if (_orderManager == null)
        {
            _orderManager = GameObject.FindFirstObjectByType<OrderManager>();
        }
    }
}