using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnBedInteract : UnityEvent { }
public class Bed : InteractibleObject
{
    public OnBedInteract onBedInteracted = new OnBedInteract();

    protected override void OnEnable()
    {
        base.OnEnable();
        onBedInteracted.AddListener(TimeManager.instance.EndDay);
 
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if (TimeManager.instance)
        {
            onBedInteracted.RemoveListener(TimeManager.instance.EndDay);
        }
        
        
    }
    protected override void OnInteract()
    {
        onBedInteracted.Invoke();
   

    }
}
