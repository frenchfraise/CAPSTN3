using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ToolInfoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GenericBarUI genericBarUI;

    private void OnEnable()
    {
        ToolManager.OnToolChanged.AddListener(ToolChanged);
        ToolManager.OnExpIncrease.AddListener(genericBarUI.UpdateBar);
        ToolManager.OnExpLevelIncrease.AddListener(UpdateLevel);
        ToolManager.OnExpLevelExpIncrease.AddListener(genericBarUI.ResetBar);
    }
    private void OnDisable()
    {
        ToolManager.OnToolChanged.RemoveListener(ToolChanged);
        ToolManager.OnExpIncrease.RemoveListener(genericBarUI.UpdateBar);
        ToolManager.OnExpLevelIncrease.RemoveListener(UpdateLevel);
        ToolManager.OnExpLevelExpIncrease.RemoveListener(genericBarUI.ResetBar);
    }

    public void UpdateLevel(int p_level)
    {
        levelText.text = p_level.ToString();

    }

    public void ToolChanged(Tool p_tool)
    {
        levelText.text = p_tool.expLevel.ToString();
        genericBarUI.InstantUpdateBar(p_tool.expAmount,p_tool.so_Tool.maxExpAmount[p_tool.expLevel-1], p_tool.so_Tool.maxExpAmount[p_tool.expLevel - 1]);
    }
}
