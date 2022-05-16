using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceNodeSpawner : MonoBehaviour
{
    public Room room;
    public int savedIndex = 0;
    //public List<ResourceNode> resourceNodes = new List<ResourceNode>();

    private void Start()
    {

        ResourceManager.instance.OnRespawn.AddListener(Spawn);
    }

    private void OnDestroy()
    {

        ResourceManager.instance.OnRespawn.RemoveListener(Spawn);
    }
    bool CheckSpawnAvailability()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)transform.position, 3f);
        foreach (Collider2D hit in collider)
        {

            if (hit.gameObject != gameObject)
            {
                if (hit != null)
                {
                   
                    return false;
                }
            }

        }
        return true;
    }
    void Spawn()
    {
  
        bool test = CheckSpawnAvailability();
      
        if (test)
        {
          
            float chanceRolled = Random.Range(0, 100);
            savedIndex = 0;
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
