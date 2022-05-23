using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Passageway : MonoBehaviour
{

    private Room room;
    private Transform playerDestinationPosition;
    [HideInInspector] public Vector2 cameraDestinationPosition;
    private Passageway connectedToPassageway;

    public void AssignPassageway(Room p_room,
                                 Transform p_playerDestinationPosition,
                                 Vector2 p_cameraDestinationPosition,
                                 Passageway p_connectedToPassageway)
    {
        room = p_room;
        playerDestinationPosition = p_playerDestinationPosition;
        cameraDestinationPosition = p_cameraDestinationPosition;
        connectedToPassageway = p_connectedToPassageway;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
  
            collision.gameObject.transform.position = connectedToPassageway.playerDestinationPosition.position;
            string roomName;
            string roomDescription;
            List<ResourceNodeDrop> availableResourceNodeDrops;
            connectedToPassageway.room.GetRoomInfos(out roomName, out roomDescription, out availableResourceNodeDrops);
            Vector3 cameraPosition = new Vector3(connectedToPassageway.cameraDestinationPosition.x,
                                                connectedToPassageway.cameraDestinationPosition.y, 
                                                CameraManager.instance.worldCamera.transform.position.z);
            
            connectedToPassageway.room.onRoomEntered.Invoke(
                roomName, 
                roomDescription, 
                availableResourceNodeDrops, 
                cameraPosition);

        }
       

    }


}
