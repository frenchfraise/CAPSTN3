using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnInteract : UnityEvent { }
public class InteractibleObject : MonoBehaviour
{
    public OnInteract onInteract = new OnInteract();

    protected virtual void OnEnable()
    {
        onInteract.AddListener(OnInteract);
    }

    protected virtual void OnDisable()
    {
        onInteract.RemoveListener(OnInteract);
    }
    protected virtual void OnInteract()
    {

    }
}
