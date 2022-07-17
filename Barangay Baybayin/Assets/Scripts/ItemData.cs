using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public SO_Item so_Item;
    public bool isUnlocked;
    [SerializeField] private bool isDynamicallyShown;


    public ItemUI itemUI;
    public int amount;
    public void SetItemUI(ItemUI p_itemUI)
    {
        itemUI = p_itemUI;
    }
    
}
