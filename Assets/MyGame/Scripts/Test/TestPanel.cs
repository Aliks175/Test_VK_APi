using TMPro;
using UnityEngine;

public class TestPanel : MonoBehaviour
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