using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemAmountText;
    [SerializeField] private Image itemIconImage;
    public void InitializeValues(string p_itemName, string p_itemAmount, Sprite p_itemIcon)
    {
        itemNameText.text = p_itemName;
        itemAmountText.text = p_itemAmount;
        itemIconImage.sprite = p_itemIcon;
    }

    public TMP_Text GetitemAmountText()
    {
        return itemAmountText;
    }
}
