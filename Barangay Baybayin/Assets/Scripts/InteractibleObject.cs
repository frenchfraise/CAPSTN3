using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnInteract : UnityEvent { }
public class InteractibleObject : MonoBehaviour
{
    public OnInteract onInteract = new OnInteract();

    protected bool canInteract;
    protected virtual void OnEnable()
    {
        onInteract.AddListener(OnInteract);
        canInteract = true;
    }

    protected virtual void OnDisable()
    {
        onInteract.RemoveListener(OnInteract);
    }
    protected virtual void OnInteract()
    {

    }
}
