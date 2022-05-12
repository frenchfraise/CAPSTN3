using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Room : MonoBehaviour
{
    [SerializeField] private List<Passageway> passageways = new List<Passageway>();

    private ObjectRequirer objectRequirer;

    public event Action OnRoomDiscovered;
    public event Action OnRoomCleared;

    public void Awake()
    {
        objectRequirer = GetComponent<ObjectRequirer>();
    }

    public List<Passageway> GetPassageways()
    {
        return passageways;
    }

    public bool HasPassagewaysForRequiredDirections(List<CardinalDirection> p_dir)
    {
        //Check if all directions needed is present, if one of them is missing, this is not the right room
        foreach(CardinalDirection currentPassageway in p_dir)
        {
            for (int i=0; i < passageways.Count; )
            {
                if (currentPassageway == passageways[i].cardinalDirection) //Passageway has the direction
                {
                    break; //move on to the next cardinal direction
                }
                i++;
                if (i >= passageways.Count) //If loop reaches end and wasnt able to find any passageways with the same direction of list
                {
                    return false;
                }
            
            }
          
        }
        return true;
    }
    public List<Passageway> GetSurroundingRoomsThatAreNotOccupied()
    {
        List<Passageway> unoccupied = new List<Passageway>();
        foreach (Passageway currentPassageway in passageways) // Loop through directions in list
        {
            if (!currentPassageway.isConnectedToAnotherPassageway) //If it isnt connected already
            {
                unoccupied.Add(currentPassageway);
                
            }
        }

        return unoccupied;//It isnt connected already
    }
    public Passageway MatchPassagewayDirection(CardinalDirection p_direction)
    {
        foreach (Passageway currentPassageway in passageways) // Loop through directions in list
        {
            if (currentPassageway.cardinalDirection == p_direction) //Find list that matches direction
            {
                return currentPassageway;
            }
        }
        return null;
    }

    public void OnEnable()
    {
        foreach (Passageway currentPassageways in passageways)
        {
            currentPassageways.OnFirstTimeEntered += RoomDiscovered;
            OnRoomDiscovered += currentPassageways.Close;
            OnRoomCleared += currentPassageways.Open;

            

        }
        objectRequirer.OnAllRequirementsMet += RoomCleared;
        OnRoomDiscovered += objectRequirer.StartRequiring;
    }

    public void OnDisable()
    {
        foreach (Passageway currentPassageways in passageways)
        {
            currentPassageways.OnFirstTimeEntered -= RoomDiscovered;
            OnRoomDiscovered -= currentPassageways.Close;
            OnRoomCleared -= currentPassageways.Open;

        }
        objectRequirer.OnAllRequirementsMet -= RoomCleared;
        OnRoomDiscovered -= objectRequirer.StartRequiring;
        //OnRoomDiscovered -= objectRequirer.OnStartRequiring;
    }

    public void RoomDiscovered()
    {
        Debug.Log("ROOM DESIC");
        OnRoomDiscovered.Invoke();
    }
    public void RoomCleared()
    {
        OnRoomCleared.Invoke();
    }
 

}
