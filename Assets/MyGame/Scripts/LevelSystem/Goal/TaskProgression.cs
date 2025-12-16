[System.Serializable]
public class TaskProgression
{
    public string Level = "Level";
    public TaskProgressionInfo[] TaskManuals;
}

[System.Serializable]
public struct TaskProgressionInfo
{
    public TaskType TaskType;
    public int Value;
}