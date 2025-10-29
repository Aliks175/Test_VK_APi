using System.Threading.Tasks;
using UnityEngine;

public class TaskGenerator : MonoBehaviour
{
    [SerializeField] private LevelProgressionData levelProgressionData;

    public void Initialize()
    {
       
    }

    public void GetTask(int level)
    {
        //return new LevelTask
        //{
        //    MoneyGoal = Mathf.RoundToInt(levelProgressionData.MoneyGoal.GetValueForLevel(level)),
        //    TimeLimit = levelProgressionData.TimeLimit.GetValueForLevel(level),
        //    CloseOrder = Mathf.RoundToInt(levelProgressionData.CloseOrder.GetValueForLevel(level)),
        //    CloseOrderForTime = Mathf.RoundToInt(levelProgressionData.CloseOrderForTime.GetValueForLevel(level)),
        //    ValuePlayerTask = Mathf.RoundToInt(levelProgressionData.ValueTask.Evaluate(level)),
        //};


       int valuePlayerTask  = Mathf.RoundToInt(levelProgressionData.ValueTask.Evaluate(level));

            float chance = levelProgressionData.ValueTask.Evaluate(level);
            float random = Random.value;
            int count = 1;

            if (random < chance)
                count++;
            if (Random.value < chance * 0.5f)
                count++;
    }
}

//public struct LevelTask
//{
//    public int MoneyGoal;
//    public int CloseOrder;
//    public int CloseOrderForTime;
//    public float TimeLimit;
//    public int ValuePlayerTask;
//}

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

public struct ListTask
{
    public TaskType OneTask;
    public float ValueOneTask;
    public TaskType TwoTask;
    public float ValueTwoTask;
    public TaskType ThreeTask;
    public float ValueThreeTask;
}