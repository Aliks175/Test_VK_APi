using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    [SerializeField] private OrderManager _orderManager;
    private List<SwordSlot> list;

    private void Awake()
    {
        list = new List<SwordSlot>(gameObject.GetComponentsInChildren<SwordSlot>());
    }

    public void Initialize()
    {
        //Заполнить список листов 
        // list = new List<AnvilSlot>(gameObject.GetComponentsInChildren<AnvilSlot>());
        foreach (SwordSlot slot in list)
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

    public void UpgradeSwordHand()
    {
        if (list == null) return;
        foreach (SwordSlot slot in list)
        {
            if (!slot.IsFree)
            {
                if (slot.TypeSword == TypeSword.SwordLezz || slot.TypeSword == TypeSword.Sword)
                {
                    slot.ChangeHand();
                    break;
                }
            }
        }
    }

    public void UpgradeSwordLezz()
    {
        if (list == null) return;
        foreach (SwordSlot slot in list)
        {
            if (!slot.IsFree)
            {
                if (slot.TypeSword == TypeSword.SwordHand || slot.TypeSword == TypeSword.Sword)
                {
                    slot.ChangeLezz();
                    break;
                }
            }
        }
    }

    public SwordSlot CheckFreeSlot()
    {
        if (list == null) return null;
        foreach (SwordSlot slot in list)
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
