using System.Collections.Generic;
using UnityEngine;

public class ManagerItems : MonoBehaviour
{
    [SerializeField] private Sprite Pin;
    [SerializeField] private Sprite Sword;
    [SerializeField] private Sprite Armor;

    [SerializeField] private GameObject _imagePanel;

    [SerializeField] private int _valuePool = 20;

    private List<GameObject> list;

    void Start()
    {
        for (int i = 0; i < _valuePool; i++)
        {
            GameObject newItemPanel = Instantiate(_imagePanel, Vector3.zero, Quaternion.identity);
            newItemPanel.transform.parent = transform;
            list.Add(newItemPanel);
        }
    }
}
