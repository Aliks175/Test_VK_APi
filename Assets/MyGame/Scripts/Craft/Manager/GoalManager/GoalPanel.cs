using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalPanel : MonoBehaviour
{
    [SerializeField] private GameObject _successIcon;
    [SerializeField] private Image _iconGoal;
    [SerializeField] private Slider _trackerGoal;
    [SerializeField] private TextMeshProUGUI _valueGoal;
    public bool IsActive => gameObject.activeSelf;
    public bool IsComplite => _iscomplite;
    public TaskType TaskType => _taskProgressionInfo.TaskType;
    public event Action<TaskType> Oncomplite;

    private TaskProgressionInfo _taskProgressionInfo;
    private SpriteManagerGoal _spriteManagerGoal;
    private bool _iscomplite;

    public void Initialize(TaskProgressionInfo taskProgressionInfo, SpriteManagerGoal spriteManagerGoal)
    {
        _spriteManagerGoal = spriteManagerGoal;
        _successIcon.SetActive(false);
        gameObject.SetActive(true);
        _taskProgressionInfo = taskProgressionInfo;
        _iscomplite = false;
        SetGoal();
    }

    /// <summary>
    /// Выполнение задания для Закрытия заказа, и быстрого Закрытия заказа
    /// </summary>
    public void CompletingGoals()
    {
        if (_iscomplite) return;
        if (TaskType == TaskType.CloseOrder || TaskType == TaskType.CloseOrderForTime)
        {
            if (_trackerGoal.value + 1 < _trackerGoal.maxValue)
            {
                _trackerGoal.value++;
                Debug.Log("CompletingGoals");
                ViewUpdate();
            }
            else
            {
                GoalSuccess();
            }
        }
    }

    /// <summary>
    /// Выполнение задания для ограничения времени
    /// </summary>
    /// <param name="time"></param>
    public void CompletingGoals(float time)
    {
        // Это отображение оставшегося времени
        // Мы можем просто вывести числовое значение без слайдера

        if (TaskType != TaskType.TimeLimit || _iscomplite) return;

        _trackerGoal.value = time;
        ViewUpdate();

        if (time <= 0)
        {
            GoalSuccess();
        }
    }

    /// <summary>
    /// Выполнение задания для Заработка монет
    /// </summary>
    /// <param name="ValueGold"></param>
    public void CompletingGoals(int ValueGold)
    {
        // Это отображение количества монет 
        if (TaskType != TaskType.MoneyGoal || _iscomplite) return;


        Debug.Log($"_trackerGoal.value = {_trackerGoal.value}");
        Debug.Log($"ValueGold = {ValueGold}");
        Debug.Log($"_trackerGoal.maxValue = {_trackerGoal.maxValue}");
        Debug.Log($"_trackerGoal.value + ValueGold < _trackerGoal.maxValue = {ValueGold < _trackerGoal.maxValue}");
        if (ValueGold < _trackerGoal.maxValue)
        {
            _trackerGoal.value = ValueGold;
            ViewUpdate();
        }
        else
        {
            GoalSuccess();
        }
    }

    private void GoalSuccess()
    {
        Debug.Log("Заказ выполнен");
        if (TaskType == TaskType.CloseOrderForTime|| TaskType == TaskType.MoneyGoal)
        {
        _successIcon.SetActive(true);
        _trackerGoal.value = _trackerGoal.maxValue;
        }
        _iscomplite = true;
        Oncomplite?.Invoke(TaskType);
        // Можно вывести галочку на месте задания , это доходчиво объяснит что задание выполнено 
    }

    private void ViewUpdate()
    {
        if (!_iscomplite)
        {
            _valueGoal.text = $"{_trackerGoal.value}/{_trackerGoal.maxValue}";
        }
        else
        {
            _successIcon.SetActive(true);
        }
    }

    private void SetGoal()
    {
        switch (_taskProgressionInfo.TaskType)
        {
            case TaskType.None:
                break;
            case TaskType.MoneyGoal:
                _iconGoal.sprite = _spriteManagerGoal.GetSprite(_taskProgressionInfo.TaskType);
                _trackerGoal.wholeNumbers = true;
                _trackerGoal.maxValue = _taskProgressionInfo.Value;
                _trackerGoal.value = 0f;
                _valueGoal.text = $"{_trackerGoal.value}/{_trackerGoal.maxValue}";
                break;
            case TaskType.CloseOrder:
                _iconGoal.sprite = _spriteManagerGoal.GetSprite(_taskProgressionInfo.TaskType);
                _trackerGoal.wholeNumbers = true;
                _trackerGoal.maxValue = _taskProgressionInfo.Value;
                _trackerGoal.value = 0f;
                _valueGoal.text = $"{_trackerGoal.value}/{_trackerGoal.maxValue}";
                break;
            case TaskType.CloseOrderForTime:
                _iconGoal.sprite = _spriteManagerGoal.GetSprite(_taskProgressionInfo.TaskType);
                _trackerGoal.wholeNumbers = true;
                _trackerGoal.maxValue = _taskProgressionInfo.Value;
                _trackerGoal.value = 0f;
                _valueGoal.text = $"{_trackerGoal.value}/{_trackerGoal.maxValue}";
                break;
            case TaskType.TimeLimit:
                _iconGoal.sprite = _spriteManagerGoal.GetSprite(_taskProgressionInfo.TaskType);
                _valueGoal.text = _taskProgressionInfo.Value.ToString();
                _trackerGoal.maxValue = _taskProgressionInfo.Value;
                _trackerGoal.value = _trackerGoal.maxValue;
                _valueGoal.text = $"{_trackerGoal.maxValue}";
                break;
            default:
                break;
        }
    }
}
