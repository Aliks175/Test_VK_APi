using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private ViewGoal _viewTask;
    
    private TaskProgression _taskProgression;

    public void Initialize(TaskProgression taskProgression)
    {
        // Мы получаем задания которые будут активны на этом уровнеъ
        // мы инициируем класс для отображения

        _taskProgression = taskProgression;

        _viewTask.Initialize(_taskProgression);
    }

    public void CloseOrder(TaskInfo taskInfo)
    {
        GoalPanel goalPanel = null;
        // Здесь задание срабатывает : 
        // обслужить столько то гостей 
        // обслужить гостей когда у них больше половины терпения 
        //  он будет вызываться из класса отслежующего Заказы при их выполнении 
        Debug.Log("Use CloseOrder");
        if (taskInfo.IsFastOrder)
        {
            goalPanel = _viewTask.GetGoal(TaskType.CloseOrderForTime);
        }
        if (goalPanel != null)
        {
            goalPanel.CompletingGoals();
        }

        goalPanel = _viewTask.GetGoal(TaskType.CloseOrder);
        if (goalPanel != null)
        {
            Debug.Log("Use _viewTask.GetGoal(TaskType.CloseOrder);");
            goalPanel.CompletingGoals();
        }
    }

    public void AddGold(int valueGold)
    {
        //  заработать столько то денег за игру
        //  он будет вызываться из класса отслежующего количество заработаных денег 
        GoalPanel goalPanel = _viewTask.GetGoal(TaskType.MoneyGoal);
        if (goalPanel != null)
        {
            goalPanel.CompletingGoals(valueGold);
        }
    }

    public void TrackTime(float time)
    {
        //  отработать столько то времени
        //  он будет вызываться из спец класса отвечающего за время длительности раунда 

        GoalPanel goalPanel = _viewTask.GetGoal(TaskType.TimeLimit);
        if (goalPanel != null)
        {
            goalPanel.CompletingGoals(time);
        }
    }
}

public struct TaskInfo
{
    public bool IsFastOrder;
}

//1) заработать столько то денег за игру 
//2) обслужить столько то гостей 
//3) обслужить гостей когда у них больше половины терпения 
//4) отработать столько то времени

//None,
//MoneyGoal,
//CloseOrder,
//CloseOrderForTime,
//TimeLimit
public enum TaskType
{
    //    1) заработать столько то денег за игру 
    //2) обслужить столько то гостей 
    //3) обслужить гостей когда у них больше половины терпения 
    //4) отработать столько то времени

    None,
    MoneyGoal,
    CloseOrder,
    CloseOrderForTime,
    TimeLimit
}