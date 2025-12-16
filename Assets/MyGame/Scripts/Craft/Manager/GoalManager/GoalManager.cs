using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private ViewGoal _viewTask;

    private OrderManager _orderManager;
    private TaskProgression _taskProgression;

    private void OnDisable()
    {
        _viewTask.Oncomplite -= CompliteGoal;
    }

    public void Initialize(TaskProgression taskProgression, OrderManager orderManager)
    {
        // Мы получаем задания которые будут активны на этом уровнеъ
        // мы инициируем класс для отображения
        _orderManager = orderManager;
        _taskProgression = taskProgression;
        _viewTask.Initialize(_taskProgression);
        _viewTask.Oncomplite += CompliteGoal;
    }

    // Пределать класс здесь должно быть 2 класса 1 на выполнение заказа 2 на провал 
    public void CloseOrder(TaskInfo taskInfo)
    {
        GoalPanel goalPanel = null;
        // Здесь задание срабатывает : 
        // обслужить столько то гостей 
        // обслужить гостей когда у них больше половины терпения 
        //  он будет вызываться из класса отслежующего Заказы при их выполнении 
        Debug.Log("Use CloseOrder");

        if (!taskInfo.isComplite)
        {
            goalPanel = _viewTask.GetGoal(TaskType.CloseOrder);
            if (goalPanel != null)
            {
                Debug.Log("No Complite Order");
                goalPanel.CompletingGoals();
            }
            return;
        }

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

    public bool CheckResultGame()
    {
        bool result = false;

        GoalPanel resultGoalOne = _viewTask.GetGoal(TaskType.MoneyGoal);
        GoalPanel resultGoalTwo = _viewTask.GetGoal(TaskType.CloseOrderForTime);

        if (resultGoalOne != null && resultGoalTwo != null)
        {
            result = resultGoalOne.IsComplite == true && resultGoalOne.IsComplite == true;
            return result;
        }

        if (resultGoalOne != null)
        {
            result = resultGoalOne.IsComplite;
        }

        if (resultGoalTwo != null)
        {
            result = resultGoalTwo.IsComplite;
        }
        return result;
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

    private void CompliteGoal(TaskType taskType)
    {
        switch (taskType)
        {
            case TaskType.TimeLimit:
            case TaskType.CloseOrder:

                _orderManager.StopWork();
                break;
        }
    }

}

public struct TaskInfo
{
    public bool IsFastOrder;
    public bool isComplite;
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
