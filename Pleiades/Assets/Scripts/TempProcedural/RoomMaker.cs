using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum RoomDirection
{
    left = 0,
    right = 1,
    front = 2,
    back = 3,

}
[System.Serializable]
public class DirectionRoom
{
    [SerializeField] public string directionName;
    [SerializeField] public List<GameObject> rooms = new List<GameObject>();
}
public class RoomMaker : MonoBehaviour
{

   
    //Vector3 currentRoom;
    Room currentRoom;
    public static RoomMaker instance;
    public Action<GameObject, RoomDirection,Vector3> OnNewRoom;

    public List<DirectionRoom> direction = new List<DirectionRoom>();

    private void Awake()
    {
        instance = this;
    }
    //private void OnEnable()
    //{
    //    OnNewRoom += NewRoom;
    //}

    //private void OnDisable()
    //{
    //    OnNewRoom -= NewRoom;
    //}

    //void NewRoom(GameObject roomThatPlayerIsOn, RoomDirection dire, Vector3 spawnPosition)
    //{
    //    Debug.Log((int)dire + "  ");// + rooms[(int)dire].Count);
    //    int roomDire = (int)dire;
    //    Debug.Log(direction[3].rooms.Count);
    //    int chosenRoomIndex = UnityEngine.Random.Range(0, direction[roomDire].rooms.Count);
    //    GameObject newRoomInstance = Instantiate(direction[roomDire].rooms[chosenRoomIndex]);
    //    currentRoom = roomThatPlayerIsOn.GetComponent<Room>();
    //    Room newRoomSpawnDirection = newRoomInstance.GetComponent<Room>();
    //    Vector3 currentRoomConnector = spawnPosition;// Vector3.zero;
    //    Debug.Log(" ra " + currentRoom.transform.position.ToString());
    //    if (RoomDirection.left == dire)
    //    {
          
           
           
    //        //get new room's connector
    //        for (int i = 0; i < newRoomSpawnDirection.connectorPoints.Count; i++)
    //        {
    //            if (newRoomSpawnDirection.connectorPoints[i].entranceToWhatRoom == RoomDirection.right)
    //            {
    //                Vector3 newRoomConnectorDistanceFromNewRoomOrigin = newRoomInstance.transform.position - newRoomSpawnDirection.connectorPoints[i].transform.position;

    //                newRoomSpawnDirection.connectorPoints[i].canNewRoom = false;
    //                Vector3 currentRoomConnectorDistanceFromCurrentRoomOrigin = currentRoomConnector - currentRoom.transform.position;

    //                Vector3 newPos = currentRoomConnectorDistanceFromCurrentRoomOrigin + newRoomConnectorDistanceFromNewRoomOrigin;
                  
    //                newRoomInstance.transform.position = currentRoom.transform.position + newPos;
    //                //= spawnPosition.position;//currentRoom + new Vector3(50, 0, 50);
    //            }
    //        }
            
          
    //    }
    //    else if (RoomDirection.right == dire)
    //    {
    //        //get new room's connector
    //        for (int i = 0; i < newRoomSpawnDirection.connectorPoints.Count; i++)
    //        {
    //            if (newRoomSpawnDirection.connectorPoints[i].entranceToWhatRoom == RoomDirection.left)
    //            {
    //                Vector3 newRoomConnectorDistanceFromNewRoomOrigin = newRoomInstance.transform.position - newRoomSpawnDirection.connectorPoints[i].transform.position;

    //                newRoomSpawnDirection.connectorPoints[i].canNewRoom = false;
    //                Vector3 currentRoomConnectorDistanceFromCurrentRoomOrigin = currentRoomConnector - currentRoom.transform.position;

    //                Vector3 newPos = currentRoomConnectorDistanceFromCurrentRoomOrigin + newRoomConnectorDistanceFromNewRoomOrigin;
                   
    //                newRoomInstance.transform.position = currentRoom.transform.position + newPos;
    //                //= spawnPosition.position;//currentRoom + new Vector3(50, 0, 50);
    //            }
    //        }
    //        //newRoomInstance.transform.position = currentRoom + new Vector3(-50, 0, 0);
    //    }
    //    else if (RoomDirection.front == dire)
    //    {
    //        //get new room's connector
    //        for (int i = 0; i < newRoomSpawnDirection.connectorPoints.Count; i++)
    //        {
    //            if (newRoomSpawnDirection.connectorPoints[i].entranceToWhatRoom == RoomDirection.back)
    //            {
    //                Vector3 newRoomConnectorDistanceFromNewRoomOrigin = newRoomInstance.transform.position - newRoomSpawnDirection.connectorPoints[i].transform.position;

    //                newRoomSpawnDirection.connectorPoints[i].canNewRoom = false;
    //                Vector3 currentRoomConnectorDistanceFromCurrentRoomOrigin = currentRoomConnector - currentRoom.transform.position;

    //                Vector3 newPos = currentRoomConnectorDistanceFromCurrentRoomOrigin + newRoomConnectorDistanceFromNewRoomOrigin;
                   
    //                newRoomInstance.transform.position = currentRoom.transform.position + newPos;
    //                //= spawnPosition.position;//currentRoom + new Vector3(50, 0, 50);
    //            }
    //        }
    //        //newRoomInstance.transform.position = currentRoom + new Vector3(0, 0, -50);
    //    }
    //    else if (RoomDirection.back == dire)
    //    {
    //        //get new room's connector
    //        for (int i = 0; i < newRoomSpawnDirection.connectorPoints.Count; i++)
    //        {
    //            if (newRoomSpawnDirection.connectorPoints[i].entranceToWhatRoom == RoomDirection.front)
    //            {
    //                Vector3 newRoomConnectorDistanceFromNewRoomOrigin = newRoomInstance.transform.position - newRoomSpawnDirection.connectorPoints[i].transform.position;

    //                newRoomSpawnDirection.connectorPoints[i].canNewRoom = false;
    //                Vector3 currentRoomConnectorDistanceFromCurrentRoomOrigin = currentRoomConnector - currentRoom.transform.position;

    //                Vector3 newPos = currentRoomConnectorDistanceFromCurrentRoomOrigin + newRoomConnectorDistanceFromNewRoomOrigin;
                   
    //                newRoomInstance.transform.position = currentRoom.transform.position + newPos;
    //                //= spawnPosition.position;//currentRoom + new Vector3(50, 0, 50);
    //            }
    //        }
    //        //newRoomInstance.transform.position = currentRoom + new Vector3(0, 0, 50);
    //    }
    //    currentRoom = newRoomInstance.GetComponent<Room>(); 
    //    //currentRoom = newRoomInstance.transform.position;

    //}
}
