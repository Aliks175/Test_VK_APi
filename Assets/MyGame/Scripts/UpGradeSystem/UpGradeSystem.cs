using TMPro;
using UnityEngine;

public class UpGradeSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textLevel;
    private LevelSettings _levelSettings;

    public void Initialize(LevelSettings levelSettings)
    {
        _levelSettings = levelSettings;
        _textLevel.text = $"Level : {_levelSettings.Level}";
    }

    public void UpNumberLevel()
    {
        _levelSettings.Level++;
        _levelSettings.ScaleHardLevel();
        _textLevel.text = $"Level : {_levelSettings.Level}";
    }

    public void DownNumberLevel()
    {
        if (_levelSettings.Level > 1)
        {
            _levelSettings.Level--;
            _textLevel.text = $"Level : {_levelSettings.Level}";
            _levelSettings.ScaleHardLevel();
        }
    }

    public void UpLevelAnvil()
    {
        if (_levelSettings.ValueAnvil == 3) return;
        _levelSettings.ValueAnvil++;
        _levelSettings.WaitCreateIron -= 1f;
        _levelSettings.UpdateChangers(TypeChangers.Anvil);
    }

    public void UpLevelPin()
    {
        if (_levelSettings.ValuePinSlot == 6) return;
        _levelSettings.ValuePinSlot += 2;
        _levelSettings.WaitCreatePin -= 1f;
        _levelSettings.UpdateChangers(TypeChangers.Pin);
    }

    public void UpLevelSword()
    {
        if (_levelSettings.ValueSword == 0) return;
        if (_levelSettings.ValueSword == 3) return;
        _levelSettings.ValueSword++;
        _levelSettings.UpdateChangers(TypeChangers.Sword);
    }

    public void UpLevelArmor()
    {
        if (_levelSettings.ValueArmor == 0) return;
        if (_levelSettings.ValueArmor == 3) return;
        _levelSettings.ValueArmor++;
        _levelSettings.WaitCreateeLeatherArmor -= 1f;
        _levelSettings.WaitCreateMetallArmor -= 1f;
        _levelSettings.UpdateChangers(TypeChangers.Armor);
    }

    public void ClearUpLevel()
    {
        _levelSettings.ValueAnvil = 1;
        _levelSettings.WaitCreateIron = 4;
        _levelSettings.ValuePinSlot = 2;
        _levelSettings.WaitCreatePin = 3;
        _levelSettings.ValueSword = 0;
        _levelSettings.ValueArmor = 0;
        _levelSettings.WaitCreateeLeatherArmor = 4f;
        _levelSettings.WaitCreateMetallArmor = 6f;
        _levelSettings.UpdateChangers(TypeChangers.Armor);
        _levelSettings.UpdateChangers(TypeChangers.Sword);
        _levelSettings.UpdateChangers(TypeChangers.Pin);
        _levelSettings.UpdateChangers(TypeChangers.Anvil);
    }
}