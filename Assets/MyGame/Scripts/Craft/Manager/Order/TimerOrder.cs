using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerOrder : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private TimerInfo TimerWait;
    //private TimerInfo TimerGoodJob;
    private Action _action;
    private float _startTime = 0;
    private float _lostTime = 0;
    private bool _isPlay = false;

    public void Initialize(float _timeWait)
    {
        TimerWait = new TimerInfo() { Color = new Color(0.5f, 1, 0.5f, 1), StartTime = _timeWait };
        //TimerGoodJob = new TimerInfo() { Color = new Color(1, 0, 0, 1), StartTime = _timeWait / 2 };
    }

    private void Update()
    {
        Wait();
    }

    private void Wait()
    {
        if (_isPlay)
        {
            if (_action == null) return;
            if (_lostTime > 0)
            {
                var gf = _lostTime / _startTime;
                _slider.value = gf * _slider.maxValue;
                _lostTime -= Time.deltaTime;
            }
            else
            {
                Stop();
                _action?.Invoke();
            }
        }
    }

    public void AddTimeWait()
    {
        _lostTime = TimerWait.StartTime;
    }

    public void Play(Action action)
    {
        if (!_isPlay)
        {
            _action = action;
            _startTime = TimerWait.StartTime; ;
            _lostTime = _startTime;
            _isPlay = true;
        }
    }

    public bool CheckFastOrder()
    {
        Debug.Log($"_lostTime = {_lostTime} ||| _startTime = {_startTime}  |||_lostTime >= _startTime / 2 = {_lostTime >= _startTime / 2}");
        return _lostTime >= _startTime / 2;
    }

    public void Stop()
    {
        _lostTime = TimerWait.StartTime; 
        _isPlay = false;
    }
}