using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite Pin;
    [SerializeField] private Sprite Sword;
    [SerializeField] private Sprite SwordLezz;
    [SerializeField] private Sprite SwordHand;
    [SerializeField] private Sprite SwordFull;
    [SerializeField] private Sprite ArmorLeather;
    [SerializeField] private Sprite ArmorMetall;

    public Sprite GetSprite(OrderItem orderItem)
    {
        Sprite sprite = null;

        switch (orderItem)
        {
            case OrderItem.none:
                
                break;
            case OrderItem.Pin:
                sprite = Pin;
                break;
            case OrderItem.Sword:
                sprite = Sword;
                break;
            case OrderItem.SwordLezz:
                sprite = SwordLezz;
                break;
            case OrderItem.SwordHand:
                sprite = SwordHand;
                break;
            case OrderItem.SwordFull:

                sprite = SwordFull;
                break;
            case OrderItem.ArmorLeather:
                sprite = ArmorLeather;
                break;
            case OrderItem.ArmorMetall:
                sprite = ArmorMetall;
                break;
        }

        return sprite;
    }

}
