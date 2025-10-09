using UnityEngine;
using UnityEngine.Events;

public class AnvilSlot : MonoBehaviour
{
    [SerializeField] private MySpinner _spinner;
    [SerializeField] private float _timeCreateIron = 4;
    [SerializeField] private float _timeFire = 8;
    [SerializeField] private float _timeClearBreack = 3;

    public bool IsFree { get; private set; } = true;
    public bool IsIronReady { get; private set; } = false;
    public bool IsFire { get; private set; } = false;

    public UnityEvent _initializeEvent;

    public UnityEvent _startEvent;
    public UnityEvent _endEvent;

    public UnityEvent _endFireEvent;

    public UnityEvent _pickUpIronEvent;
    public UnityEvent _clearIronEvent;

    private TimerInfo timerFire;
    private TimerInfo timerCreateIron;
    private TimerInfo timerClearBreackIron;

    public void Initialize()
    {
        if (_spinner != null)
        {
            _spinner.Initialize();
        }
        else
        {
            //Debug.LogError($"Not Found MySpinner - {gameObject.name}");
        }
        timerCreateIron = new TimerInfo() { Color = new Color(0.5f, 1, 0.5f, 1), StartTime = _timeCreateIron };
        timerFire = new TimerInfo() { Color = new Color(1, 0, 0, 1), StartTime = _timeFire };
        timerClearBreackIron = new TimerInfo() { Color = new Color(1, 0.8f,0, 1), StartTime = _timeClearBreack };
        _initializeEvent?.Invoke();
    }

    private void OnDisable()
    {
        if (_spinner != null)
        {
        }
    }

    public void Play()
    {
        IsFree = false;
        if (_spinner != null)
        {
            StartEvent();
            _spinner.Play(timerCreateIron, EndEvent);
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
            _spinner.Play(timerClearBreackIron, Clear);
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
            _spinner.Play(timerFire, EndFireEvent);
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

    //private void OnValidate()
    //{
    //    if (_spinner == null)
    //    {
    //        _spinner = GetComponentInChildren<MySpinner>(true);
    //    };
    //}
}

public struct TimerInfo
{
    public Color Color;
    public float StartTime;
}

