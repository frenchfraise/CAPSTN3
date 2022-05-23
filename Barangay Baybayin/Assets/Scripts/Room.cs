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
    [SerializeField] private Passageway connectedToPassageway;

    public void GetPassagewayInfos(
                                out Transform p_playerDestinationPosition,
                                out Vector2 p_cameraDestinationPosition,
                                out Passageway p_connectedToPassageway)
    {
        //p_room = room;
        p_playerDestinationPosition = playerDestinationPosition;
        p_cameraDestinationPosition = cameraDestinationPosition;
        p_connectedToPassageway = connectedToPassageway;
    }
}
public class OnRoomEntered : UnityEvent<string, string, List<ResourceNodeDrop>, Vector3> { }

public class Room : MonoBehaviour
{
    [SerializeField] private string roomName;
    [SerializeField] private string roomDescription;
    public List<ResourceNodeDrop> availableResourceNodeDrops = new List<ResourceNodeDrop>(); // populate by node
    [SerializeField] private List<ResourceNodeSpawner> resourceNodeSpawners = new List<ResourceNodeSpawner>();

    [SerializeField] private List<PassagewayInfo> passagewayInfos = new List<PassagewayInfo>();

    public OnRoomEntered onRoomEntered = new OnRoomEntered();
    
    public void GetRoomInfos(out string p_roomName, out string p_roomDescription, out List<ResourceNodeDrop> p_availableResourceNodeDrops)
    {
        p_roomName = roomName;
        p_roomDescription = roomDescription;
        p_availableResourceNodeDrops = availableResourceNodeDrops;
    }

    private void OnEnable()
    {
        onRoomEntered.AddListener(UIManager.instance.roomInfoUI.RoomEntered);
        foreach (ResourceNodeSpawner resourceNodeSpawner in resourceNodeSpawners)
        {
            resourceNodeSpawner.AssignRoom(this);

        }
        foreach (PassagewayInfo passagewayInfo in passagewayInfos)
        {
            Room room = this;
            Transform playerDestinationPosition;
            Vector2 cameraDestinationPosition;
            Passageway connectedToPassageway;
            passagewayInfo.GetPassagewayInfos(out playerDestinationPosition,
                out cameraDestinationPosition,
                out connectedToPassageway);
            passagewayInfo.passageway.AssignPassageway(room, 
                                                        playerDestinationPosition,
                                                        cameraDestinationPosition,
                                                        connectedToPassageway);
        }
    }

    private void OnDisable()
    {
        if (UIManager.instance)
        {
            onRoomEntered.RemoveListener(UIManager.instance.roomInfoUI.RoomEntered);
        }
        
        
    }
}
