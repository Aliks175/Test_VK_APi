using UnityEngine;

[CreateAssetMenu(fileName = "LevelProgression", menuName = "Data/LevelProgression")]
public class LevelProgressionData : ScriptableObject
{
    [Header("Кривая роста сложности")]
    public AnimationCurve difficultyCurve = AnimationCurve.Linear(0, 1, 10, 3f);
    [Header("Максимальный множитель сложности")]
    public float maxMultiplier = 10f;
    public TaskProgression[] TaskManual;

    public TaskProgression GetTask(int level)
    {
        if (TaskManual == null || TaskManual.Length == 0)
        {
            Debug.LogError("TaskManual пуст! Добавь хотя бы один уровень.");
            return null;
        }

        int baseIndex = (level - 1) % TaskManual.Length; // индекс текущего уровня 
        int cycleNumber = (level - 1) / TaskManual.Length;// номер текущего цыкла если у нас 10 уровней а игрок попал на 11 то у нас 1 уровень 2 цыкла 

        float curveValue = difficultyCurve.Evaluate(cycleNumber);
        float multiplier = Mathf.Clamp(curveValue, 1f, maxMultiplier);
        TaskProgression baseProgression = TaskManual[baseIndex];

        // создаём копию, чтобы не менять оригинал
        TaskProgression temp = new TaskProgression
        {
            Level = baseProgression.Level,
            TaskManuals = new TaskProgressionInfo[baseProgression.TaskManuals.Length]
        };
        // копируем с увеличением значений
        for (int i = 0; i < baseProgression.TaskManuals.Length; i++)
        {
            temp.TaskManuals[i] = new TaskProgressionInfo
            {
                TaskType = baseProgression.TaskManuals[i].TaskType,
                Value = Mathf.RoundToInt(baseProgression.TaskManuals[i].Value * multiplier)
            };
        }
        return temp;
    }
}