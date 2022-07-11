using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolUI : MonoBehaviour
{
    [SerializeField] private Image toolIcon;


    private void OnEnable()
    {
        ToolManager.onToolChangedEvent.AddListener(ChangeToolIcon);
        //ChangeToolIcon(ToolManager.instance.tools[0]);
    }

    private void OnDisable()
    {
        ToolManager.onToolChangedEvent.RemoveListener(ChangeToolIcon);
    }


    public void ChangeToolIcon(Tool p_currentTool)
    {
        toolIcon.sprite = p_currentTool.so_Tool.unlockedIcon;

    }

}
