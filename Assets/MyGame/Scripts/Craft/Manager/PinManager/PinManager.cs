using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    [SerializeField] private OrderManager _orderManager;
    private List<SlotPin> list;

    private void Awake()
    {
        list = new List<SlotPin>(gameObject.GetComponentsInChildren<SlotPin>());
    }

    public void Initialize()
    {
        //Заполнить список листов 
        // list = new List<AnvilSlot>(gameObject.GetComponentsInChildren<AnvilSlot>());
        foreach (SlotPin slot in list)
        {
            slot.Initialize(_orderManager);
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

    public SlotPin CheckFreeSlot()
    {
        if (list == null) return null;
        foreach (SlotPin slot in list)
        {
            if (slot.IsFree)
            {
                return slot;
            }
        }
        return null;
    }


    private void OnValidate()
    {
        if (_orderManager == null)
        {
            _orderManager = GameObject.FindFirstObjectByType<OrderManager>();
        }
    }
}
