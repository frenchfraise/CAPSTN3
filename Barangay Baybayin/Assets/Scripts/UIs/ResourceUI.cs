using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResourceUI : MonoBehaviour
{
    public TMP_Text resourceNameText;
    public TMP_Text resourceAmountText;

    public void InitializeValues(string p_resourceName, string p_resourceAmount)
    {
        resourceNameText.text = p_resourceName;
        resourceAmountText.text = p_resourceAmount;
    }
}
