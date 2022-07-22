using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    public bool canInteract = true;
    public float delay = 0.5f;
    private Transform aim;
    private void Awake()
    {
        aim = GetComponent<PlayerJoystick>().aim;
    }
    void DetectInteractibles() 
    {
        InteractibleObject targetResourceNode = GetInteractibleObject();
        if (targetResourceNode)
        {
            if (canInteract == false)
            {
                targetResourceNode.onInteractEvent.Invoke();
            }
           
        }

    }
    
    public InteractibleObject GetInteractibleObject()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)aim.position, 3f);
        foreach (Collider2D hit in collider)
        {
            //Debug.Log(collider[0].gameObject.name);
            if (hit != null)
            {
                
                if (hit.gameObject != gameObject)
                {
                
                    if (hit.TryGetComponent<InteractibleObject>(out InteractibleObject interactibleObject))
                    {
                        return interactibleObject;
  
                    }
                }
            }
        }
        ////Means it found none in its direction
        //collider = Physics2D.OverlapCircleAll(transform.position, 10f);
        //foreach (Collider2D hit in collider)
        //{
        //    Debug.Log(collider[0].gameObject.name);
        //    if (hit.gameObject != gameObject)
        //    {
        //        if (hit != null)
        //        {
        //            InteractibleObject targetInteractibleObject = hit.gameObject.GetComponent<InteractibleObject>();
        //            if (targetInteractibleObject)
        //            {
        //                if (targetInteractibleObject.GetType() == typeof(Item))
        //                {
        //                    return targetInteractibleObject;
        //                }
                        

        //            }
        //        }
        //    }
        //}

        return null;
       

    }
    public void OnInteractButtonPressed()
    {
        if (canInteract)
        {
       
            canInteract = false;
           // Debug.Log("PRESSED");
            
            StartCoroutine(Cooldown());
        }
       
    }

    IEnumerator Cooldown()
    {
       // Debug.Log("IT WORKED");
        DetectInteractibles();
        yield return new WaitForSeconds(delay);
        canInteract = true;
       // Debug.Log("IT WORKED 2");
    }
}
