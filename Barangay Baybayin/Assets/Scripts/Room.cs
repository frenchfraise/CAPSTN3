using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnRoomEntered : UnityEvent<Passageway> { }

public class Room : MonoBehaviour
{
    public string roomName;
    public List<ResourceNodeDrop> availableResourceNodeDrops = new List<ResourceNodeDrop>(); // populate by node
    public OnRoomEntered onRoomEntered = new OnRoomEntered();

    private void OnEnable()
    {
        onRoomEntered.AddListener(UIManager.instance.roomInfoUI.RoomEntered);
    }

    private void OnDisable()
    {
        onRoomEntered.RemoveListener(UIManager.instance.roomInfoUI.RoomEntered);
    }
}
