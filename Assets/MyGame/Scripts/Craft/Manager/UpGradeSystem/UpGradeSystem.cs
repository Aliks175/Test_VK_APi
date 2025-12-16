using UnityEngine;

public class UpGradeSystem : MonoBehaviour
{
    private LevelSettings _levelSettings;

    public void Initialize(LevelSettings levelSettings)
    {
        _levelSettings = levelSettings;
    }


    public void UpLevelAnvil()
    {
        if (_levelSettings.ValueAnvil == 3) return;
        _levelSettings.ValueAnvil++;
        _levelSettings.WaitCreateIron -= 1f;
        _levelSettings.UpdateChangers(TypeChangers.Anvil);

    }

    public void UpLevelPin(bool isgold)
    {
        if (isgold)
        {
            if (_levelSettings.GoldPin == 6) return;
            _levelSettings.GoldPin += 1;
        }
        else
        {
            if (_levelSettings.ValuePinSlot == 6) return;
            _levelSettings.ValuePinSlot += 2;
            _levelSettings.WaitCreatePin -= 1f;
            _levelSettings.UpdateChangers(TypeChangers.Pin);
        }
    }

    public void UpLevelSword(bool isgold)
    {
        if (_levelSettings.ValueSword == 0) return;

        if (isgold)
        {
            if (_levelSettings.GoldSword == 10) return;
            _levelSettings.GoldSword += 2;
        }
        else
        {
            if (_levelSettings.ValueSword == 3) return;
            _levelSettings.ValueSword++;
            _levelSettings.UpdateChangers(TypeChangers.Sword);
        }
    }

    public void UpLevelArmor(bool isgold)
    {
        if (_levelSettings.ValueArmor == 0) return;

        if (isgold)
        {
            if (_levelSettings.GoldArmor == 8) return;
            _levelSettings.GoldArmor += 1;
        }
        else
        {
            if (_levelSettings.ValueArmor == 3) return;
            _levelSettings.ValueArmor++;
            _levelSettings.WaitCreateeLeatherArmor -= 1f;
            _levelSettings.WaitCreateMetallArmor -= 1f;
            _levelSettings.UpdateChangers(TypeChangers.Armor);
        }
    }
}