using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AddItemEvent : UnityEvent<string, int> { };
public class ReduceItemEvent : UnityEvent<string, int> { };
public class InventoryManager : MonoBehaviour
{

    private static InventoryManager _instance;
    public static InventoryManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InventoryManager>();
            }

            return _instance;
        }
    }

    [NonReorderable] public List<InventoryPageData> inventoryPages = new List<InventoryPageData>();
    public static AddItemEvent onAddItemEvent = new AddItemEvent();
    public static ReduceItemEvent onReduceItemEvent = new ReduceItemEvent();
    
    IEnumerator runningCoroutine;

    private void Awake()
    {
        
        //if (_instance != null)
        //{
        //    //Destroy(gameObject);
        //}
        //else
        //{
            _instance = this;
        onAddItemEvent.AddListener(AddItem);
        onReduceItemEvent.AddListener(ReduceItem);
        //    DontDestroyOnLoad(gameObject);

        //}
    }
    public void OnEnable()
    {

        //UIManager.instance.inventoryUI.GenerateInventoryPageUIs();
    }
    public static ItemData GetItem(string p_item)
    {
   
        for (int iii = 0; iii < InventoryManager.instance.inventoryPages.Count; iii++)
        {
            for (int ii = 0; ii < InventoryManager.instance.inventoryPages[iii].itemCategories.Count; ii++)
            {

                for (int i = 0; i < InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items.Count;)
                {
                    //Debug.Log(p_item);
                    if (InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items[i].so_Item.name == p_item)
                    {
                        //Debug.Log(InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items[i].so_Item.name);
                        return InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items[i];
                    }
                    i++;
                    if (i >= InventoryManager.instance.inventoryPages.Count)
                    {
                        //Loop finished but didnt find any matching item
                        //Debug.Log("FAILED TO ADD ITEM " + p_item + " BECAUSE COULD NOT FIND ITEM IN INVENTORY WITH MATCHING NAME");
                    }
                }
            }
        }
        return null;
    }
    public void AddAllItems(int p_amount)
    {
        for (int iii = 0; iii < InventoryManager.instance.inventoryPages.Count; iii++)
        {
            for (int ii = 0; ii < InventoryManager.instance.inventoryPages[iii].itemCategories.Count; ii++)
            {

                for (int i = 0; i < InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items.Count;)
                {
                    //Debug.Log(p_item);
                    if (InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items[i].so_Item.name != "Food")
                    {
                        ItemData foundItem = InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items[i];
                        //Debug.Log(foundItem);
                        if (runningCoroutine != null)
                        {
                            StopCoroutine(runningCoroutine);
                            runningCoroutine = null;
                        }
                        runningCoroutine = foundItem.itemUI.Co_UpdateText(p_amount);
                        StartCoroutine(runningCoroutine);
                    }
                    i++;
                }
            }
        }

      
    }
    public void AddItem(string p_item, int p_amount)
    {
        if (p_item != "Food")
        {
            ItemData foundItem = GetItem(p_item);
            //Debug.Log(foundItem);
            if (runningCoroutine != null)
            {
                StopCoroutine(runningCoroutine);
                runningCoroutine = null;
            }
            runningCoroutine = foundItem.itemUI.Co_UpdateText(p_amount);
            StartCoroutine(runningCoroutine);
        }
        else
        {
           // Debug.Log("Food");
            Food.onAddFood.Invoke(p_amount);
        }
       
    
   
        //MAKE THIS EVENT FOR NOW ITS PLAYERMANAGER


    }

    public void ReduceItem(string p_item, int p_amount)
    {
        ItemData foundItem = GetItem(p_item);
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
        runningCoroutine = foundItem.itemUI.Co_UpdateText(-p_amount);
        StartCoroutine(runningCoroutine);

    }

    //public static void ReduceItems(List<ItemData> p_itemDatas, List<int> p_amount, UnityEvent p_eventCallback = null)
    //{
    //    int itemsFound = 0;
    //    for (int i = 0; i < p_itemDatas.Count; i++)
    //    {
    //        ItemData itemData = p_itemDatas[i];
    //        if (itemData != null)
    //        {
    //            if (itemData.amount >= p_amount[i])
    //            {
    //                itemsFound++;

    //            }
    //            else
    //            {

    //            }
    //        }

    //    }
    //    if (itemsFound == p_itemDatas.Count) //this means it has everything
    //    {
    //        for (int i = 0; i < p_itemDatas.Count; i++)
    //        {
    //            ItemData itemData = p_itemDatas[i];
    //            //Check quest requirement type


    //            if (itemData != null)
    //            {
    //                if (itemData.amount >= p_amount[i])
    //                {
    //                    itemData.amount -= p_amount[i];

    //                }
    //                else
    //                {

    //                }
    //            }

    //        }
    //        p_eventCallback.Invoke();
    //    }
    //}
}
