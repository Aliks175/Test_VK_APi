using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _defeatPanel;
    [SerializeField] private GameObject _resultPanel; 
    private OrderManager _orderManager;
    private GoalManager _goalManager;

    private void OnDisable()
    {
        _orderManager.OnOver -= Over;
    }

    public void Initialize(OrderManager orderManager, GoalManager goalManager)
    {
        _orderManager = orderManager;
        _goalManager = goalManager;
        SetUp();
    }

    private void SetUp()
    {
        //это место где мы сверяем какой результат уровня 
        _orderManager.OnOver += Over;
    }

    private void Over()
    {
        // мы вызываем все цели сверяем выполнены ли они 
        if (_goalManager.CheckResultGame())
        {
            _winPanel.SetActive(true);
        }
        else
        {
            _defeatPanel.SetActive(true);
        }
        _resultPanel.SetActive(true);
    }
}