using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    private Transform aim;
    private void Awake()
    {
        aim = GetComponent<PlayerJoystick>().aim;
    }
    void DetectInteractibles() 
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)aim.position, 3f);
        

        foreach (Collider2D hit in collider)
        {
            Debug.Log(collider[0].gameObject.name);
            if (hit.gameObject != gameObject)
            {
             
                if (hit != null)
                {

                    InteractibleObject targetInteractibleObject = hit.gameObject.GetComponent<InteractibleObject>();

                    if (targetInteractibleObject)
                    {
                   
                        targetInteractibleObject.onInteract.Invoke();
                        break;


                    }
                }
            }
        }
       


    }
  
    public void OnInteractButtonPressed()
    {
        DetectInteractibles();
    }
}
