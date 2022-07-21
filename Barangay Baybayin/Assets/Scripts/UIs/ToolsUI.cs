using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ToolUIElements
{
    public Image background;
    public Image icon;
    public TMP_Text levelCount;
    public Image frame;
    [SerializeField] public GenericBarUI genericBarUI;
}

public class ToolQuestSwitchEvent : UnityEvent<int> { };
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
    [SerializeField] private int requiredTool = -1;

    public void Awake()
    {
       
    }

    private void Start()
    {
        OnToolButtonPressed(0); // temporary (?)
        ToolManager.onToolChangedEvent.Invoke(ToolManager.instance.tools[0]);
    }
    public static ToolQuestSwitchEvent onToolQuestSwitchEvent = new ToolQuestSwitchEvent();
    private void OnEnable()
    {
        toolCaster = FindObjectOfType<ToolCaster>();
        toolCaster.onToolCanUseUpdatedEvent.AddListener(CanUseUpdate);
        //toolCaster.onToolCanSwitchUpdatedEvent.AddListener(CanSwitchUpdate);

        canUse = true;
        canSwitch = true;
        //ToolManager.onProficiencyLevelModifiedEvent.AddListener(UpdateLevel);
        //ToolManager.onProficiencyAmountModifiedEvent.AddListener(UpdateProf);
        ToolManager.onProficiencyAmountModifiedEvent.AddListener(UpdateProf);
        ToolManager.onProficiencyLevelModifiedEvent.AddListener(UpdateProfLevel);
        ToolManager.onToolCraftLevelUpgradedEvent.AddListener(UpdateCraftLevel);
        onToolQuestSwitchEvent.AddListener(RequireTool);
        foreach (ToolUIElements t in toolUI)
        {
            t.genericBarUI.InstantUpdateBar(0f,1f,1f);

        }
    }

    private void OnDisable()
    {
        ToolManager.onProficiencyLevelModifiedEvent.RemoveListener(UpdateProfLevel);
        toolCaster.onToolCanUseUpdatedEvent.RemoveListener(CanUseUpdate);
        //toolCaster.onToolCanSwitchUpdatedEvent.RemoveListener(CanSwitchUpdate);
    }
    void RequireTool(int p_tool)
    {
        requiredTool = p_tool;
    }
    void CanSwitchUpdate(bool p_canSwitch)
    {
        canSwitch = p_canSwitch;
    }

    void CanUseUpdate(bool p_canSwitch)
    {
        canSwitch = p_canSwitch;
    }
    public void UpdateCraftLevel(int p_index)
    {
        //Tool selected_Tool = ToolManager.instance.tools[p_index];
        //SO_Tool selectedSO_Tool = selected_Tool.so_Tool;
      
        //toolUI[currentEquip].icon.sprite = selectedSO_Tool.equippedIcon[selected_Tool.craftLevel+1];
        for (int i = 0; i < toolUI.Count; i++)
        {
           
            
            Tool current_Tool = ToolManager.instance.tools[i];
            SO_Tool so_Tool = current_Tool.so_Tool;
            toolUI[i].background.sprite = so_Tool.unlockedFrame;
            toolUI[i].icon.sprite = so_Tool.equippedIcon[current_Tool.craftLevel];//.unlockedIcon;

            


        }



    }
    public void UpdateProfLevel(int p_level)
    {
        Tool selected_Tool = ToolManager.instance.tools[currentEquip];
        SO_Tool selectedSO_Tool = selected_Tool.so_Tool;
        toolUI[currentEquip].levelCount.text = selected_Tool.proficiencyLevel.ToString();
        toolUI[currentEquip].frame.sprite = frameLevels[selected_Tool.proficiencyLevel];
        toolUI[currentEquip].genericBarUI.ResetBar(selected_Tool.proficiencyAmount, 
            selectedSO_Tool.maxProficiencyAmount[selected_Tool.proficiencyLevel]);
  


    }

    public void UpdateProf(float p_curr, float p_max)
    {
        Tool selected_Tool = ToolManager.instance.tools[currentEquip];
        SO_Tool selectedSO_Tool = selected_Tool.so_Tool;
        Debug.Log(p_curr + " - " + p_max);
        Debug.Log(selected_Tool.proficiencyAmount + " - " +  selectedSO_Tool.maxProficiencyAmount[selected_Tool.craftLevel]);
        //toolUI[currentEquip].genericBarUI.fillAmount = selected_Tool.proficiencyAmount / selectedSO_Tool.maxProficiencyAmount[selected_Tool.craftLevel];
        if (toolUI[currentEquip].genericBarUI.gameObject.activeSelf)
        {
            toolUI[currentEquip].genericBarUI.UpdateBar(selected_Tool.proficiencyAmount, selectedSO_Tool.maxProficiencyAmount[selected_Tool.craftLevel]);
        }
        else
        {
            toolUI[currentEquip].genericBarUI.InstantUpdateBar(selected_Tool.proficiencyAmount, selectedSO_Tool.maxProficiencyAmount[selected_Tool.craftLevel], selectedSO_Tool.maxProficiencyAmount[selected_Tool.craftLevel]);
        }
        
    }
    public void OnToolButtonPressed(int index)
    {
        Debug.Log("tool button pressed");
        if (requiredTool == -1 || requiredTool != -1 && requiredTool == index)
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
                            toolUI[index].icon.sprite = selectedSO_Tool.equippedIcon[selected_Tool.craftLevel];

                        }
                        else
                        {
                            Tool current_Tool = ToolManager.instance.tools[i];
                            SO_Tool so_Tool = current_Tool.so_Tool;
                            toolUI[i].background.sprite = so_Tool.unlockedFrame;
                            toolUI[i].icon.sprite = so_Tool.equippedIcon[current_Tool.craftLevel];//.unlockedIcon;

                        }


                    }


                    ToolManager.onToolChangedEvent.Invoke(selected_Tool);
                    CanSwitchUpdate(true);
                }
            }
        }
        else if (requiredTool != -1 && requiredTool != index)
        {
            Debug.Log("DADA");
            StorylineManager.onWorldEventEndedEvent.Invoke("EQUIPPINGWRONGTOOL", 0, 0);
        }


    }

}
