using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class ResourceNodeHitEvent : UnityEvent<List<SO_ResourceNode> , int , int, UnityEvent > { }
public class ResourceNode : Unit
{
 
    public SO_ResourceNode so_ResourceNode;

    [Header("Item")]
    public Item itemPrefab;

    public Sprite hintSprite;

    public int levelRequirement;

    [NonReorderable] public List<ResourceDrop> resourceDrops = new List<ResourceDrop>(); //chance

    public ResourceNodeHitEvent OnResourceNodeHitEvent = new ResourceNodeHitEvent();

    protected override void OnEnable()
    {
        Health health = GetComponent<Health>();
        health.OnDeathEvent.AddListener(RewardResource);
 
        OnResourceNodeHitEvent.AddListener(Hit);
    }

    protected override void OnDisable()
    {
        Health health = GetComponent<Health>();
        health.OnDeathEvent.RemoveListener(RewardResource);
   
        OnResourceNodeHitEvent.RemoveListener(Hit);
    }

    public virtual void Hit( List<SO_ResourceNode> p_useForResourceNode,int p_craftLevel, int p_currentDamage, UnityEvent p_eventCallback) 
    {
        //Debug.Log("1 " + p_useForResourceNode + " - " + p_craftLevel + " - " + p_currentDamage + " - ");
        foreach(SO_ResourceNode useForResourceNode in p_useForResourceNode)
        {
            if (useForResourceNode == so_ResourceNode)
            {

                //if (p_craftLevel >= levelRequirement)
                //{

                Health health = GetComponent<Health>();

                health.onHealthModifyEvent.Invoke(-p_currentDamage);

                p_eventCallback.Invoke();

                //}
            }
        }
    
    }

    public void RewardResource()
    {
        int chosenIndex = Random.Range(0, resourceDrops.Count);
        
        ResourceDrop chosenResourceDrop = resourceDrops[chosenIndex];
        int rewardAmount = Random.Range(chosenResourceDrop.minAmount, chosenResourceDrop.maxAmount);
        for (int i=0; i<rewardAmount; i++)
        {
            Item newItem = Instantiate(itemPrefab);
            
            newItem.transform.position = (Vector2) transform.position;
            newItem.startPosition = (Vector2) transform.position;
            newItem.so_Item = chosenResourceDrop.so_Item;
            newItem.GetComponent<SpriteRenderer>().sprite = chosenResourceDrop.so_Item.icon;
        }
        
    }

    //public override void InitializeValues()
    //{
    //    base.InitializeValues();
    //    StartCoroutine(co_test());

    //}
}
