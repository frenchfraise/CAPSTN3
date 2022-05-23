using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolCritMeterUI : MonoBehaviour
{
    public Image image;

    public void ResetBar()
    {
        image.fillAmount = 0;
    } 

    public void UpdateBar(float p_current, float p_max)
    {
        float fill = p_current / p_max;
        image.fillAmount = fill;
    }
}
