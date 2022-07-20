using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolChargesUI : MonoBehaviour
{
    private TMP_Text toolChargeTMP;
    private Tool tool;
    public ToolCaster toolCaster;
    public Image currentFistIcon;
    public Sprite availFistIcon;
    public Sprite unavailFistIcon;

    private void Awake()
    {
        toolChargeTMP = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        ToolManager.onSpecialPointsModifiedEvent.AddListener(ModifiedUpdate);
        ToolManager.onSpecialPointsFilledEvent.AddListener(ChangeUpdate);
        toolCaster.onToolSpecialUsedEvent.AddListener(ChangeUpdate);
        ToolManager.onToolChangedEvent.AddListener(UpdateToolOnSwitch);
    }

    private void OnDisable()
    {
        ToolManager.onSpecialPointsModifiedEvent.RemoveListener(ModifiedUpdate);
        ToolManager.onSpecialPointsFilledEvent.RemoveListener(ChangeUpdate);
        toolCaster.onToolSpecialUsedEvent.RemoveListener(ChangeUpdate);
        ToolManager.onToolChangedEvent.RemoveListener(UpdateToolOnSwitch);
    }

    void UpdateToolOnSwitch(Tool p_tool)
    {
        tool = p_tool;
        if (tool.specialChargesCounter > 0)
        {
            currentFistIcon.sprite = availFistIcon;
        }
        else
        {
            currentFistIcon.sprite = unavailFistIcon;
        }
        toolChargeTMP.text = $"{tool.specialChargesCounter}";
    }
    void ModifiedUpdate(float p_max, float p_current)
    {
        if (tool.specialChargesCounter > 0)
        {
            currentFistIcon.sprite = availFistIcon;
        }
        else
        {
            currentFistIcon.sprite = unavailFistIcon;
        }
        toolChargeTMP.text = $"{tool.specialChargesCounter}";
    }
    void ChangeUpdate()
    {
        if (tool.specialChargesCounter > 0)
        {
            currentFistIcon.sprite = availFistIcon;
        }
        else
        {
            currentFistIcon.sprite = unavailFistIcon;
        }
        toolChargeTMP.text = $"{tool.specialChargesCounter}";
    }
}
