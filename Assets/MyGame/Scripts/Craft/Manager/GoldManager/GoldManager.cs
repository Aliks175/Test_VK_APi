using UnityEngine;

public class GoldManager : MonoBehaviour
{
    private LevelSettings _levelSettings;
    private GoldInfo _goldInfo;

    public void Initialize( LevelSettings levelSettings)
    {
        _levelSettings = levelSettings;
        _goldInfo = new GoldInfo()
        {
            GoldArmor = _levelSettings.GoldArmor,
            GoldPin = _levelSettings.GoldPin,
            GoldSword = _levelSettings.GoldSword,
        };
    }
}

public struct GoldInfo
{
    public int GoldPin;
    public int GoldSword;
    public int GoldArmor;
}