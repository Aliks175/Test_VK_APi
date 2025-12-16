using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] private Button _buttonLeave;
    [SerializeField] private Button _buttonContinue;
    [SerializeField] private Button _buttonBackToMenu;

    private void OnDisable()
    {
        _buttonContinue.onClick.RemoveListener(() => GameManager.instance.LoadMenu());
        _buttonLeave.onClick.RemoveListener(() => GameManager.instance.LoadMenu());
        _buttonBackToMenu.onClick.RemoveListener(() => GameManager.instance.LoadMenu());
    }

    public void Initialize()
    {
        _buttonLeave.onClick.AddListener(() => GameManager.instance.LoadMenu());
        _buttonContinue.onClick.AddListener(() => GameManager.instance.LoadMenu());
        _buttonBackToMenu.onClick.AddListener(() => GameManager.instance.LoadMenu());
    }
}