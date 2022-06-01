using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BedInteractedEvent : UnityEvent { }
public class Bed : InteractibleObject
{
    public BedInteractedEvent onBedInteractedEvent = new BedInteractedEvent();

   
    protected override void OnEnable()
    {
        base.OnEnable();
        onBedInteractedEvent.AddListener(TimeManager.instance.EndDay);
 
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if (TimeManager.instance)
        {
            onBedInteractedEvent.RemoveListener(TimeManager.instance.EndDay);
        }
        
        
    }
    protected override void OnInteract()
    {
        onBedInteractedEvent.Invoke();
   

    }
}
