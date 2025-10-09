using TMPro;
using UnityEngine;

public class Cliker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textClick;

    private string text = "Value Click :";
    private int value = 0;

    private void Start()
    {
        _textClick.SetText($"{text}\n{value}");
    }

    public void Click()
    {
        value++;
        _textClick.SetText($"{text}\n{value}");
    }
}
