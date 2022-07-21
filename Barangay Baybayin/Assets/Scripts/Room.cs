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
    [SerializeField] private Passageway connectedToPassageway;

    public void GetPassagewayInfos(
                                out Transform p_playerDestinationPosition,

                                out Passageway p_connectedToPassageway
                                )
    {
     
        p_playerDestinationPosition = playerDestinationPosition;

        p_connectedToPassageway = connectedToPassageway;
    }
}
public class RoomEnteredEvent : UnityEvent<Passageway> { }

public class Room : MonoBehaviour
{
    public int currentRoomID;
    public string roomName;
    public string roomDescription;
   // [SerializeField] public Transform cameraDestinationTransform;
    [SerializeField] public Transform cameraPanLimitUpperRightTransform;
    [HideInInspector] public Vector2 cameraPanLimit;
    [NonReorderable] public List<ResourceNodeDrop> availableResourceNodeDrops = new List<ResourceNodeDrop>(); // populate by node
    [NonReorderable] [SerializeField] private List<ResourceNodeSpawner> resourceNodeSpawners = new List<ResourceNodeSpawner>();

    [NonReorderable] [SerializeField] private List<PassagewayInfo> passagewayInfos = new List<PassagewayInfo>();


    
    public void GetRoomInfos(out string p_roomName, out string p_roomDescription, out List<ResourceNodeDrop> p_availableResourceNodeDrops)
    {
        p_roomName = roomName;
        p_roomDescription = roomDescription;
        p_availableResourceNodeDrops = availableResourceNodeDrops;
    }

    private void OnEnable()
    {
        
        cameraPanLimit = Vector2Abs(transform.position - cameraPanLimitUpperRightTransform.position);

        foreach (ResourceNodeSpawner resourceNodeSpawner in resourceNodeSpawners)
        {
            resourceNodeSpawner.AssignRoom(this);

        }
        foreach (PassagewayInfo passagewayInfo in passagewayInfos)
        {
            Room room = this;
            Transform playerDestinationPosition;
           
            Passageway connectedToPassageway;
            
            passagewayInfo.GetPassagewayInfos(out playerDestinationPosition,
                out connectedToPassageway);
            passagewayInfo.passageway.AssignPassageway(room, 
                                                        playerDestinationPosition,
                                                        transform.position,
                                                        cameraPanLimit,
                                                        connectedToPassageway);
        }
    }
    Vector2 Vector2Abs(Vector2 p_vector2)
    {
        Vector2 answer = new Vector2(Mathf.Abs(p_vector2.x), Mathf.Abs(p_vector2.y));
        return answer;
    }

    private void OnDisable()
    {
        
        
    }
}
