using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolUI : MonoBehaviour
{
    [SerializeField] private Image toolIcon;
    SO_Tool selectedSO_Tool;
    private void Awake()
    {
        ToolManager.onToolChangedEvent.AddListener(ChangeToolIcon);
        ToolManager.onToolCraftLevelUpgradedEvent.AddListener(UpdateCraftLevel);
    }
    private void OnDestroy()
    {
    
        ToolManager.onToolChangedEvent.RemoveListener(ChangeToolIcon);
        ToolManager.onToolCraftLevelUpgradedEvent.RemoveListener(UpdateCraftLevel);
    }
    private void OnEnable()
    {
    
        //ChangeToolIcon(ToolManager.instance.tools[0]);
    }

    private void OnDisable()
    {
        
    }

    public void UpdateCraftLevel(int p_index)
    {
     
        toolIcon.sprite = selectedSO_Tool.equippedIcon[ToolManager.instance.tools[p_index].craftLevel];




    }
    public void ChangeToolIcon(Tool p_currentTool)
    {
       
        selectedSO_Tool = p_currentTool.so_Tool;
        toolIcon.sprite = p_currentTool.so_Tool.equippedIcon[p_currentTool.craftLevel];

    }

}
