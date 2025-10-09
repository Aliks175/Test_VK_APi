using System.Collections.Generic;
using UnityEngine;

public class ManikenManager : MonoBehaviour
{
    [SerializeField] private OrderManager _orderManager;
    private List<ManikenSlot> list;

    private void Awake()
    {
        list = new List<ManikenSlot>(gameObject.GetComponentsInChildren<ManikenSlot>());
    }

    public void Initialize()
    {
        //Заполнить список листов 
        // list = new List<AnvilSlot>(gameObject.GetComponentsInChildren<AnvilSlot>());
        foreach (ManikenSlot slot in list)
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

    public ManikenSlot AddMetallArmor()
    {
        if (list == null) return null;
        foreach (ManikenSlot slot in list)
        {
            if (slot.IsFree)
            {
                //slot.AddArmor(TypeArmor.MetallArmor);
                return slot;
            }
        }
        return null;
    }

    public void AddLeatherArmor()
    {
        if (list == null) return;
        foreach (ManikenSlot slot in list)
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
