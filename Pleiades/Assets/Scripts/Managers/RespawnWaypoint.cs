using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnWaypoint : MonoBehaviour
{
    public Player player;
    public int currentWaypoint;
    public Transform[] spawnLocs;

    public static RespawnWaypoint Instance;

       private void Start()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }


    public void Respawn(int currentWaypoint)
    {
        player.AddHealth(100);
        player.transform.position = spawnLocs[currentWaypoint].transform.position;
    }
}
