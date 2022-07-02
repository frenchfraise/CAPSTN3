using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Passageway : MonoBehaviour
{

    [HideInInspector] public Room room;
    private Transform playerDestinationPosition;
    public Vector2 cameraDestinationPosition { get; private set; }
    public Vector2 cameraPanLimit { get; private set; }
    private Passageway connectedToPassageway;

    public void AssignPassageway(Room p_room,
                                 Transform p_playerDestinationPosition,
                                 Vector2 p_cameraDestinationPosition,
                                 Vector2 p_cameraPanLimit,
                                 Passageway p_connectedToPassageway)
    {
        room = p_room;
        playerDestinationPosition = p_playerDestinationPosition;
        cameraDestinationPosition = p_cameraDestinationPosition;
        cameraPanLimit = p_cameraPanLimit;


        connectedToPassageway = p_connectedToPassageway;
    }
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
  
            collision.gameObject.transform.position = connectedToPassageway.playerDestinationPosition.position;
            
            PlayerManager.onRoomEnteredEvent.Invoke(connectedToPassageway);
            PlayerManager.onUpdateCurrentRoomIDEvent.Invoke(connectedToPassageway.room.currentRoomID);

        }
       

    }


}
