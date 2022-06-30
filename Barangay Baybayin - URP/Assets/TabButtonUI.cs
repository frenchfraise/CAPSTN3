using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class TabUISelectedEvent : UnityEvent { }
public class TabUIDeselectedEvent : UnityEvent { }

[RequireComponent(typeof(Image))]
public class TabButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{

    public TabGroupUI tabGroupUI;

    public Image backgroundImage;

    public TabUISelectedEvent onTabUISelectedEvent = new TabUISelectedEvent();
    public TabUIDeselectedEvent onTabUIDeselectedEvent = new TabUIDeselectedEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroupUI.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroupUI.OnTabEntered(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroupUI.OnTabExited(this);
    }

    public void Select()
    {
        if (onTabUISelectedEvent != null)
        {
            onTabUISelectedEvent.Invoke();
        }
    }

    public void Deselect()
    {
        if (onTabUIDeselectedEvent != null)
        {
            onTabUIDeselectedEvent.Invoke();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        backgroundImage = GetComponent<Image>();
        tabGroupUI.Subscribe(this);
    }

   
}
