using UnityEngine;
using UnityEngine.UI;

public class SetUpGame : MonoBehaviour
{
    [Header("ManagerOrder")]
    [SerializeField] private AnvilManager _anvilManager;
    [SerializeField] private PinManager _pinManager;
    [SerializeField] private SwordManager _swordManager;
    [SerializeField] private MannequinManager _manikenManager;
    [SerializeField] private OrderManager _orderManager;
    [SerializeField] private InstrumentManager _instrumentManager;
    [Header("ManagerGoal")]
    [SerializeField] private GoalManager _goalManager;
    [SerializeField] private GoldManager _goldManager;
    [SerializeField] private ResultManager _resultManager;
    [SerializeField] private ButtonControl _buttonControl;
    [SerializeField] private TimeManager _timeManager;

    private LevelSettings _levelSettings;

    public void Initialize()
    {
        _levelSettings = GameObject.FindFirstObjectByType<LevelSettings>();
        if (_levelSettings == null) return;
        
        SetUp(_levelSettings.LevelHard);
        if (!CheckTutorial())
        {
            _orderManager.StartWork();
        }
    }

    private void SetUp(LevelHard levelHard)
    {
        if (levelHard == LevelHard.none) return;
        _goalManager.Initialize(_levelSettings.GetTaskManuals(),_orderManager);
        _orderManager.Initialize(levelHard, _goalManager, _goldManager);
        _anvilManager.Initialize(_levelSettings.infoAnvilManager);
        _pinManager.Initialize(_levelSettings.infoPinManager);
        _swordManager.Initialize(_levelSettings.infoSwordManager);
        _manikenManager.Initialize(_levelSettings.infoArmorManager);
        _instrumentManager.Initialize();
        _goldManager.Initialize(_levelSettings, _goalManager);
        _resultManager.Initialize(_orderManager, _goalManager);
        _timeManager.Initialize(_goalManager);
        _buttonControl.Initialize();
    }

    private bool CheckTutorial()
    {
        bool _isTutorial = false;
        switch (_levelSettings.Level)
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