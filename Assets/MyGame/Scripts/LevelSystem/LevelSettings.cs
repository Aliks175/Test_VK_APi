using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public static LevelSettings Instance;
    public static bool IsAwake { get { return (Instance != null); } }
    public int Level
    {
        get
        {
            return _levelNumber;
        }
        set
        {
            _levelNumber = value;
        }
    }
    [SerializeField, Min(1)] private int _levelNumber = 1;
    public LevelHard LevelHard { get; private set; }
    //public LevelTask LevelTask { get; private set; }
    [Header("ValueSlot")]
    [Range(1, 3)] public int ValueAnvil = 1;
    [Range(2, 6)] public int ValuePinSlot = 2;
    [Range(0, 3)] public int ValueSword = 0;
    [Range(0, 3)] public int ValueArmor = 0;
    [Space(15)]
    [Header("TIME")]
    [Header("Anvil")]
    public float WaitCreateIron = 4;
    public float WaitFireIron = 8;
    //public float WaitClearBrakeIron = 2;
    [Header("Pin")]
    public float WaitCreatePin = 8;
    [Header("Armor")]
    public float WaitCreateeLeatherArmor = 4;
    public float WaitCreateMetallArmor = 6;
    public InfoAnvilManager infoAnvilManager;
    public InfoPinManager infoPinManager;
    public InfoSwordManager infoSwordManager;
    public InfoArmorManager infoArmorManager;

    private void Awake()
    {
        if (IsAwake)
        {
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
            GameObject.DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        // нужно позаимствовать из сингл тона проверку что это единственый скрипт на сцене иначе после каждого пройденого уровня мы будем кланировать этот скрипт при загрузке сцены меню
        infoAnvilManager = new InfoAnvilManager()
        {
            ValueAnvil = ValueAnvil,
            WaitCreateIron = WaitCreateIron,
            WaitFireIron = WaitFireIron,
        };
        infoPinManager = new InfoPinManager()
        {
            ValuePinSlot = ValuePinSlot,
            WaitCreatePin = WaitCreatePin
        };
        infoSwordManager = new InfoSwordManager()
        {
            IsUpgrade = false,
            ValueSwordSlot = ValueSword
        };
        infoArmorManager = new InfoArmorManager()
        {
            IsOpenMetallArmor = false,
            ValueArmorSlot = ValueArmor,
            WaitCreateLeather = WaitCreateeLeatherArmor,
            WaitCreateMetall = WaitCreateMetallArmor,
        };
        ScaleHardLevel();
    }

    //public void SetLevelTask(LevelTask levelTask)
    //{
    //    LevelTask = levelTask;
    //}

    public void UpdateChangers(TypeChangers typeChangers)
    {
        switch (typeChangers)
        {
            case TypeChangers.Anvil:

                infoAnvilManager.ValueAnvil = ValueAnvil;
                infoAnvilManager.WaitCreateIron = WaitCreateIron;
                infoAnvilManager.WaitFireIron = WaitFireIron;

                break;
            case TypeChangers.Pin:
                infoPinManager.WaitCreatePin = WaitCreatePin;
                infoPinManager.ValuePinSlot = ValuePinSlot;

                break;
            case TypeChangers.Sword:

                infoSwordManager.ValueSwordSlot = ValueSword;
                break;
            case TypeChangers.Armor:

                infoArmorManager.ValueArmorSlot = ValueArmor;
                infoArmorManager.WaitCreateLeather = WaitCreateeLeatherArmor;
                infoArmorManager.WaitCreateMetall = WaitCreateMetallArmor;

                break;
            default:
                break;
        }
    }

    public void ScaleHardLevel()
    {
        LevelHard = LevelHard.LevelOne;
        if (Level > 1 && Level <= 3)
        {
            LevelHard = LevelHard.LevelTwo;
            ValueSword = 1;
        }
        else if (Level > 3 && Level <= 6)
        {
            LevelHard = LevelHard.LevelThree;
            ValueSword = 1;
            ValueArmor = 1;
        }
        else if (Level > 6 && Level <= 9)
        {
            LevelHard = LevelHard.LevelFour;
            ValueSword = 1;
            ValueArmor = 1;
            infoArmorManager.IsOpenMetallArmor = true;
        }
        else if (Level >= 10)
        {
            LevelHard = LevelHard.LevelFive;
            ValueSword = 1;
            ValueArmor = 1;
            infoArmorManager.IsOpenMetallArmor = true;
            infoSwordManager.IsUpgrade = true;
        }
        UpdateChangers(TypeChangers.Anvil);
        UpdateChangers(TypeChangers.Sword);
        UpdateChangers(TypeChangers.Armor);
        UpdateChangers(TypeChangers.Pin);
    }

}

//    1) гвозди 
//1 ур (Туториал и игровой процесс)
//2) гвозди + мечи 
//2 - 3 ур 
//3) гвозди + мечи + кожаный доспех
//4 - 6 ур 
//4) гвозди + мечи + кожаный доспех + металлический доспех 
//7 - 10 ур
//5) гвозди + мечи + кожаный доспех + металлический доспех  + полный меч
//+10 ур


public struct InfoAnvilManager
{
    public int ValueAnvil;
    public float WaitCreateIron;
    public float WaitFireIron;
}

public struct InfoPinManager
{
    public int ValuePinSlot;
    public float WaitCreatePin;
}

public struct InfoSwordManager
{
    public int ValueSwordSlot;
    public bool IsUpgrade;
}

public struct InfoArmorManager
{
    public int ValueArmorSlot;
    public bool IsOpenMetallArmor;
    public float WaitCreateMetall;
    public float WaitCreateLeather;
}

public enum TypeChangers
{
    Anvil,
    Pin,
    Sword,
    Armor
}