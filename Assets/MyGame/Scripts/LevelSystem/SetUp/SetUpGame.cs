using UnityEngine;
using UnityEngine.UI;

public class SetUpGame : MonoBehaviour
{
    [SerializeField] private AnvilManager _anvilManager;
    [SerializeField] private PinManager _pinManager;
    [SerializeField] private SwordManager _swordManager;
    [SerializeField] private MannequinManager _manikenManager;
    [SerializeField] private OrderManager _orderManager;
    [SerializeField] private GoalManager _goalManager;
    [SerializeField] private InstrumentManager _instrumentManager;
    [SerializeField] private GoldManager _goldManager;
    [SerializeField] private Button Button;
    private LevelSettings levelSettings;

    public void Initialize()
    {
        levelSettings = GameObject.FindFirstObjectByType<LevelSettings>();
        if (levelSettings == null) return;
        Button.onClick.AddListener(() => GameManager.instance.LoadMenu());
        SetUp(levelSettings.LevelHard);
        if (!CheckTutorial())
        {
            _orderManager.StartWork();
        }
    }

    private void SetUp(LevelHard levelHard)
    {
        if (levelHard == LevelHard.none) return;
        _goalManager.Initialize(levelSettings.GetTaskManuals());
        _orderManager.Initialize(levelHard, _goalManager);
        _anvilManager.Initialize(levelSettings.infoAnvilManager);
        _pinManager.Initialize(levelSettings.infoPinManager);
        _swordManager.Initialize(levelSettings.infoSwordManager);
        _manikenManager.Initialize(levelSettings.infoArmorManager);
        _instrumentManager.Initialize();
        _goldManager.Initialize(levelSettings);
    }

    private bool CheckTutorial()
    {
        bool _isTutorial = false;
        switch (levelSettings.Level)
        {
            case 1:
                //    1) гвозди 
                //1 ур (Туториал и игровой процесс)
                //_isTutorial = true;
                //Debug.Log("Туториал запущен");
                //orderManager.StartWork();
                // Метод вызова катсцены
                break;

            case 2:
                //2) гвозди + мечи 
                //2 - 3 ур 
                //_isTutorial = true;

                // Метод вызова катсцены
                break;

            case 4:
                //3) гвозди + мечи + кожаный доспех
                //4 - 6 ур 
                //_isTutorial = true;

                // Метод вызова катсцены
                break;

            case 7:
                //4) гвозди + мечи + кожаный доспех + металлический доспех 
                //7 - 10 ур
                //_isTutorial = true;

                // Метод вызова катсцены
                break;

            case 10:
                //5) гвозди + мечи + кожаный доспех + металлический доспех  + полный меч
                //+10 ур
                //_isTutorial = true;

                // Метод вызова катсцены
                break;
        }
        // Уже в самой катсцене мы вызовем переход к игре с помощью метода orderManager.StartWork();
        return _isTutorial;
    }
}