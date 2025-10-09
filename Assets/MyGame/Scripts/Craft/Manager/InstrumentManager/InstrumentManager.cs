using UnityEngine;

public class InstrumentManager : MonoBehaviour
{
    [SerializeField] private AnvilManager _anvilManager;
    [SerializeField] private ManikenManager _manikenManager;
    [SerializeField] private SwordManager _swordManager;
    [SerializeField] private PinManager _pinManager;

    private AnvilSlot _iron;
    private ManikenSlot _manikenSlot;
    private SwordSlot _swordSlot;
    private SlotPin _slotPin;

    public void Initialize()
    {
        _iron = null;

    }

    private void Start() // Когда будет единая точка входа удалить 
    {
        Initialize();
    }


    public void CreateSword()
    {
        _iron = _anvilManager.CheckFreeAiron();
        _swordSlot = _swordManager.CheckFreeSlot();

        if (_iron != null && _swordSlot != null)
        {
            _iron.PickUpIron();
            _swordSlot.AddSword();
        }
        _iron = null;
        _swordSlot = null;
    }

    public void CreateArmor()
    {
        _iron = _anvilManager.CheckFreeAiron();
        _manikenSlot = _manikenManager.AddMetallArmor();

        if (_iron != null && _manikenSlot != null)
        {
            _iron.PickUpIron();
            _manikenSlot.AddArmor(TypeArmor.MetallArmor);
        }
        _iron = null;
        _manikenSlot = null;
    }

    public void CreatePin()
    {
        _iron = _anvilManager.CheckFreeAiron();
        _slotPin = _pinManager.CheckFreeSlot();
        if (_iron != null && _slotPin != null)
        {
            _iron.PickUpIron();
            _slotPin.AddPin();
        }
        _iron = null;
        _slotPin = null;
    }



    private void OnValidate()
    {
        if (_anvilManager == null)
        {
            _anvilManager = GameObject.FindFirstObjectByType<AnvilManager>();
        }

        if (_manikenManager == null)
        {
            _manikenManager = GameObject.FindFirstObjectByType<ManikenManager>();
        }

        if (_swordManager == null)
        {
            _swordManager = GameObject.FindFirstObjectByType<SwordManager>();
        }

        if (_pinManager == null)
        {
            _pinManager = GameObject.FindFirstObjectByType<PinManager>();
        }
    }
}
