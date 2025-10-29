using UnityEngine;
using UnityEngine.Events;

public class SwordSlot : MonoBehaviour
{
    public bool IsFree { get; private set; } = true;
    public TypeSword TypeSword { get; private set; }
    public UnityEvent _initializeEvent;
    public UnityEvent _startEvent;
    public UnityEvent _changeLezzEvent;
    public UnityEvent _changeHandEvent;
    public UnityEvent _changeFullEvent;
    public UnityEvent _pickUpEvent;
    private OrderManager _orderManager;

    public void Initialize(OrderManager orderManager)
    {
        gameObject.SetActive(true);
        _orderManager = orderManager;
        _initializeEvent?.Invoke();
        TypeSword = TypeSword.None;
    }

    public void AddSword()
    {
        IsFree = false;
        TypeSword = TypeSword.Sword;
        StartEvent();
    }

    public void ChangeHand()
    {
        ChangeSword(TypeSword.SwordHand);
    }

    public void ChangeLezz()
    {
        ChangeSword(TypeSword.SwordLezz);
    }

    public void PickUp()
    {
        if (IsFree) return;
        if (TypeSword != TypeSword.None)
        {
            OrderItem item = (OrderItem)TypeSword + 1; // Мы переводим enum TypeSword в enum OrderItem По этому значение увеличиваем 
            if (_orderManager.UseOrder(item))           //1 , 2 ,3, 4      //2 ,3, 4 , 5 
            {
                IsFree = true;
                TypeSword = TypeSword.None;
                _pickUpEvent?.Invoke();
            }
        }
    }

    public void ClearSlot()
    {
        if (IsFree) return;
        TypeSword = TypeSword.None;
        IsFree = true;
        _pickUpEvent?.Invoke();
    }

    private void ChangeSword(TypeSword newTypeSword)
    {
        if (newTypeSword == TypeSword) return;
        if (newTypeSword == TypeSword.None) return;
        if (newTypeSword == TypeSword.Sword) return;
        switch (TypeSword)
        {
            case TypeSword.Sword:
                TypeSword = newTypeSword;
                ChangeSwordEvent(newTypeSword);
                break;
            case TypeSword.SwordHand:
            case TypeSword.SwordLezz:
                TypeSword = TypeSword.SwordFull;
                ChangeSwordEvent(TypeSword);
                break;
        }
    }
    #region Events

    private void StartEvent()
    {
        _startEvent?.Invoke();
    }

    private void ChangeSwordEvent(TypeSword typeSword)
    {
        if (typeSword == TypeSword.SwordLezz)
        {
            _changeLezzEvent?.Invoke();
        }
        else if (typeSword == TypeSword.SwordHand)
        {
            _changeHandEvent?.Invoke();
        }
        else if (typeSword == TypeSword.SwordFull)
        {
            _changeFullEvent?.Invoke();
        }
    }
    #endregion

    private void OnValidate()
    {
        if (_orderManager == null)
        {
            _orderManager = GameObject.FindFirstObjectByType<OrderManager>();
        }
    }
}

public enum TypeSword
{
    None,
    Sword,
    SwordLezz,
    SwordHand,
    SwordFull
}