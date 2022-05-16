using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //FOR TESTING PURPOSES
        if (Input.GetMouseButtonDown(0))
        {
            Use();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            current_Tool = ToolManager.instance.tools[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            current_Tool = ToolManager.instance.tools[1];
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
}
