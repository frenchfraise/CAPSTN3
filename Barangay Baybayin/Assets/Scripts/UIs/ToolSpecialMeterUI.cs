using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolSpecialMeterUI : MonoBehaviour
{
    [SerializeField] private Image realBar;
    private void OnEnable()
    {
        ToolManager.onSpecialPointsFilledEvent.AddListener(ResetBar);
        ToolManager.onSpecialPointsModifiedEvent.AddListener(UpdateBar);
        ToolManager.onToolChangedEvent.AddListener(InstantUpdateBar);
    }

    private void OnDisable()
    {
        ToolManager.onSpecialPointsFilledEvent.RemoveListener(ResetBar);
        ToolManager.onSpecialPointsModifiedEvent.RemoveListener(UpdateBar);
        ToolManager.onToolChangedEvent.RemoveListener(InstantUpdateBar);
    }

    void InstantUpdateBar(Tool p_tool)
    {
   
        float fill = p_tool.specialPoints / p_tool.so_Tool.maxSpecialPoints[p_tool.craftLevel];
        realBar.fillAmount = fill;
    }
    void UpdateBar(float p_current, float p_max)
    {
        //Debug.Log("UI: " + p_current + " - " + p_max);
        float fill = p_current / p_max;
        realBar.fillAmount = fill;
    }

    void FillBar()
    {
        //Debug.Log("UI FILLED");
        float fill = 1 / 1;
        realBar.fillAmount = fill;
    }

    void ResetBar()
    {
        float fill = 0;
        realBar.fillAmount = fill;
    }
}
