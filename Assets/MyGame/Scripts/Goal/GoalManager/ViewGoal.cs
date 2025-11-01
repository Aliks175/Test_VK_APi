using System.Collections.Generic;
using UnityEngine;

public class ViewGoal : MonoBehaviour
{
    [SerializeField] private SpriteManagerGoal _spriteManagerGoal;
    private List<GoalPanel> _goalPanels;
    //[SerializeField] private GameObject MoneyGoal;
    //[SerializeField] private GameObject CloseOrder;
    //[SerializeField] private GameObject CloseOrderForTime;
    //[SerializeField] private GameObject TimeLimit;


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
            templist.Add(_goalPanels[i]);
        }
        _goalPanels = templist;
    }


    public GoalPanel GetGoal(TaskType taskType)
    {
        GoalPanel goalPanel = null;
        foreach (var item in _goalPanels)
        {
            if (item.taskType == taskType)
            {
                goalPanel = item;
            }
        }
        return goalPanel;
    }
}