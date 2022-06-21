using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ToolUIElements
{
    public Image background;
    public Image icon;
    public TMP_Text levelCount;
    public Image fill;
}
public class ToolsUI : MonoBehaviour
{
    [NonReorderable]
    [SerializeField]
    private List<Sprite> frameLevels;
    [NonReorderable] [SerializeField] private List<ToolUIElements> toolUI = new List<ToolUIElements>();
    private ToolCaster toolCaster;
    private bool canUse = true;
    private bool canSwitch = true;
    private int currentEquip = 0;

    private void OnEnable()
    {
        toolCaster = FindObjectOfType<ToolCaster>();
        toolCaster.onToolCanUseUpdatedEvent.AddListener(CanUseUpdate);
        toolCaster.onToolCanSwitchUpdatedEvent.AddListener(CanSwitchUpdate);
        OnToolButtonPressed(0); // temporary (?)
        canUse = true;
        canSwitch = true;
        ToolManager.onProficiencyLevelModifiedEvent.AddListener(UpdateLevel);
        ToolManager.onProficiencyAmountModifiedEvent.AddListener(UpdateProf);
    }

    private void OnDisable()
    {
        ToolManager.onProficiencyLevelModifiedEvent.AddListener(UpdateLevel);
        toolCaster.onToolCanUseUpdatedEvent.RemoveListener(CanUseUpdate);
        toolCaster.onToolCanSwitchUpdatedEvent.RemoveListener(CanSwitchUpdate);
    }

    void CanSwitchUpdate(bool p_canSwitch)
    {
        canSwitch = p_canSwitch;
    }

    void CanUseUpdate(bool p_canSwitch)
    {
        canSwitch = p_canSwitch;
    }

    public void UpdateLevel(int p_level)
    {
        Tool selected_Tool = ToolManager.instance.tools[currentEquip];
        SO_Tool selectedSO_Tool = selected_Tool.so_Tool;
        toolUI[currentEquip].levelCount.text = selected_Tool.proficiencyLevel.ToString();
        toolUI[currentEquip].fill.sprite = frameLevels[selected_Tool.proficiencyLevel];
    }

    public void UpdateProf(float p_curr, float p_max)
    {
        Tool selected_Tool = ToolManager.instance.tools[currentEquip];
        SO_Tool selectedSO_Tool = selected_Tool.so_Tool;
        Debug.Log(p_curr + " - " + p_max);
        Debug.Log(selected_Tool.proficiencyAmount + " - " +  selectedSO_Tool.maxProficiencyAmount[selected_Tool.craftLevel-1]);
        toolUI[currentEquip].fill.fillAmount = selected_Tool.proficiencyAmount / selectedSO_Tool.maxProficiencyAmount[selected_Tool.craftLevel-1];

    }
    public void OnToolButtonPressed(int index)
    {
        if (canUse)
        {
            if (canSwitch)
            {
                currentEquip = index;
                canSwitch = false;
                Tool selected_Tool = ToolManager.instance.tools[index];
                SO_Tool selectedSO_Tool = selected_Tool.so_Tool;
                for (int i = 0; i < toolUI.Count; i++)
                {
                    if (index == i)
                    {
                      
                       
                        toolUI[index].background.sprite = selectedSO_Tool.equippedFrame;
                        toolUI[index].icon.sprite = selectedSO_Tool.equippedIcon[selected_Tool.craftLevel-1];
                        
                    }
                    else
                    {
                        Tool current_Tool = ToolManager.instance.tools[i];
                        SO_Tool so_Tool = current_Tool.so_Tool;
                        toolUI[i].background.sprite = so_Tool.unlockedFrame;
                        toolUI[i].icon.sprite = so_Tool.unlockedIcon;

                    }


                }


                ToolManager.onToolChangedEvent.Invoke(selected_Tool);
            }
        }
        
       
    }

}
