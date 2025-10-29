using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MySpinner : MonoBehaviour
{
    [SerializeField] private AnimationCurve RotationAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);
    private bool _isPlay = false;
    private float _speed = 1;
    private float _startTime = 0;
    private float _lostTime = 0;
    private Vector3 EulerAngles = Vector3.zero;
    private Color _colorNoView = new(1, 1, 1, 0);
    private Image _image;
    private Action _action;

    public void Initialize()
    {
        _image = GetComponent<Image>();
        _speed = 1;
        _lostTime = 0f;
        _startTime = 0;
        _isPlay = false;
        _image.color = _colorNoView;
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
            if (_lostTime <= _startTime)
            {
                EulerAngles.z = -360 * RotationAnimationCurve.Evaluate((_speed * _lostTime / _startTime));
                transform.localEulerAngles = EulerAngles;
                _lostTime += Time.deltaTime;
            }
            else
            {
                Stop();
                _action?.Invoke();
            }
        }
    }

    public void Play(TimerInfo timerInfo, Action action)
    {
        if (!_isPlay)
        {
            _action = action;
            _lostTime = 0f;
            _image.color = timerInfo.Color;
            _startTime = timerInfo.StartTime;
            _isPlay = true;
        }
    }

    public void Stop()
    {
        _lostTime = 0f;
        _isPlay = false;
        _image.color = _colorNoView;
    }
}