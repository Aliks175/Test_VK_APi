using UnityEngine;

public class GoldManager : MonoBehaviour
{
    private GoalManager _goalManager;
    private int _gold;
    private LevelSettings _levelSettings;
    private GoldInfo _goldInfo;

    public void Initialize(LevelSettings levelSettings, GoalManager goalManager)
    {
        _levelSettings = levelSettings;
        _goalManager = goalManager;
        _goldInfo = new GoldInfo()
        {
            GoldArmor = _levelSettings.GoldArmor,
            GoldPin = _levelSettings.GoldPin,
            GoldSword = _levelSettings.GoldSword,
        };
    }

    public void SetGold(OrderItem orderItem)
    {
        switch (orderItem)
        {
            case OrderItem.Pin:
                _gold += _goldInfo.GoldPin;
                break;
            case OrderItem.Sword:
            case OrderItem.SwordLezz:
            case OrderItem.SwordHand:
            case OrderItem.SwordFull:
                _gold += _goldInfo.GoldSword;
                break;
            case OrderItem.ArmorLeather:
            case OrderItem.ArmorMetall:
                _gold += _goldInfo.GoldArmor;
                break;
            default:
                break;
        }
        Debug.Log($"Gold = {_gold}");
        _goalManager.AddGold(_gold);
    }
}

public struct GoldInfo
{
    public int GoldPin;
    public int GoldSword;
    public int GoldArmor;
}