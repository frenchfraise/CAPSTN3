using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public SO_Tool so_Tool;
    public bool canUse;

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

                        targetResourceNode.OnHit.Invoke(this); //THIS IS IMPORTANT

                    }
                }
            }

        }
    }
    IEnumerator Co_Cooldown()
    {
        canUse = false;
        DetectResourceNode();
        Stamina.instance.StaminaDecreased(5); //temporary, will imrpove in future
        yield return new WaitForSeconds(so_Tool.useRate);
        canUse = true;
    }
}
