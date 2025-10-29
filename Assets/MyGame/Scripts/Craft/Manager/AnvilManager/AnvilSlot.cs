using UnityEngine;
using UnityEngine.Events;

public class AnvilSlot : MonoBehaviour
{
    public bool IsFree { get; private set; } = true;
    public bool IsIronReady { get; private set; } = false;
    public bool IsFire { get; private set; } = false;
    [SerializeField] private MySpinner _spinner;
    [SerializeField] private float _timeClearBreack = 3;
    public UnityEvent _initializeEvent;
    public UnityEvent _startEvent;
    public UnityEvent _endEvent;
    public UnityEvent _endFireEvent;
    public UnityEvent _pickUpIronEvent;
    public UnityEvent _clearIronEvent;
    private TimerInfo _timerFire;
    private TimerInfo _timerCreateIron;
    private TimerInfo _timerClearBreackIron;

    public void Initialize(float timeCreateIron, float timeFire)
    {
        if (_spinner != null)
        {
            _spinner.Initialize();
        }
        gameObject.SetActive(true);
        _timerCreateIron = new TimerInfo() { Color = new Color(0.5f, 1, 0.5f, 1), StartTime = timeCreateIron };
        _timerFire = new TimerInfo() { Color = new Color(1, 0, 0, 1), StartTime = timeFire };
        _timerClearBreackIron = new TimerInfo() { Color = new Color(1, 0.8f, 0, 1), StartTime = _timeClearBreack };
        _initializeEvent?.Invoke();
    }

    public void Play()
    {
        IsFree = false;
        if (_spinner != null)
        {
            StartEvent();
            _spinner.Play(_timerCreateIron, EndEvent);
        }
    }

    public void PickUpIron()
    {
        _spinner.Stop();
        IsFree = true;
        IsIronReady = false;
        _pickUpIronEvent?.Invoke();
    }

    public void ClearBreackIron()
    {
        if (_spinner != null)
        {
            _spinner.Play(_timerClearBreackIron, Clear);
        }
    }

    #region Events
    private void Clear()
    {
        IsIronReady = false;
        IsFire = false;
        IsFree = true;
        _clearIronEvent?.Invoke();
    }

    private void StartEvent()
    {
        _startEvent?.Invoke();
    }

    private void EndEvent()
    {
        if (_spinner != null)
        {
            _spinner.Play(_timerFire, EndFireEvent);
            IsIronReady = true;
        }
        _endEvent?.Invoke();
    }

    private void EndFireEvent()
    {
        IsIronReady = false;
        IsFire = true;
        _endFireEvent?.Invoke();
    }
    #endregion

    private void OnValidate()
    {
        if (_spinner == null)
        {
            _spinner = GetComponentInChildren<MySpinner>(true);
        };
    }
}

public struct TimerInfo
{
    public Color Color;
    public float StartTime;
}