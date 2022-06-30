using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TabGroupUI : MonoBehaviour
{
    //protected
    public List<TabButtonUI> tabButtonUIs;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;

    //protected List<Tab> _tabs = new List<Tab>();

    public bool tabSwapsActiveGameObject;
    public GameObject[] gameObjects;
    public List<GameObject> objectsToSwap;

    public PanelGroup panelGroup;

    //[SerializeField]
    //protected

    public TabButtonUI selectedTab;

    public ActionTransform onTabSelectedCallback;

    private void Start()
    {
        //StartActiveTab();
    }

    //public void StartActiveTab()
    //{
    //    if (activeTab != null)
    //    {
    //        SetActive(activeTab);
    //    }
    //}

    public void Subscribe(TabButtonUI button)
    {
        if (tabButtonUIs == null)
        {
            tabButtonUIs = new List<TabButtonUI>();
        }

        tabButtonUIs.Add(button);
    }

    public void OnTabEntered(TabButtonUI button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.backgroundImage.sprite = tabHover;
        }
        
    }

    public void OnTabExited(TabButtonUI button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButtonUI button)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }
        selectedTab = button;
        selectedTab.Select();

        ResetTabs();
        button.backgroundImage.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i=0; i<objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);

            }
        }

        //activeTab = tab;
        //activeTab.Activate();
        //if (onTabSelectedCallback != null)
        //{
        //    onTabSelectedCallback();
        //}

        //if (tabSwapsActiveGameObject)
        //{
        //    SwapGameObject();
        //}

        //if (panelGroup != null)
        //{
        //    panelGroup.SetPageIndex(tab.transform.GetSiblingIndex());
        //}
    }

    //public void SetActive(int siblingIndex)
    //{
    //    foreach (Tab t in _tabs)
    //    {
    //        if (t.transform.GetSiblingIndex() == siblingIndex)
    //        {
    //            SetActive(t);
    //            return;
    //        }
    //    }
    //}

    public void ResetTabs()
    {
        foreach(TabButtonUI button in tabButtonUIs)
        {
            if (selectedTab != null && button == selectedTab)
            {
                continue;
            }
            button.backgroundImage.sprite = tabIdle;
        }
    }
      


}
