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
        ToolManager.onToolChanged.AddListener(ToolChanged);
        ToolManager.onProficiencyAmountModified.AddListener(genericBarUI.UpdateBar);
        ToolManager.onProficiencyLevelModified.AddListener(UpdateLevel);
     
    }
    private void OnDisable()
    {
        ToolManager.onToolChanged.RemoveListener(ToolChanged);
        ToolManager.onProficiencyAmountModified.RemoveListener(genericBarUI.UpdateBar);
        ToolManager.onProficiencyLevelModified.RemoveListener(UpdateLevel);
        
    }

    public void UpdateLevel(int p_level)
    {
        levelText.text = p_level.ToString();
        genericBarUI.ResetBar(1,1);
    }

    public void ToolChanged(Tool p_tool)
    {
        levelText.text = p_tool.proficiencyLevel.ToString();
        if (p_tool.so_Tool.maxProficiencyAmount.Count > 0)
        {
            genericBarUI.InstantUpdateBar(p_tool.proficiencyAmount, p_tool.so_Tool.maxProficiencyAmount[p_tool.proficiencyLevel - 1], p_tool.so_Tool.maxProficiencyAmount[p_tool.proficiencyLevel - 1]);

        }
        else
        {
            Debug.Log(p_tool.so_Tool.name + " IS MISSING maxProficiencyAmount OR proficiencyLevel");
        }
    }
}
