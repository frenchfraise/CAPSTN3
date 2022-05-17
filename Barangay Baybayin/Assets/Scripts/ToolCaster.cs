using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolCaster : MonoBehaviour
{
    public Tool current_Tool;
    public bool canUse = true;

    //Feel free to change anything but the SO_Tool reference
    //To make use of the resoucenode, make sure that the OnHit Unity Event of the resource node will be invoked
    //To temporarily decrease stamina, do Stamina.instance.StaminaDecreased(5)
    public void Use()
    {
        if (canUse)
        {
            StartCoroutine(Co_Cooldown());
        }

    }


    private void Update()
    {
        // Uses too much Stamina
        /*if (Input.touchCount > 0)
        {
            Use();
        }*/

        //FOR TESTING PURPOSES
        /*if (Input.GetMouseButtonDown(0))
        {
            Use();
        }*/
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            current_Tool = ToolManager.instance.tools[0];
            ToolManager.OnToolChanged.Invoke(current_Tool);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            current_Tool = ToolManager.instance.tools[1];
            ToolManager.OnToolChanged.Invoke(current_Tool);
        }
    }
    public void DetectResourceNode() //TEMPORARY DETECTION, CHANGE AS YOU SEE FIT
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)transform.position, 3f);
        foreach (Collider2D hit in collider)
        {
            
            if (hit.gameObject != gameObject)
            {
                if (hit != null)
                {
                 
                    //Debug.Log("HIT " + hit.gameObject.name);
                    ResourceNode targetResourceNode = hit.gameObject.GetComponent<ResourceNode>(); //THIS IS IMPORTANT

                    if (targetResourceNode)
                    {
                        
                        targetResourceNode.OnHit.Invoke(current_Tool); //THIS IS IMPORTANT
                        
                    }
                }
            }

        }
    }
    IEnumerator Co_Cooldown()
    {
        canUse = false;
        DetectResourceNode();
        Stamina.instance.StaminaDecreased(current_Tool.so_Tool.staminaCost); //temporary, will imrpove in future
        yield return new WaitForSeconds(current_Tool.so_Tool.useRate);
        canUse = true;
    }

    #region Buttons UI
    public void OnUseButtonPressed()
    {
        Use();
    }

    public void OnHammerButtonPressed()
    {
        // Switch to Current Tool to Hammer 
    }

    public void OnBareHandsButtonPressed()
    {
        // Switch to Current Tool to Bare Hands
    }

    public void OnPickaxeButtonPressed()
    {
        current_Tool = ToolManager.instance.tools[0]; // Pickaxe
        //GameObject child = UIManager.instance.pickaxeButton.transform.GetChild(0).gameObject;
        // Debug.Log("This is child: " + child);
        OnSwitchPress(0); // temporary
    }

    public void OnBoloButtonPressed()
    {
        // Switch Current Tool to Bolo
    }

    public void OnAxeButtonPressed()
    {
        current_Tool = ToolManager.instance.tools[1]; // Axe
        // GameObject child = UIManager.instance.axeButton.transform.GetChild(0).gameObject;
        // child.SetActive(true);
        OnSwitchPress(1); // temporary
    }

    public void OnSwitchPress(int id)
    {
        
        GameObject child;
        for (int i = 0; i < UIManager.instance.toolButtons.Count; i++)
        {
            if (id == i)
            {
                child = UIManager.instance.toolButtons[id].transform.GetChild(0).gameObject;
                child.SetActive(true);
            }
            else
            {
                child = UIManager.instance.toolButtons[i].transform.GetChild(0).gameObject;
                child.SetActive(false);
            }
        }
        ToolManager.OnToolChanged.Invoke(current_Tool);
    }
    #endregion
}
