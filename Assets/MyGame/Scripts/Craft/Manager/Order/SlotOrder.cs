using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotOrder : MonoBehaviour
{
    [HideInInspector] public bool IsVisit { get; private set; } = true;
    [Header("ChildrenClass")]
    [SerializeField] private ViewOrder _viewOrder;
    [SerializeField] private TimerOrder _timerOrder;
    [Header("TimerOrderSettings")]
    [SerializeField] private float _timeWait = 10f;
    [Header("RandomVisitSettings")]
    [SerializeField] private float _timeWaitMax = 7f;
    [SerializeField] private float _timeWaitMin = 3f;
    private TaskInfo _taskInfo;
    private ListOrders _listOrders;
    private LevelHard _typeOrder;
    private WaitForSeconds wait = null;
    private int _levellock;
    private bool _readyOrder = false;
    public event Action<TaskInfo> OnCloseOrder;

    /// <summary>
    /// Инициализация проброс зависимостей 
    /// </summary>
    /// <param name = "order" ></ param >
    public void Initialize(LevelHard order)
    {
        if (_viewOrder != null)
        {
            _viewOrder.Initialize();
        }
        if (_timerOrder != null)
        {
            _timerOrder.Initialize(_timeWait);
        }
        _listOrders = new ListOrders();
        _taskInfo = new TaskInfo();
        _typeOrder = order;
        CorectLevelOrder(_typeOrder);
    }

    /// <summary>
    /// Метод активирующий начало фазы игры 
    /// </summary>
    public void StartWork()
    {
        if (IsVisit == false) return;
        ChooseOrder();
        if (CheckListOrder(OrderItem.none))
        {
            IsVisit = false;
            _viewOrder.SetView(_listOrders);
            float time = Random.Range(_timeWaitMin, _timeWaitMax);
            wait = wait ?? new WaitForSeconds(time);
            StartCoroutine(ComonVisit());
        }
        else
        {
            ChooseOrder();
        }
    }

    public void CheckOverOrder()
    {
        if (!_readyOrder) return;
        if (!CheckListOrder(OrderItem.none))
        {
            _taskInfo.IsFastOrder = _timerOrder.CheckFastOrder();

            Debug.Log("Use - OnCloseOrder");
            OnCloseOrder?.Invoke(_taskInfo);
            Clientleave();
        }
    }

    public bool UseOrder(OrderItem order)
    {
        bool isUse = false;
        if (_readyOrder)
        {
            if (_listOrders.OneItem == order)
            {
                _listOrders.OneItem = OrderItem.none;
                isUse = true;
            }
            else if (_listOrders.TwoItem == order)
            {
                _listOrders.TwoItem = OrderItem.none;
                isUse = true;
            }
            else if (_listOrders.ThreeItem == order)
            {
                _listOrders.ThreeItem = OrderItem.none;
                isUse = true;
            }
            _viewOrder.OnView(_listOrders);
        }
        return isUse;
    }

    public void AddTimeWait()
    {
    _timerOrder.AddTimeWait();
    }

    private void Clientleave()
    {
        ClearListOrder();
        _viewOrder.OnView(_listOrders);
        _viewOrder.OnEndEvent();
        IsVisit = true;
        _readyOrder = false;
        _timerOrder.Stop();
        ClearTaskInfo();
    }

    private void ClearTaskInfo()
    {
        _taskInfo.IsFastOrder = false;
    }

    /// <summary>
    /// Ожидание до прихода гостя 
    /// </summary>
    /// <param name = "time" ></ param >
    /// < returns ></ returns >
    private IEnumerator ComonVisit()
    {
        yield return wait;
        _viewOrder.OnStartEvent();
        _viewOrder.OnView(_listOrders);
        _readyOrder = true;
        _timerOrder.Play(Clientleave);
        //должны обратится к другому классу для отображения заказа
    }

    //#region Events
    ///// <summary>
    ///// Unity событие отыгрывается в начале запуска сценария 
    ///// </summary>
    //private void StartEvent()
    //{
    //    OnStart?.Invoke();
    //}
    //#endregion

    private bool CheckListOrder(OrderItem orderItem)
    {
        bool isNotNull = false;
        if (_listOrders.OneItem != orderItem)
        {
            isNotNull = true;
        }
        else if (_listOrders.TwoItem != orderItem)
        {
            isNotNull = true;
        }
        else if (_listOrders.ThreeItem != orderItem)
        {
            isNotNull = true;
        }
        return isNotNull;
    }

    #region CraftSystem
    /// <summary>
    /// Метод создает список заказов пользователя 
    /// </summary>
    private void ChooseOrder()
    {
        int valueOrder = Random.Range(1, 4); // Выбор сколько предметов в этом заказе 
        //После метода Clear остаются ли пустые слоты?
        //можно ли после него обращаться по индексу ?
        ClearListOrder();
        for (int i = 0; i < valueOrder; i++)
        {
            if (i == 0)
            {
                _listOrders.OneItem = ChooseType();
            }
            else if (i == 1)
            {
                _listOrders.TwoItem = ChooseType();
            }
            else if (i == 2)
            {
                _listOrders.ThreeItem = ChooseType();
            }
        }
    }

    /// <summary>
    /// Очистка списка заказов 
    /// </summary>
    private void ClearListOrder()
    {
        _listOrders.OneItem = OrderItem.none;
        _listOrders.TwoItem = OrderItem.none;
        _listOrders.ThreeItem = OrderItem.none;
    }

    /// <summary>
    /// Определения какой предмет будет в заказе 
    /// </summary>
    /// <returns></returns>
    private OrderItem ChooseType()
    {
        OrderItem newOrder = new();
        int typeItem = Random.Range(1, _levellock);

        switch (typeItem)
        {
            case 1:
                //создать пин
                newOrder = OrderItem.Pin;
                break;
            case 2:
                //создать меч
                newOrder = ChooseSword();
                break;
            case 3:
                //создать броню
                newOrder = ChooseArmor();
                break;
        }

        return newOrder;
    }

    /// <summary>
    /// определение какой меч будет в заказе 
    /// </summary>
    /// <returns></returns>
    private OrderItem ChooseSword()
    {
        OrderItem sword = new();
        int typeSwordLock = (int)_typeOrder;

        if (typeSwordLock < 5)
        {
            return OrderItem.Sword;
        }
        else if (typeSwordLock == 5)
        {
            int typeItem = Random.Range(2, 6);
            sword = (OrderItem)typeItem;
        }
        return sword;
    }

    /// <summary>
    /// определение какой доспех будет в заказе 
    /// </summary>
    /// <returns></returns>
    private OrderItem ChooseArmor()
    {
        OrderItem armor = new();
        int typeArmorLock = (int)_typeOrder;
        if (typeArmorLock < 4)
        {
            return OrderItem.ArmorLeather;
        }
        else if (typeArmorLock >= 4)
        {
            int typeItem = Random.Range(6, 8);
            armor = (OrderItem)typeItem;
        }
        return armor;
    }

    /// <summary>
    /// Костыль создает переменную которая используется в ограничении выподения предметов 
    /// </summary>
    /// <param name = "order" ></ param >
    private void CorectLevelOrder(LevelHard order)
    {
        if (order == LevelHard.none)
        {
            Debug.LogError($"Not found TypeOrder - {gameObject.name}");
            return;
        }
        _levellock = (int)order;
        if (_levellock >= 3)
        {
            _levellock = 4;
        }
        else
        {
            _levellock += 1;
        }
    }
    #endregion

    private void OnValidate()
    {
        if (_viewOrder == null && gameObject.activeInHierarchy)
        {
            _viewOrder = GetComponent<ViewOrder>();
        }
    }
}
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

/// <summary>
/// Является предметом из списка заказов 
/// </summary>
public enum OrderItem
{
    none,
    Pin,
    Sword,
    SwordLezz,
    SwordHand,
    SwordFull,
    ArmorLeather,
    ArmorMetall
}

public struct ListOrders
{
    public OrderItem OneItem;
    public OrderItem TwoItem;
    public OrderItem ThreeItem;
}