using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTrigger : Interactables
{
    public int doorToLock;
    public int currentWaypointIndex;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            interactedWith = true;
            PuzzleManager.instance.OnNewRoomEnter(doorToLock);
            doorToLock++;

            PuzzleManager.instance.OnNewRoomEnter(doorToLock);
            RespawnWaypoint.Instance.currentWaypoint = currentWaypointIndex;
        }
    }
}
