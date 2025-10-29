using UnityEngine;

[CreateAssetMenu(fileName = "LevelProgression", menuName = "Data/LevelProgression")]
public class LevelProgressionData : ScriptableObject
{
    [Header("Task")]
    public TaskProgression MoneyGoal;
    public TaskProgression CloseOrder;
    public TaskProgression CloseOrderForTime;
    public TaskProgression TimeLimit;
    [Header("ValueTask")]
    public AnimationCurve ValueTask;
}
