using UnityEngine;
using UnityEngine.Events;

public class SlotPin : MonoBehaviour
{
    public bool IsFree { get; private set; } = true;
    public UnityEvent _initializeEvent;
    public UnityEvent _startEvent;
    public UnityEvent _pickUpEvent;
    private OrderManager _orderManager;

    public void Initialize(OrderManager orderManager)
    {
        gameObject.SetActive(true);
        _orderManager = orderManager;
        _initializeEvent?.Invoke();
    }

    public void AddPin()
    {
        IsFree = false;
        StartEvent();
    }

    public void PickUp()
    {
        if (IsFree) return;
        if (_orderManager.UseOrder(OrderItem.Pin))
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        if (IsFree) return;
        IsFree = true;
        _pickUpEvent?.Invoke();
    }

    #region Events
    private void StartEvent()
    {
        _startEvent?.Invoke();
    }
    #endregion
}