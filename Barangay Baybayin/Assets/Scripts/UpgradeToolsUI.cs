using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeToolsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text currentPower;
    [SerializeField] private TMP_Text upgradePower;

    [SerializeField] private TMP_Text currentStamina;
    [SerializeField] private TMP_Text upgradeStamina;

    private int currentIndex;
    [SerializeField]
    private List<UpgradeToolUI> upgradeToolUIs = new List<UpgradeToolUI>();

    public void ToolButtonUIClicked(int p_toolIndex)
    {
        currentIndex = p_toolIndex;
        UpdateCurrentToolUI(ToolManager.instance.tools[currentIndex]);
    }

    void UpdateCurrentToolUI(Tool p_currentTool)
    {
        SO_Tool currentSOTool = ToolManager.instance.tools[currentIndex].so_Tool;
        int currentToolLevel = ToolManager.instance.tools[currentIndex].craftLevel;
        int upgradeToolLevel = currentToolLevel+1;

        currentPower.text = p_currentTool.so_Tool.damage[currentToolLevel].ToString();
        upgradePower.text = p_currentTool.so_Tool.damage[upgradeToolLevel].ToString();
        currentStamina.text = p_currentTool.so_Tool.staminaCost[currentToolLevel].ToString();
        upgradeStamina.text = p_currentTool.so_Tool.staminaCost[upgradeToolLevel].ToString();

    }

    public void ToolUpgradeButtonUIClicked()
    {
        UpgradeToolUI currentlyUpgradedToolUI = upgradeToolUIs[currentIndex];


    }
}
