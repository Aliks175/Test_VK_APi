using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SlotOrder : MonoBehaviour
{
    [Header("ChildrenClass")]
    [SerializeField] private ViewOrder _viewOrder;
    [SerializeField] private TimerOrder _timerOrder;
    [Header("RandomVisitSettings")]
    [SerializeField] private float _timeWaitMax = 7f;
    [SerializeField] private float _timeWaitMin = 3f;
   [HideInInspector] public bool IsVisit { get; private set; } = true;


    private ListOrders listOrders;
    private bool _readyOrder = false;
    private int levellock;
    private TypeOrder typeOrder;
    private WaitForSeconds wait = null;


    public UnityEvent OnInitialize;

    public UnityEvent OnStart;

    public UnityEvent OnEnd;
    /// <summary>
    /// Инициализация проброс зависимостей 
    /// </summary>
    /// <param name = "order" ></ param >
    public void Initialize(TypeOrder order)
    {
        if (_viewOrder != null)
        {
            _viewOrder.Initialize();
        }

        if (_timerOrder != null)
        {
            _timerOrder.Initialize();
        }

        listOrders = new ListOrders();
        typeOrder = order;

        CorectLevelOrder(typeOrder);
        OnInitialize?.Invoke();

      
    }

    /// <summary>
    /// Метод активирующий начало фазы игры 
    /// </summary>
    public void StartWork()
    {
        if (IsVisit == false) return;

        ChooseOrder();
        if (CheckListOrder())
        {
            IsVisit = false;
            _viewOrder.SetView(listOrders);
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

        if (!CheckListOrder())
        {
            _viewOrder.OnView(listOrders);
            OnEnd?.Invoke();
            IsVisit = true;
            _readyOrder = false;
            ClearListOrder();
            _timerOrder.Stop();
        }
    }

    public bool UseOrder(OrderItem order)
    {
        bool isUse = false;
        if (_readyOrder)
        {
            if (listOrders.OneItem == order)
            {
                listOrders.OneItem = OrderItem.none;
                isUse = true;
            }
            else if (listOrders.TwoItem == order)
            {
                listOrders.TwoItem = OrderItem.none;
                isUse = true;
            }
            else if (listOrders.ThreeItem == order)
            {
                listOrders.ThreeItem = OrderItem.none;
                isUse = true;
            }
            _viewOrder.OnView(listOrders);
            if (isUse)
            {
                _timerOrder.AddTimeWait(); 
            }
        }
        return isUse;
    }

    private void Clientleave()
    {
        ClearListOrder();
        _viewOrder.OnView(listOrders);
        OnEnd?.Invoke();
        IsVisit = true;
        _readyOrder = false;
        _timerOrder.Stop();
    }

    /// <summary>
    /// Ожидание до прихода гостя 
    /// </summary>
    /// <param name = "time" ></ param >
    /// < returns ></ returns >
    private IEnumerator ComonVisit()
    {
        yield return wait;
        StartEvent();
        _viewOrder.OnView(listOrders);
        _readyOrder = true;
        _timerOrder.Play(Clientleave);
        //должны обратится к другому классу для отображения заказа
    }

    #region Events

    /// <summary>
    /// Unity событие отыгрывается в начале запуска сценария 
    /// </summary>
    private void StartEvent()
    {
        OnStart?.Invoke();
    }

    #endregion

    private bool CheckListOrder()
    {
        bool isNotNull = false;
        if (listOrders.OneItem != OrderItem.none)
        {
            isNotNull = true;
        }
        else if (listOrders.TwoItem != OrderItem.none)
        {
            isNotNull = true;
        }
        else if (listOrders.ThreeItem != OrderItem.none)
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
                listOrders.OneItem = ChooseType();
            }
            else if (i == 1)
            {
                listOrders.TwoItem = ChooseType();
            }
            else if (i == 2)
            {
                listOrders.ThreeItem = ChooseType();
            }

        }
    }

    /// <summary>
    /// Очистка списка заказов 
    /// </summary>
    private void ClearListOrder()
    {
        listOrders.OneItem = OrderItem.none;
        listOrders.TwoItem = OrderItem.none;
        listOrders.ThreeItem = OrderItem.none;
    }

    /// <summary>
    /// Определения какой предмет будет в заказе 
    /// </summary>
    /// <returns></returns>
    private OrderItem ChooseType()
    {
        OrderItem newOrder = new();
        int typeItem = Random.Range(1, levellock);

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
        int typeSwordLock = (int)typeOrder;

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
        int typeArmorLock = (int)typeOrder;

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
    private void CorectLevelOrder(TypeOrder order)
    {
        if (order == TypeOrder.none)
        {
            Debug.LogError($"Not found TypeOrder - {gameObject.name}");
            return;
        }
        levellock = (int)order;
        if (levellock >= 3)
        {
            levellock = 4;
        }
        else
        {
            levellock += 1;
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
//public struct Order
//{
//    public bool Pin;//1) гвозди
//    public TypeSword TypeSword;//мечи
//    public TypeArmor TypeArmor;//доспехи
//}

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

