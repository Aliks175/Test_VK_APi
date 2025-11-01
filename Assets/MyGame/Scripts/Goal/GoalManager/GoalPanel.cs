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
    private bool _iscomplite;

    public TaskType taskType => _taskProgressionInfo.TaskType;

    private TaskProgressionInfo _taskProgressionInfo;
    private SpriteManagerGoal _spriteManagerGoal;

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
        if (taskType == TaskType.CloseOrder || taskType == TaskType.CloseOrderForTime)
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

        if (taskType != TaskType.TimeLimit || _iscomplite) return;

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
    /// <param name="Value"></param>
    public void CompletingGoals(int Value)
    {
        // Это отображение количества монет 
        if (taskType != TaskType.MoneyGoal || _iscomplite) return;

        if (_trackerGoal.value + Value < _trackerGoal.maxValue)
        {
            _trackerGoal.value += Value;
            ViewUpdate();
        }
        else
        {
            GoalSuccess();
        }
    }

    private void GoalSuccess()
    {
        _trackerGoal.value = _trackerGoal.maxValue;
        Debug.Log("Заказ выполнен");
        _successIcon.SetActive(true);
        _iscomplite = true;
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
                _valueGoal.text = $"{_trackerGoal.value}/{_trackerGoal.maxValue}";
                break;
            default:
                break;
        }
    }
}
