using UnityEngine;
using UnityEngine.UI;

public class SetUpMenu : MonoBehaviour
{
    [SerializeField] private UpGradeSystem upGradeSystem;
    [SerializeField] private TestPanel _testPanel;
    [SerializeField] private Button Button;

    // Мы инициируем все что есть по сохранениям тоесть нам нужно запросить сохранения из вк
    // если их нет то это новая игра
    // и мы инициируем баланс денег
    // текущий уровень уровни
    // 
    // Мы должны сохранять количество денег игрока
    // мы сохраняем последний пройденый уровень
    // мы сохраняем улучшения игрока

    public void Initialize()
    {
        LevelSettings levelSettings = GameObject.FindFirstObjectByType<LevelSettings>();
        if (levelSettings == null) return;

        //taskGenerator.Initialize(levelSettings);
        upGradeSystem.Initialize(levelSettings);
        _testPanel.Initialize(levelSettings);
        Button.onClick.AddListener(() => GameManager.instance.LoadGame());
    }
}