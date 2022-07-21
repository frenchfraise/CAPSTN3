using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Waypoint : MonoBehaviour
{
    public GameObject target;

    public delegate void OnStep();
    public static event OnStep onStepEvent;

    private void Start()
    {
        //a reference to a function
        //registering this on start
        onStepEvent += DuringStep;
    }

    private void OnDisable()
    {
        onStepEvent -= DuringStep;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("colliding");

        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("collided with player!");
            if (onStepEvent != null)
            {
                DuringStep();
            }
        }  
    }

    public void DuringStep()
    {
        //compatible with delegate example
        Debug.Log("during step!");
        GameManager.Instance.player.transform.position = target.transform.position;
    }
}
