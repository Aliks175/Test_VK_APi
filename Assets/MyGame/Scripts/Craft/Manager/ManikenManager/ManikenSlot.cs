using UnityEngine;
using UnityEngine.Events;

public class ManikenSlot : MonoBehaviour
{
    [SerializeField] private MySpinner _spinner;
    [SerializeField] private float _timeCreateLeatherArmor = 4;
    [SerializeField] private float _timeCreateMetallArmor = 6;


    public bool IsFree { get; private set; } = true;

    public bool IsMetallArmorReady { get; private set; } = false;
    public bool IsLeatherArmorReady { get; private set; } = false;

    public UnityEvent _initializeEvent;

    public UnityEvent _startEvent;

    public UnityEvent _endLeatherEvent;
    public UnityEvent _endMetallEvent;

    public UnityEvent _pickUpArmorEvent;

    private TypeArmor armor;
    private OrderManager _orderManager;

    private TimerInfo timerCreateMetallArmor;
    private TimerInfo timerCreateLeatherArmor;

    public void Initialize(OrderManager orderManager)
    {
        if (_spinner != null)
        {
            _spinner.Initialize();
        }
        else
        {
            Debug.LogError($"Not Found MySpinner - {gameObject.name}");
        }
        _orderManager = orderManager;
        timerCreateMetallArmor = new TimerInfo() { Color = new Color(0, 1, 1, 1), StartTime = _timeCreateMetallArmor };
        timerCreateLeatherArmor = new TimerInfo() { Color = new Color(1, 0.5f, 0, 1), StartTime = _timeCreateLeatherArmor };
        _initializeEvent?.Invoke();
    }

    private void OnDisable()
    {
        if (_spinner != null)
        {
        }
    }

    public void AddArmor(TypeArmor typeArmor)
    {
        IsFree = false;
        if (_spinner == null) return;
        StartEvent();
        if (typeArmor == TypeArmor.LeatherArmor)
        {
            _spinner.Play(timerCreateLeatherArmor, EndLeatherEvent);
        }
        else if (typeArmor == TypeArmor.MetallArmor)
        {
            _spinner.Play(timerCreateMetallArmor, EndMetallEvent);
        }
    }

    public void PickUp()
    {
        if (IsFree) return;
        if (armor != TypeArmor.None)
        {
            if (armor == TypeArmor.LeatherArmor)
            {
                if (_orderManager.UseOrder(OrderItem.ArmorLeather))
                {
                    IsFree = true;
                    armor = TypeArmor.None;
                    _pickUpArmorEvent?.Invoke();
                }
            }
            else if (armor == TypeArmor.MetallArmor)
            {
                if (_orderManager.UseOrder(OrderItem.ArmorMetall))
                {
                    IsFree = true;
                    armor = TypeArmor.None;
                    _pickUpArmorEvent?.Invoke();
                }
            }
        }
    }

    public void ClearSlot()
    {
        //PickUp();
        if (IsFree) return;
        if (armor == TypeArmor.None) return;
        Debug.Log("Drag");
        armor = TypeArmor.None;
        IsFree = true;
        _pickUpArmorEvent?.Invoke();
    }


    #region Events

    private void StartEvent()
    {
        _startEvent?.Invoke();
    }

    private void EndLeatherEvent()
    {
        armor = TypeArmor.LeatherArmor;
        _endLeatherEvent?.Invoke();
    }

    private void EndMetallEvent()
    {
        armor = TypeArmor.MetallArmor;
        _endMetallEvent?.Invoke();
    }


    #endregion


}

public enum TypeArmor
{
    None,
    LeatherArmor,
    MetallArmor
}

