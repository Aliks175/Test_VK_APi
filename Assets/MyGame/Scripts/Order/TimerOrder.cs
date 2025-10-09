using System;
using UnityEngine;
using UnityEngine.UI;
using static System.Collections.Specialized.BitVector32;

public class TimerOrder : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _timeWait = 10f;
    [SerializeField] private float _timeGoodJob = 10f;

    private TimerInfo TimerWait;
    private TimerInfo TimerGoodJob;

    private Action _action;
    private bool _isPlay = false;
    private float _startTime = 0;
    private float _lostTime = 0;

    public void Initialize()
    {
        TimerWait = new TimerInfo() { Color = new Color(0.5f, 1, 0.5f, 1), StartTime = _timeWait };
        TimerGoodJob = new TimerInfo() { Color = new Color(1, 0, 0, 1), StartTime = _timeGoodJob };
    }

    private void Update()
    {
        Wait();
    }

    private void Wait() // Собрать все таймеры и запускать через единый Update
    {
        if (_isPlay)
        {
            if (_action == null) return;

            //Debug.Log($"Time - {Time.time}   _lostTime - {_lostTime} ");
            //Debug.Log($"Time - {(_speed * _lostTime) % _startTime} ");

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
        _lostTime = _timeWait;
    }

    public void Play( Action action)
    {
        if (!_isPlay)
        {
            _action = action;
            _startTime = _timeWait;
            _lostTime = _startTime;
            _isPlay = true;
        }
    }

    public void Stop()
    {
        _lostTime = _timeWait;
        _isPlay = false;
    }

}
