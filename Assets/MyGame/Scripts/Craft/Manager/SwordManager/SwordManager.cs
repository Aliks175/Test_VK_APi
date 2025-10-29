using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    [SerializeField] private OrderManager _orderManager;
    [SerializeField] private GameObject _panelUpgrade;
    [SerializeField] private GameObject _buttonCreate;
    private List<SwordSlot> _swordSlots;

    public void Initialize(InfoSwordManager infoSwordManager)
    {
        if (infoSwordManager.ValueSwordSlot == 0) return;
        _swordSlots = new List<SwordSlot>(gameObject.GetComponentsInChildren<SwordSlot>(true));
        int count = infoSwordManager.ValueSwordSlot > _swordSlots.Count + 1 ? _swordSlots.Count : infoSwordManager.ValueSwordSlot;
        List<SwordSlot> templist = new();
        for (int i = 0; i < count; i++)
        {
            templist.Add(_swordSlots[i]);
        }
        _swordSlots = templist;
        foreach (SwordSlot slot in _swordSlots)
        {
            slot.Initialize(_orderManager);
        }
        _buttonCreate.SetActive(true);
        _panelUpgrade.SetActive(infoSwordManager.IsUpgrade);
    }

    public void UpgradeSwordHand()
    {
        if (_swordSlots == null) return;
        foreach (SwordSlot slot in _swordSlots)
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
        if (_swordSlots == null) return;
        foreach (SwordSlot slot in _swordSlots)
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
        if (_swordSlots == null) return null;
        foreach (SwordSlot slot in _swordSlots)
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