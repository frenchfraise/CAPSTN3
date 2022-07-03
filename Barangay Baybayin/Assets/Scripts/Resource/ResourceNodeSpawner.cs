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
            //Debug.Log(hit.gameObject.name + " - 1 " + hit.gameObject.tag);
            if (hit.gameObject != gameObject)
            {
                //Debug.Log(hit.gameObject.name + " - 2 " + hit.gameObject.tag);
                //Debug.Log("D " + hit.gameObject.name);
                if (!hit.gameObject.CompareTag("Player"))
                {
                    //Debug.Log(hit.gameObject.name + " - 3 " + hit.gameObject.tag);
                    if (hit != null)
                    {
                        //Debug.Log(hit.gameObject.name + " - 4 " + hit.gameObject.tag);
                        if (hit.gameObject.CompareTag("Nodes"))
                        {
                            //Debug.Log("RA");
                            Despawn(hit.gameObject.GetComponent<ResourceNode>());
                            //Destroy(hit.gameObject);
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
            //Debug.Log(gameObject.name + " - 5 " + isSpawnAvailable);
            if (isSpawnAvailable)
            {
                //Debug.Log(gameObject.name + " SPAN SPAN " + isSpawnAvailable);
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

                //System.Type m = room.availableResourceNodeDrops[savedIndex].resourceNode.GetType();
                //System.Type m1 = m.GetGenericTypeDefinition();
                //System.Type[] typeArgs = { typeof(string), typeof(ResourceNode) };
                //System.Type constructed = m1.MakeGenericType(typeArgs);
                //System.Type m = .GetProperty("Value").PropertyType;
                //var ra = typeof(ResourceNode);
                //var test = ra.MakeGenericType(m);
                //Spawn<constructed>(savedIndex);
                //Debug.Log(room.gameObject.name + " - ");
                TempSpawn(room.availableResourceNodeDrops[savedIndex].resourceNode,savedIndex);
            }
        }
        void TempSpawn(ResourceNode newResourceNode, int savedIndex)
        {
            //This is wrong, dont do this, Asking sir Dale
         
            
            if (newResourceNode is TreeVariantOneNode)
            {
                TreeVariantOneNode nResourceNode = TreeVariantOneNodePool.pool.Get();//<TreeVariantOneNode>.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is TreeVariantTwoNode)
            {
                TreeVariantTwoNode nResourceNode = TreeVariantTwoNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is TreeVariantThreeNode)
            {
                TreeVariantTwoNode nResourceNode = TreeVariantTwoNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is OreVariantOneNode)
            {
                TreeVariantTwoNode nResourceNode = TreeVariantTwoNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is OreVariantTwoNode)
            {
                TreeVariantTwoNode nResourceNode = TreeVariantTwoNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is OreVariantThreeNode)
            {
                TreeVariantTwoNode nResourceNode = TreeVariantTwoNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is BambooVariantOneNode)
            {
                BambooVariantOneNode nResourceNode = BambooVariantOneNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is BambooVariantTwoNode)
            {
                BambooVariantTwoNode nResourceNode = BambooVariantTwoNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is BambooVariantThreeNode)
            {
                BambooVariantThreeNode nResourceNode = BambooVariantThreeNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is NipaLeavesVariantOneNode)
            {
                NipaLeavesVariantOneNode nResourceNode = NipaLeavesVariantOneNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is NipaLeavesVariantTwoNode)
            {
                NipaLeavesVariantTwoNode nResourceNode = NipaLeavesVariantTwoNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is NipaLeavesVariantThreeNode)
            {
                NipaLeavesVariantThreeNode nResourceNode = NipaLeavesVariantThreeNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is HerbVariantOneNode)
            {
                HerbVariantOneNode nResourceNode = HerbVariantOneNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is HerbVariantTwoNode)
            {
                HerbVariantTwoNode nResourceNode = HerbVariantTwoNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
            else if (newResourceNode is HerbVariantThreeNode)
            {
                HerbVariantThreeNode nResourceNode = HerbVariantThreeNodePool.pool.Get();
                nResourceNode.transform.position = transform.position;
                nResourceNode.InitializeValues();
            }
        

        }
        //void Spawn<T>(int savedIndex) where T : MonoBehaviour
        //{
        //    //if ((System.Type)T is typeof(TreeVariantOneNode))
        //    //{

        //    //}
  
        //    T genericObject = GenericObjectPool<T>.pool.Get();
      
        //    ResourceNode newResourceNode = genericObject as ResourceNode;
        //    newResourceNode.transform.position = transform.position;

        //}


    }
    void Despawn(ResourceNode p_newResourceNode)
    {
        //This is wrong, dont do this, Asking sir Dale
        //This is wrong, dont do this, Asking sir Dale


        if (p_newResourceNode is TreeVariantOneNode)
        {
            TreeVariantOneNode nResourceNode = TreeVariantOneNodePool.pool.Get();//<TreeVariantOneNode>.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is TreeVariantTwoNode)
        {
            TreeVariantTwoNode nResourceNode = TreeVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is TreeVariantThreeNode)
        {
            TreeVariantTwoNode nResourceNode = TreeVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is OreVariantOneNode)
        {
            TreeVariantTwoNode nResourceNode = TreeVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is OreVariantTwoNode)
        {
            TreeVariantTwoNode nResourceNode = TreeVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is OreVariantThreeNode)
        {
            TreeVariantTwoNode nResourceNode = TreeVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is BambooVariantOneNode)
        {
            BambooVariantOneNode nResourceNode = BambooVariantOneNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is BambooVariantTwoNode)
        {
            BambooVariantTwoNode nResourceNode = BambooVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is BambooVariantThreeNode)
        {
            BambooVariantThreeNode nResourceNode = BambooVariantThreeNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is NipaLeavesVariantOneNode)
        {
            NipaLeavesVariantOneNode nResourceNode = NipaLeavesVariantOneNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is NipaLeavesVariantTwoNode)
        {
            NipaLeavesVariantTwoNode nResourceNode = NipaLeavesVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is NipaLeavesVariantThreeNode)
        {
            NipaLeavesVariantThreeNode nResourceNode = NipaLeavesVariantThreeNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is HerbVariantOneNode)
        {
            HerbVariantOneNode nResourceNode = HerbVariantOneNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is HerbVariantTwoNode)
        {
            HerbVariantTwoNode nResourceNode = HerbVariantTwoNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }
        else if (p_newResourceNode is HerbVariantThreeNode)
        {
            HerbVariantThreeNode nResourceNode = HerbVariantThreeNodePool.pool.Get();
            nResourceNode.transform.position = transform.position;
            nResourceNode.DeinitializeValues();
        }


    }

}
