using System;
using System.Collections.Generic;
using UnityEngine;

public class ViewGoal : MonoBehaviour
{
    [SerializeField] private SpriteManagerGoal _spriteManagerGoal;
    private Dictionary<TaskType, GoalPanel> GoalPanels = new Dictionary<TaskType, GoalPanel>();
    private List<GoalPanel> _goalPanels;
    public event Action<TaskType> Oncomplite;

    private void OnDisable()
    {
        if (_goalPanels != null)
            for (int i = 0; i < _goalPanels.Count; i++)
            {
                _goalPanels[i].Oncomplite -= CompliteGoal;
            }
    }

    public void Initialize(TaskProgression infoSwordManager)
    {
        // мы получаем все панели заданий
        // Включаем только нужные 
        // у нас создается список панелей который будет использоваться во время игры
        // мы заполняем в нем значения
        // подписываемся на событие его изменения для обнавления Ui  

        if (infoSwordManager.TaskManuals.Length == 0) return;
        _goalPanels = new List<GoalPanel>(gameObject.GetComponentsInChildren<GoalPanel>(true));
        int count = infoSwordManager.TaskManuals.Length;

        List<GoalPanel> templist = new();
        for (int i = 0; i < count; i++)
        {
            //Debug.Log($"Count = {count} ||| i = {i}");
            //Debug.Log($"infoSwordManager.TaskManuals = {infoSwordManager.TaskManuals.Length}");
            _goalPanels[i].Initialize(infoSwordManager.TaskManuals[i], _spriteManagerGoal);
            _goalPanels[i].Oncomplite += CompliteGoal;
            GoalPanels.Add(_goalPanels[i].TaskType, _goalPanels[i]);
            templist.Add(_goalPanels[i]);
        }
        _goalPanels = templist;
    }

    private void CompliteGoal(TaskType taskType)
    {
        Oncomplite?.Invoke(taskType);
    }


    public GoalPanel GetGoal(TaskType taskType)
    {
        GoalPanels.TryGetValue(taskType, out var panel);

        //GoalPanel goalPanel = null;
        //foreach (var item in _goalPanels)
        //{
        //    if (item.TaskType == taskType)
        //    {
        //        goalPanel = item;
        //    }
        //}
        return panel;
    }


}