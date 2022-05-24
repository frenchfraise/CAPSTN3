using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class Resource
{
    public SO_Resource so_Resource;
    public int amount;
    [HideInInspector] public TMP_Text text;

    public void UpdateText() //temporary
    {
        text.text = amount.ToString();
    }
}

