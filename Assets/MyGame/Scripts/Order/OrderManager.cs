using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public TypeOrder typeOrder;
    private bool _isWork = false;
    private List<SlotOrder> list;
    private Coroutine _coroutine;

    private void Awake()
    {
        list = new List<SlotOrder>(gameObject.GetComponentsInChildren<SlotOrder>());
    }

    public void Initialize()
    {
        //Заполнить список листов 
        // list = new List<AnvilSlot>(gameObject.GetComponentsInChildren<AnvilSlot>());

        foreach (SlotOrder slot in list)
        {
            slot.Initialize(typeOrder);
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
        foreach (SlotOrder slot in list)
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

            foreach (SlotOrder slot in list)
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

public enum TypeOrder
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
