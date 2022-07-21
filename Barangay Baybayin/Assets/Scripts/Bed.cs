using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BedInteractedEvent : UnityEvent { }
public class Bed : InteractibleObject
{
    public static BedInteractedEvent onBedInteractedEvent = new BedInteractedEvent();
    public Transform spawnTransform;


    private void Awake()
    {

    }
    protected override void OnEnable()
    {
        base.OnEnable();
        
 
    }
    protected override void OnDisable()
    {
        base.OnDisable();
     
    }

    protected override void OnInteract()
    {
        
        onBedInteractedEvent.Invoke();
       
        
    }
}
