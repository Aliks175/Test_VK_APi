using UnityEngine;

public class Craft : MonoBehaviour
{
    [SerializeField] private Resept Resept;
    private Slotmanager slotmanager;

    private void Awake()
    {
        slotmanager = GameObject.FindFirstObjectByType<Slotmanager>();
    }

    public void AddItem()
    {
        slotmanager.AddDetal(Resept);
        Debug.Log($"Item add - {Resept}");

        if ($"{Resept}" == "Pin")
        {
            Debug.Log($"Yes");

        }
        else
        {
            Debug.Log($"no");

        }
    }
}

/// <summary>
/// Это то что добавляется к слотам предметов на прилавке
/// </summary>

public enum Resept
{
    Pin,//гвоздь
    Horseshoe,//подкова
    Sword,//мечь
    Armor,//броня
    Helmet,//шлем
    Boots,//ботинки
    Case// Ножны


}