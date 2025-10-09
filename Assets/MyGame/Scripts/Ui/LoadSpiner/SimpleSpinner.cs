
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.SimpleSpinner
{
    [RequireComponent(typeof(Image))]
    public class SimpleSpinner : MonoBehaviour
    {
        [SerializeField] private float _startTime;
        [SerializeField] private bool _isPlay;
        private float _speed = 1;
        private float _lostTime = 0;
        private Vector3 EulerAngles = Vector3.zero;
        public UnityEvent _startEvent;
        public UnityEvent _endEvent;
        private Color _colorView = new Color(1, 1, 1, 1);
        private Color _colorNoView = new Color(1, 1, 1, 0);

        // Мои 

        [Space(40)]
        [Header("Rotation")]
        public bool Rotation = true;
        [Range(-10, 10), Tooltip("Value in Hz (revolutions per second).")]
        public float RotationSpeed = 1;
        public AnimationCurve RotationAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [Header("Rainbow")]
        public bool Rainbow = true;
        [Range(-10, 10), Tooltip("Value in Hz (revolutions per second).")]
        public float RainbowSpeed = 0.5f;
        [Range(0, 1)]
        public float RainbowSaturation = 1f;
        public AnimationCurve RainbowAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [Header("Options")]
        public bool RandomPeriod = true;

        private Image _image;
        private float _period;

        public void Start()
        {
            _image = GetComponent<Image>();
            _period = RandomPeriod ? Random.Range(0f, 1f) : 0;

        }

        public void Update()
        {
            Debug.Log($"Time - {Time.time}   _lostTime - {_lostTime} "); // Мое 
            Debug.Log($"Time - {(_speed * _lostTime) % _startTime} "); // Мое 


            //if (Rotation)
            //{
            //    transform.localEulerAngles = new Vector3(0, 0, -360 * RotationAnimationCurve.Evaluate((RotationSpeed * Time.time + _period) % 1));
            //}

            //if (Rainbow)
            //{
            //    _image.color = Color.HSVToRGB(RainbowAnimationCurve.Evaluate((RainbowSpeed * Time.time + _period) % 1), RainbowSaturation, 1);
            //}

            if (_isPlay)// Мое
            {
                if (_lostTime <= _startTime)
                {
                    EulerAngles.z = -360 * RotationAnimationCurve.Evaluate((_speed * _lostTime / _startTime));
                    transform.localEulerAngles = EulerAngles;

                    _lostTime += Time.deltaTime;
                }
                else
                {
                    _lostTime = 0f;
                    _isPlay = false;
                    _image.color = _colorNoView;
                    _endEvent?.Invoke();
                }
            }
        }

        public void Play()
        {
            _startEvent?.Invoke();
            _lostTime = 0f;
            _image.color = _colorView;
            _isPlay = true;
        }
    }
}