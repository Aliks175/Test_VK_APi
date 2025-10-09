using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
   [SerializeField] private List<Resept> list = new();

    [SerializeField] private GameObject Pin;
    [SerializeField] private GameObject Sword;
    [SerializeField] private GameObject Armor;


    public bool CheckDetal(Resept resept)
    {
        foreach (var item in list)
        {
            if (item == resept )
            {
                return false;
            }
        }
        return true;
    }

    public void AddDetals(Resept resept)
    {
        list.Add(resept);

        switch (resept)
        {
            case Resept.Pin:
                Instantiate(Pin,transform);
                break;
            case Resept.Horseshoe:
                break;
            case Resept.Sword:
                Instantiate(Sword, transform);
                break;
            case Resept.Armor:
                Instantiate(Armor, transform);
                break;
            //case Resept.Helmet:
            //    break;
            //case Resept.Boots:
            //    break;
            //case Resept.Case:
            //    break;
            //default:
            //    break;
        }
    }
}
