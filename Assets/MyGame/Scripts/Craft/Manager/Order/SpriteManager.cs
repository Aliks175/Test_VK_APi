using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite _pin;
    [SerializeField] private Sprite _sword;
    [SerializeField] private Sprite _swordLezz;
    [SerializeField] private Sprite _swordHand;
    [SerializeField] private Sprite _swordFull;
    [SerializeField] private Sprite _armorLeather;
    [SerializeField] private Sprite _armorMetall;

    public Sprite GetSprite(OrderItem orderItem)
    {
        Sprite sprite = null;
        switch (orderItem)
        {
            case OrderItem.none:
                break;
            case OrderItem.Pin:
                sprite = _pin;
                break;
            case OrderItem.Sword:
                sprite = _sword;
                break;
            case OrderItem.SwordLezz:
                sprite = _swordLezz;
                break;
            case OrderItem.SwordHand:
                sprite = _swordHand;
                break;
            case OrderItem.SwordFull:
                sprite = _swordFull;
                break;
            case OrderItem.ArmorLeather:
                sprite = _armorLeather;
                break;
            case OrderItem.ArmorMetall:
                sprite = _armorMetall;
                break;
        }
        return sprite;
    }
}