using UnityEngine;

public class InstrumentManager : MonoBehaviour
{
    [SerializeField] private AnvilManager _anvilManager;
    [SerializeField] private MannequinManager _manikenManager;
    [SerializeField] private SwordManager _swordManager;
    [SerializeField] private PinManager _pinManager;
    private AnvilSlot _iron;
    private MannequinSlots _manikenSlot;
    private SwordSlot _swordSlot;

    public void Initialize()
    {
        _iron = null;
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
        if (_iron != null && !_pinManager.IsCreate)
        {
            _iron.PickUpIron();
            _pinManager.AddPin();
        }
        _iron = null;
    }

    private void OnValidate()
    {
        if (_anvilManager == null)
        {
            _anvilManager = GameObject.FindFirstObjectByType<AnvilManager>();
        }
        if (_manikenManager == null)
        {
            _manikenManager = GameObject.FindFirstObjectByType<MannequinManager>();
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