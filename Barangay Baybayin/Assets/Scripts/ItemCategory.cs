using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemCategory
{
    [SerializeField] public string name;
    [NonReorderable] public List<ItemData> items = new List<ItemData>();
}
