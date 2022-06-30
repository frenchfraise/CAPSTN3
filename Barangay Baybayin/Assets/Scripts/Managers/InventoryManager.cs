using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    private void Awake()
    {
        
        if (_instance != null)
        {
            //Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

        }
    }
    public void OnEnable()
    {

        //UIManager.instance.inventoryUI.GenerateInventoryPageUIs();
    }
    public static ItemData GetItem(SO_Item p_item)
    {
        for (int iii = 0; iii < InventoryManager.instance.inventoryPages.Count; iii++)
        {
            for (int ii = 0; ii < InventoryManager.instance.inventoryPages[iii].itemCategories.Count; ii++)
            {
                Debug.Log(InventoryManager.instance.inventoryPages[iii].itemCategories[ii].name.ToString());
                for (int i = 0; i < InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items.Count;)
                {
                    if (InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items[i].so_Item == p_item)
                    {


                        Debug.Log(p_item + " - GOT IT - " + InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items[i].so_Item.ToString());
                        return InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items[i];
                    }
                    Debug.Log(p_item + " - - " + InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items[i].so_Item.ToString());
                    i++;
                    if (i >= InventoryManager.instance.inventoryPages.Count)
                    {
                        //Loop finished but didnt find any matching item
                        Debug.Log("FAILED TO FIND ITEM " + p_item.name + " BECAUSE COULD NOT FIND SO_Item IN INVENTORY'S ITEM CATEGORY'S ITEM WITH MATCHING NAME");
                    }
                }
            }
        }
        return null;

    }
    public static void AddItem(SO_Item p_item, int p_amount)
    {
        for (int iii = 0; iii < InventoryManager.instance.inventoryPages.Count; iii++)
        {
            for (int ii = 0; ii < InventoryManager.instance.inventoryPages[iii].itemCategories.Count; ii++)
            {

                for (int i = 0; i < InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items.Count;)
                {
                    if (InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items[i].so_Item == p_item)
                    {

                        InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items[i].amount += p_amount;
                        InventoryManager.instance.inventoryPages[iii].itemCategories[ii].items[i].UpdateText(); //temporary

                        return;
                    }
                    i++;
                    if (i >= InventoryManager.instance.inventoryPages.Count)
                    {
                        //Loop finished but didnt find any matching item
                        Debug.Log("FAILED TO ADD ITEM " + p_item.name + " BECAUSE COULD NOT FIND ITEM IN INVENTORY WITH MATCHING NAME");
                    }
                }



            }
        }
     
    }

    public static void ReduceItems(List<ItemData> p_itemDatas, List<int> p_amount, UnityEvent p_eventCallback = null)
    {
        int itemsFound = 0;
        for (int i = 0; i < p_itemDatas.Count; i++)
        {
            ItemData itemData = p_itemDatas[i];
            if (itemData != null)
            {
                if (itemData.amount >= p_amount[i])
                {
                    itemsFound++;

                }
                else
                {

                }
            }

        }
        if (itemsFound == p_itemDatas.Count) //this means it has everything
        {
            for (int i = 0; i < p_itemDatas.Count; i++)
            {
                ItemData itemData = p_itemDatas[i];
                //Check quest requirement type


                if (itemData != null)
                {
                    if (itemData.amount >= p_amount[i])
                    {
                        itemData.amount -= p_amount[i];

                    }
                    else
                    {

                    }
                }

            }
            p_eventCallback.Invoke();
        }
    }
}
