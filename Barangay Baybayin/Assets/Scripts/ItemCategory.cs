using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemCategory
{
    [SerializeField] private string name;
    public List<ItemData> items = new List<ItemData>();
}
