using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private bool _isWork = false;
    private List<SlotOrder> _orderSlots;
    private Coroutine _coroutine;
    private GoalManager _goalManager;
    private GoldManager _goldManager;

    public event Action OnOver;

    private void OnDisable()
    {
        foreach (SlotOrder slot in _orderSlots)
        {
            slot.OnCloseOrder -= _goalManager.CloseOrder;
        }
    }

    public void Initialize(LevelHard typeOrder, GoalManager taskManager, GoldManager goldManager)
    {
        _orderSlots = new List<SlotOrder>(gameObject.GetComponentsInChildren<SlotOrder>(true));
        _goldManager = goldManager;
        _goalManager = taskManager;
        if (_orderSlots == null) return;

        foreach (SlotOrder slot in _orderSlots)
        {
            slot.Initialize(typeOrder);
            slot.OnCloseOrder += _goalManager.CloseOrder;
        }
    }

    /// <summary>
    /// ѕосле туториала можно запустить игровой процесс 
    /// </summary>
    public void StartWork()
    {
        if (_coroutine != null) return;
        _isWork = true;
        _coroutine = StartCoroutine(Wait());

    }

    public void StopWork()
    {
        // когда у нас отыгрывает что мы закончили раунд
        // мы должны дождатьс€ когда все клиенты уйдут
        // и после этого по€вл€етс€ панель с финальными результатами
        // 
        if (_coroutine == null) return;
        _isWork = false;
        StopCoroutine(_coroutine);
        StartCoroutine(WaitLeaveClient());
        Debug.Log("Work is Stop");
    }

    public bool UseOrder(OrderItem orderItem)
    {
        bool succsesful = false;
        foreach (SlotOrder slot in _orderSlots)
        {
            if (!slot.IsVisit)
            {
                succsesful = slot.UseOrder(orderItem);
                if (succsesful)
                {
                    slot.CheckOverOrder();
                    slot.AddTimeWait();
                    _goldManager.SetGold(orderItem);
                    break;
                }
            }
        }
        return succsesful;
    }

    private IEnumerator Wait()
    {
        while (_isWork)
        {
            yield return null;

            foreach (SlotOrder slot in _orderSlots)
            {
                yield return new WaitForSeconds(1f);
                if (slot.IsVisit)
                {
                    slot.StartWork();
                }
            }
        }
    }

    private IEnumerator WaitLeaveClient()
    {
        Debug.Log("WaitLeaveClient");
        yield return new WaitUntil(() => CheckClearClient());
        OnOver?.Invoke();
    }

    private bool CheckClearClient()
    {
        bool NoClient = true;
        Debug.Log("CheckClearClient");
        foreach (SlotOrder slot in _orderSlots)
        {
            if (!slot.IsVisit)
            {
                return false;
            }
        }
        return NoClient;
    }

}
