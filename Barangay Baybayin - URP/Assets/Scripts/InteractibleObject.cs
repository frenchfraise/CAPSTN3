using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractEvent : UnityEvent { }
public class InteractibleObject : MonoBehaviour
{
    public InteractEvent onInteractEvent = new InteractEvent();

    protected bool canInteract;
    public Sprite hintSprite;
    protected virtual void OnEnable()
    {
        onInteractEvent.AddListener(OnInteract);
        canInteract = true;
    }

    protected virtual void OnDisable()
    {
        onInteractEvent.RemoveListener(OnInteract);
    }
    protected virtual void OnInteract()
    {

    }
}
