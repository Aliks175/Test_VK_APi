using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private bool _isWork = false;
    private List<SlotOrder> _orderSlots;
    private Coroutine _coroutine;

    public void Initialize(LevelHard typeOrder)
    {
        _orderSlots = new List<SlotOrder>(gameObject.GetComponentsInChildren<SlotOrder>(true));

        if (_orderSlots == null) return;

        foreach (SlotOrder slot in _orderSlots)
        {
            slot.Initialize(typeOrder);
        }
    }

    /// <summary>
    /// После туториала можно запустить игровой процесс 
    /// </summary>
    public void StartWork()
    {
        if (_coroutine != null) return;
        _isWork = true;
        _coroutine = StartCoroutine(Wait());
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
}

public enum LevelHard
{
    //    1) гвозди 
    //1 ур (Туториал и игровой процесс)
    //2) гвозди + мечи 
    //2 - 3 ур 
    //3) гвозди + мечи + кожаный доспех
    //4 - 6 ур 
    //4) гвозди + мечи + кожаный доспех + металлический доспех 
    //7 - 10 ур
    //5) гвозди + мечи + кожаный доспех + металлический доспех  + полный меч
    //+10 ур

    none,
    LevelOne,//    1) гвозди - 1 ур (Туториал и игровой процесс)
    LevelTwo,//2) гвозди + меч - 2 - 3 ур 
    LevelThree,//3) гвозди + мечи + кожаный доспех - 4 - 6 ур 
    LevelFour,//4) гвозди + мечи + кожаный доспех + металлический доспех - 7 - 10 ур
    LevelFive,//5) гвозди + мечи + кожаный доспех + металлический доспех  + полный меч - +10 ур
}