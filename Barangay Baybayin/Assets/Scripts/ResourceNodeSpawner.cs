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
            TimeManager.onDayChangedEvent.AddListener(Spawn);
        }

 
    }

    private void OnDestroy()
    {

        if (TimeManager.instance)
        {
            TimeManager.onDayChangedEvent.RemoveListener(Spawn);
        }

    }
    bool CheckSpawnAvailability()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)transform.position, 3f);
        foreach (Collider2D hit in collider)
        {
         
            if (hit.gameObject != gameObject)
            {
                if (!hit.gameObject.CompareTag("Player"))
                {
                    if (hit != null)
                    {

                        return false;
                    }
                }
                
            }

        }
        return true;
    }
    void Spawn(int p_day)
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
                if (chanceRolled  <= currentCount)
                {
                    //within chance range
                    savedIndex = i;
                    break;
                }
                
            }
            PoolableObject chosenResourceNode = room.availableResourceNodeDrops[savedIndex].resourceNode;
            PoolableObject newResourceNode = ObjectPoolManager.GetPool(chosenResourceNode).pool.Get();
            newResourceNode.transform.position = transform.position;
        }


     
    }


}
