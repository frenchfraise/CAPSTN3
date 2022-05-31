using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolsUI : MonoBehaviour
{
    [SerializeField] private List<Image> toolUI = new List<Image>();
    private ToolCaster toolCaster;
    private bool canUse = true;
    private bool canSwitch = true;

    private void OnEnable()
    {
        toolCaster = FindObjectOfType<ToolCaster>();
        toolCaster.onToolCanUseUpdated.AddListener(CanUseUpdate);
        toolCaster.onToolCanSwitchUpdated.AddListener(CanSwitchUpdate);
        OnToolButtonPressed(0);


    }

    private void OnDisable()
    {
        toolCaster.onToolCanUseUpdated.RemoveListener(CanUseUpdate);
        toolCaster.onToolCanSwitchUpdated.RemoveListener(CanSwitchUpdate);
    }

    void CanSwitchUpdate(bool p_canSwitch)
    {
        canSwitch = p_canSwitch;
    }

    void CanUseUpdate(bool p_canSwitch)
    {
        canSwitch = p_canSwitch;
    }
    public void OnToolButtonPressed(int index)
    {
        if (canUse)
        {
            if (canSwitch)
            {
                canSwitch = false;
                Tool selected_Tool = ToolManager.instance.tools[index];
                SO_Tool selectedSO_Tool = selected_Tool.so_Tool;
                for (int i = 0; i < toolUI.Count; i++)
                {
                    if (index == i)
                    {

                        toolUI[index].sprite = selectedSO_Tool.equippedFrame;
                        toolUI[index].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = selectedSO_Tool.equippedIcon;

                    }
                    else
                    {
                        Tool current_Tool = ToolManager.instance.tools[i];
                        SO_Tool so_Tool = current_Tool.so_Tool;
                        toolUI[i].sprite = so_Tool.unlockedFrame;
                        toolUI[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = so_Tool.unlockedIcon;

                    }


                }


                ToolManager.onToolChanged.Invoke(selected_Tool);
            }
        }
        
       
    }

}
