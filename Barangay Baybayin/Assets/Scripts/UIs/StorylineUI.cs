using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StorylineUI : MonoBehaviour
{
    public bool isSeen = false;
    public bool isFinished = false;
    public GameObject completed;
    public TMP_Text titleText;
    public TMP_Text questCountText;
    public RectTransform container;
    public RectTransform reqcontainer;
    public Image thisFrame;
    public Image questFrame;
    public Image icon;
    public List<ItemUI> itemUIs;
    public List<ItemUI> requiredItemUIs;
    public GameObject seenFrame;
    
}
