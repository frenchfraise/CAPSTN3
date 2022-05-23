using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


[System.Serializable]
public class ToolUsed : UnityEvent<float> { }
public class ToolCaster : MonoBehaviour
{
    public Tool current_Tool;
    public bool canUse = true;
    public ToolUsed onToolUsed = new ToolUsed();
    //Feel free to change anything but the SO_Tool reference
    //To make use of the resoucenode, make sure that the OnHit Unity Event of the resource node will be invoked
    public void OnEnable()
    {
        if (GetComponent<Stamina>())
        {
            onToolUsed.AddListener(GetComponent<Stamina>().ModifyStamina);
        }
    }

    public void OnDisable()
    {
        if (GetComponent<Stamina>())
        {
            onToolUsed.RemoveListener(GetComponent<Stamina>().ModifyStamina);
        }
    }
    public void Use()
    {
        if (canUse)
        {
            canUse = false;
            DetectResourceNode();
            onToolUsed.Invoke(current_Tool.so_Tool.staminaCost);
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

    public void DetectInteractibles() //TEMPORARY DETECTION, CHANGE AS YOU SEE FIT
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)transform.position, 3f);


        if (collider[0].gameObject != gameObject)
        {
            if (collider[0] != null)
            {
               
                //Debug.Log("HIT " + hit.gameObject.name);
                InteractibleObject targetResourceNode = collider[0].gameObject.GetComponent<InteractibleObject>(); //THIS IS IMPORTANT

                if (targetResourceNode)
                {
              
                    targetResourceNode.onInteract.Invoke(); //THIS IS IMPORTANT

                }
            }
        }

        
    }
    IEnumerator Co_Cooldown()
    {
  
        yield return new WaitForSeconds(current_Tool.so_Tool.useRate);
        canUse = true;
    }

    #region Buttons UI
    public void OnUseButtonPressed()
    {
        Use();
    }

    //temporary move to own scripts
    public void OnInteractButtonPressed()
    {
        DetectInteractibles();
    }

    public void OnInventoryButtonPressed()
    {
        //if ()
        UIManager.instance.inventoryUI.gameObject.SetActive(true);
    }
    //temporary move to own scripts
    public void OnToolButtonPressed(int index)
    {
        current_Tool = ToolManager.instance.tools[index];
        OnSwitchPress(index);
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
