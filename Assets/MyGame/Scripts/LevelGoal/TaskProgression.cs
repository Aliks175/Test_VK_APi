
using UnityEngine;

[System.Serializable]
public class TaskProgression
{
    public TaskType TaskName;
    public AnimationCurve DifficultyCurve;
    public float BaseValue;
    public float Multiplier = 1f;

    public float GetValueForLevel(int level)
    {
        return BaseValue + DifficultyCurve.Evaluate(level) * Multiplier;
    }
}