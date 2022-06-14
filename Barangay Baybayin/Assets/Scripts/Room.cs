using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PassagewayInfo
{
    [SerializeField] private string name;
    [SerializeField] public Passageway passageway;
    [SerializeField] private Transform playerDestinationPosition;
    [SerializeField] private Vector2 cameraDestinationPosition;
    [SerializeField] private Vector2 cameraPanLimit;
    [SerializeField] private Passageway connectedToPassageway;

    public void GetPassagewayInfos(
                                out Transform p_playerDestinationPosition,
                                out Vector2 p_cameraDestinationPosition,
                                out Vector2 p_cameraPanLimit,
                                out Passageway p_connectedToPassageway
                                )
    {
        //p_room = room;
        p_playerDestinationPosition = playerDestinationPosition;
        p_cameraDestinationPosition = cameraDestinationPosition;
        p_cameraPanLimit = cameraPanLimit;
        p_connectedToPassageway = connectedToPassageway;
    }
}
public class RoomEnteredEvent : UnityEvent<Passageway> { }

public class Room : MonoBehaviour
{
    public string roomName;
    public string roomDescription;
    [SerializeField] private Vector2 cameraDestinationPosition;
    [SerializeField] private Vector2 cameraPanLimit;
    public List<ResourceNodeDrop> availableResourceNodeDrops = new List<ResourceNodeDrop>(); // populate by node
    [SerializeField] private List<ResourceNodeSpawner> resourceNodeSpawners = new List<ResourceNodeSpawner>();

    [SerializeField] private List<PassagewayInfo> passagewayInfos = new List<PassagewayInfo>();

    public RoomEnteredEvent onRoomEnteredEvent = new RoomEnteredEvent();
    
    public void GetRoomInfos(out string p_roomName, out string p_roomDescription, out List<ResourceNodeDrop> p_availableResourceNodeDrops)
    {
        p_roomName = roomName;
        p_roomDescription = roomDescription;
        p_availableResourceNodeDrops = availableResourceNodeDrops;
    }

    private void OnEnable()
    {
        onRoomEnteredEvent.AddListener(UIManager.instance.roomInfoUI.RoomEntered);
        foreach (ResourceNodeSpawner resourceNodeSpawner in resourceNodeSpawners)
        {
            resourceNodeSpawner.AssignRoom(this);

        }
        foreach (PassagewayInfo passagewayInfo in passagewayInfos)
        {
            Room room = this;
            Transform playerDestinationPosition;
            Vector2 cameraDestinationPosition;
            Vector2 cameraPanLimit;
            Passageway connectedToPassageway;
            passagewayInfo.GetPassagewayInfos(out playerDestinationPosition,
                out cameraDestinationPosition,
                out cameraPanLimit,
                out connectedToPassageway);
            passagewayInfo.passageway.AssignPassageway(room, 
                                                        playerDestinationPosition,
                                                        cameraDestinationPosition,
                                                        cameraPanLimit,
                                                        connectedToPassageway);
        }
    }

    private void OnDisable()
    {
        if (UIManager.instance)
        {
            onRoomEnteredEvent.RemoveListener(UIManager.instance.roomInfoUI.RoomEntered);
        }
        
        
    }
}
