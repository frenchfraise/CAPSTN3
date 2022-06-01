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
        ToolManager.onToolChangedEvent.AddListener(ToolChanged);
        ToolManager.onProficiencyAmountModifiedEvent.AddListener(genericBarUI.UpdateBar);
        ToolManager.onProficiencyLevelModifiedEvent.AddListener(UpdateLevel);
     
    }
    private void OnDisable()
    {
        ToolManager.onToolChangedEvent.RemoveListener(ToolChanged);
        ToolManager.onProficiencyAmountModifiedEvent.RemoveListener(genericBarUI.UpdateBar);
        ToolManager.onProficiencyLevelModifiedEvent.RemoveListener(UpdateLevel);
        
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
