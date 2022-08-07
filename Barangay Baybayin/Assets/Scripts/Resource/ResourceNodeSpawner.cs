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
            //Debug.Log(gameObject.name + hit.gameObject.name + " - 1 " + hit.gameObject.tag);
            if (hit.gameObject != gameObject)
            {
                //Debug.Log(gameObject.name + hit.gameObject.name + " - 2 " + hit.gameObject.tag);
                //Debug.Log(gameObject.name +  "D " + hit.gameObject.name);
                if (!hit.gameObject.CompareTag("Player"))
                {
                    //Debug.Log(gameObject.name + hit.gameObject.name + " - 3 " + hit.gameObject.tag);
                    if (hit != null)
                    {
                        //Debug.Log(gameObject.name + hit.gameObject.name + " - 4 " + hit.gameObject.tag);
                        if (hit.gameObject.CompareTag("Nodes"))
                        {
                            //Debug.Log(gameObject.name + "RA");
                            Despawn(hit.gameObject.GetComponent<ResourceNode>());
                            //Destroy(hit.gameObject);
                            return true;
                        }
                        else
                        {
                            return true;
                        }
                       
                    }
                }
                
                
            }

        }
        return true;
    }
    void Spawn(int p_hour)
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

                TempSpawn(room.availableResourceNodeDrops[savedIndex].resourceNode,savedIndex);
            }
        }
    }

    void TempSpawn(ResourceNode newResourceNode, int savedIndex)
    {
        //This is wrong, dont do this, Asking sir Dale


        if (newResourceNode is TreeVariantOneNode)
        {
            TreeVariantOneNode nResourceNode = TreeVariantOneNodePool.pool.Get();//<TreeVariantOneNode>.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, 0f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is TreeVariantTwoNode)
        {
            TreeVariantTwoNode nResourceNode = TreeVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, 0f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is TreeVariantThreeNode)
        {
            TreeVariantThreeNode nResourceNode = TreeVariantThreeNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, 0f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is OreVariantOneNode)
        {
            OreVariantOneNode nResourceNode = OreVariantOneNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is OreVariantTwoNode)
        {
            OreVariantTwoNode nResourceNode = OreVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is OreVariantThreeNode)
        {
            OreVariantThreeNode nResourceNode = OreVariantThreeNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is BambooVariantOneNode)
        {
            BambooVariantOneNode nResourceNode = BambooVariantOneNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is BambooVariantTwoNode)
        {
            BambooVariantTwoNode nResourceNode = BambooVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is BambooVariantThreeNode)
        {
            BambooVariantThreeNode nResourceNode = BambooVariantThreeNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is NipaLeavesVariantOneNode)
        {
            NipaLeavesVariantOneNode nResourceNode = NipaLeavesVariantOneNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is NipaLeavesVariantTwoNode)
        {
            NipaLeavesVariantTwoNode nResourceNode = NipaLeavesVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is NipaLeavesVariantThreeNode)
        {
            NipaLeavesVariantThreeNode nResourceNode = NipaLeavesVariantThreeNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is HerbVariantOneNode)
        {
            HerbVariantOneNode nResourceNode = HerbVariantOneNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is HerbVariantTwoNode)
        {
            HerbVariantTwoNode nResourceNode = HerbVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is HerbVariantThreeNode)
        {
            HerbVariantThreeNode nResourceNode = HerbVariantThreeNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
        else if (newResourceNode is VetiverLeafVariantOneNode)
        {
            VetiverLeafVariantOneNode nResourceNode = VetiverLeafVariantOneNodePool.pool.Get();
            nResourceNode.transform.position = transform.position + new Vector3(0f, -2.35f, 0f);
            nResourceNode.InitializeValues();
        }
    }
    void Despawn(ResourceNode p_newResourceNode)
    {
        //This is wrong, dont do this, Asking sir Dale
       
        p_newResourceNode.DeinitializeValues();
        


    }

}
