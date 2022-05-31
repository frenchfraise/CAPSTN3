using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class Resource
{
    public SO_Item so_Resource;
    public int amount;
    [HideInInspector] public TMP_Text amountText;

    public void UpdateText()
    {
        amountText.text = amount.ToString();
    }
}

