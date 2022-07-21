using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FoodUI : MonoBehaviour
{
    public TMP_Text amountText;

    public void Awake()
    {
        Food.onUpdateFood.AddListener(AmountUpdated);
    }

    void AmountUpdated(int p_currentAmount)
    {
        amountText.text = p_currentAmount.ToString();
    }

}
