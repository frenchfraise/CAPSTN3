using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjectRequirer : MonoBehaviour
{
    [SerializeField] public int requirementCount;
    //[SerializeField] public List<ObjectRequirement> requirements = new List<ObjectRequirement>();
    public Action OnStartRequiring;
    public Action OnAllRequirementsMet;

    public void Awake()
    {
        //foreach (ObjectRequirement currentObjectRequirement in requirements)
        //{
        //    currentObjectRequirement.requirer = this;
            
        //}
        //Debug.Log("STAAA");
    }
    public void OnEnable()
    {
        //foreach (ObjectRequirement currentObjectRequirement in requirements)
        //{

        //    if (currentObjectRequirement)
        //    {
        //        currentObjectRequirement.OnRequirementMet += RequirementMet;
        //        OnStartRequiring += currentObjectRequirement.StartRequirement;
        //    }
        //}
    }

    public void StartRequiring()
    {
        OnStartRequiring.Invoke();
    }
    public void RequirementMet(ObjectRequirement p_currentObjectRequirement)
    {
        Debug.Log("REQUIREMENT FOR : "+ p_currentObjectRequirement.gameObject.name  +" - MET");
        requirementCount--;
        //requirements.Remove(p_currentObjectRequirement);
        CheckIfRoomCleared();
    }

    public void CheckIfRoomCleared()
    {
        if (requirementCount <= 0)
        {
            //Room Cleared
            OnAllRequirementsMet.Invoke();
        }
        else
        {
            //Room not cleared
        }
    }
}
