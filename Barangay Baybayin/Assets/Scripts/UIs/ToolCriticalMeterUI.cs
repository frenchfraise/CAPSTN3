using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolCriticalMeterUI : MonoBehaviour
{

    [SerializeField] private Image realBar;

    public void UpdateBar(float p_current, float p_max)
    {
        float fill = p_current / p_max;
        realBar.fillAmount = fill;
    }
}
