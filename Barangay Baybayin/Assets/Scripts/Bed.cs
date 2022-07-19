using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BedFirstTimeEvent : UnityEvent { }
public class BedInteractedEvent : UnityEvent { }
public class Bed : InteractibleObject
{
    public static BedInteractedEvent onBedInteractedEvent = new BedInteractedEvent();
    public Transform spawnTransform;
    private bool isFirstTime;
    public static BedFirstTimeEvent onBedFirstTimeEvent = new BedFirstTimeEvent();
    private void Awake()
    {
        onBedFirstTimeEvent.AddListener(FirstTime);
        isFirstTime = true;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        
 
    }
    protected override void OnDisable()
    {
        base.OnDisable();
     
    }
    public void FirstTime()
    {
        isFirstTime = false;
        onBedFirstTimeEvent.RemoveListener(FirstTime);
        OnInteract();
    }
    protected override void OnInteract()
    {
        if (!isFirstTime)
        {
            onBedInteractedEvent.Invoke();
        }
        else
        {
            TutorialUI.onRemindTutorialEvent.Invoke("bed");

        }
    }
}
