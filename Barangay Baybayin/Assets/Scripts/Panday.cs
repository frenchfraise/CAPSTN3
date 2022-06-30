using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PandaySpokenToEvent : UnityEvent { }
public class Panday : InteractibleObject
{
    //[SerializeField]
    //private int index;
    public static PandaySpokenToEvent onPandaySpokenToEvent = new PandaySpokenToEvent();
    protected override void OnInteract()
    {
        onPandaySpokenToEvent.Invoke();
       
    }
}
