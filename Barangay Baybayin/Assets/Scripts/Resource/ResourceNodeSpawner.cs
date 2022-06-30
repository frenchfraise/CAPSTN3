using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceNodeSpawner : MonoBehaviour
{
    private Room room;
 
    public void AssignRoom(Room p_room)
    {
        room = p_room;
    }
    private void Start()
    {
        if (TimeManager.instance)
        {
            TimeManager.onHourChanged.AddListener(Spawn);
        }

 
    }

    private void OnDestroy()
    {

        if (TimeManager.instance)
        {
            TimeManager.onHourChanged.RemoveListener(Spawn);
        }

    }
    bool CheckSpawnAvailability()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)transform.position, 3f);
        foreach (Collider2D hit in collider)
        {
         
            if (hit.gameObject != gameObject)
            {
                //Debug.Log("D " + hit.gameObject.name);
                if (!hit.gameObject.CompareTag("Player"))
                {
                    if (hit != null)
                    {
                        if (hit.gameObject.CompareTag("Nodes"))
                        {
                            //Debug.Log("RA");
                            Destroy(hit.gameObject);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                       
                    }
                }
                
                
            }

        }
        return true;
    }
    void Spawn()
    {
        if (PlayerManager.instance.currentRoomID != room.currentRoomID)
        {
            bool isSpawnAvailable = CheckSpawnAvailability();

            if (isSpawnAvailable)
            {

                float chanceRolled = Random.Range(0, 100);
                int savedIndex = 0;
                float currentCount = 0;
                for (int i = 0; i < room.availableResourceNodeDrops.Count; i++)
                {
                    currentCount += room.availableResourceNodeDrops[i].chance;
                    if (chanceRolled <= currentCount)
                    {
                        //within chance range
                        savedIndex = i;
                        break;
                    }

                }
                //Debug.Log(room.gameObject.name + " - ");
                //PoolableObject chosenResourceNode = room.availableResourceNodeDrops[savedIndex].resourceNode; // URGENT FIX
                //Debug.Log(chosenResourceNode.gameObject.name + " - ");
                //Debug.Log(" - " + ObjectPoolManager.GetPool(chosenResourceNode).gameObject.name);
                //PoolableObject newResourceNode = ObjectPoolManager.GetPool(chosenResourceNode).pool.Get(); //URGENT FIX
                //newResourceNode.transform.position = transform.position;
            }
        }
        


     
    }


}
