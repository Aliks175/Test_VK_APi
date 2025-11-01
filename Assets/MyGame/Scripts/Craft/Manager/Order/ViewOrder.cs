using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ViewOrder : MonoBehaviour
{
    [SerializeField] private Image _oneImage;
    [SerializeField] private Image _twoImage;
    [SerializeField] private Image _threeImage;
    [SerializeField] private SpriteManager _spriteManager;
    public UnityEvent OnInitialize;
    public UnityEvent OnStart;
    public UnityEvent OnEnd;

    public void Initialize()
    {
        OnInitialize?.Invoke();
    }

    public void SetView(ListOrders orderList)
    {
        _oneImage.sprite = _spriteManager.GetSprite(orderList.OneItem);
        _twoImage.sprite = _spriteManager.GetSprite(orderList.TwoItem);
        _threeImage.sprite = _spriteManager.GetSprite(orderList.ThreeItem);
    }

    public void OnStartEvent()
    {
        OnStart?.Invoke();
    }

    public void OnEndEvent()
    {
        OnEnd?.Invoke();
    }

    public void OnView(ListOrders orderList)
    {
        if (orderList.OneItem != OrderItem.none)
        {
            _oneImage.enabled = true;
        }
        else
        {
            _oneImage.enabled = false;
        }

        if (orderList.TwoItem != OrderItem.none)
        {
            _twoImage.enabled = true;
        }
        else
        {
            _twoImage.enabled = false;
        }

        if (orderList.ThreeItem != OrderItem.none)
        {
            _threeImage.enabled = true;
        }
        else
        {
            _threeImage.enabled = false;
        }
    }

    private void OnValidate()
    {
        if (_spriteManager == null && gameObject.activeInHierarchy)
        {
            _spriteManager = transform.parent.GetComponent<SpriteManager>();
        }
    }
}