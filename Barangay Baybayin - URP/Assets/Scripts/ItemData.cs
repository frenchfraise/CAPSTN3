using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class ItemData
{
    public SO_Item so_Item;
    public bool isUnlocked;
    [SerializeField] private bool isDynamicallyShown;
    public int amount;
    [HideInInspector] public TMP_Text amountText;

    public void UpdateText()
    {
        amountText.text = amount.ToString();
    }
}
