using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ToolInfoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private ExpUI expUI;

    private void OnEnable()
    {
        ToolManager.OnToolChanged.AddListener(ToolChanged);
        ToolManager.OnExpIncrease.AddListener(expUI.UpdateUI);
        ToolManager.OnExpLevelIncrease.AddListener(UpdateLevel);
        ToolManager.OnExpLevelExpIncrease.AddListener(expUI.LevelUpdateUI);
    }
    private void OnDisable()
    {
        ToolManager.OnToolChanged.RemoveListener(ToolChanged);
        ToolManager.OnExpIncrease.RemoveListener(expUI.UpdateUI);
        ToolManager.OnExpLevelIncrease.RemoveListener(UpdateLevel);
        ToolManager.OnExpLevelExpIncrease.RemoveListener(expUI.LevelUpdateUI);
    }

    public void UpdateLevel(int p_level)
    {
        levelText.text = p_level.ToString();

    }

    public void ToolChanged(Tool p_tool)
    {
        levelText.text = p_tool.expLevel.ToString();
        expUI.InstantUpdateUI(p_tool.expAmount,p_tool.so_Tool.maxExpAmount[p_tool.expLevel-1]);
    }
}
