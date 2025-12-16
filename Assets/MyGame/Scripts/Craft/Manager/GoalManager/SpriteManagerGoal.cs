using UnityEngine;

public class SpriteManagerGoal : MonoBehaviour
{
    [SerializeField] private Sprite _success;
    [SerializeField] private Sprite _order;
    [SerializeField] private Sprite _fastOrder;
    [SerializeField] private Sprite _gold;
    [SerializeField] private Sprite _time;

    public Sprite GetSprite(TaskType orderItem)
    {
        Sprite sprite = null;
        switch (orderItem)
        {
            case TaskType.None:
                break;
            case TaskType.MoneyGoal:
                sprite = _gold;
                break;
            case TaskType.CloseOrder:
                sprite = _order;
                break;
            case TaskType.CloseOrderForTime:
                sprite = _fastOrder;
                break;
            case TaskType.TimeLimit:
                sprite = _time;
                break;
            default:
                break;
        }
        return sprite;
    }
}