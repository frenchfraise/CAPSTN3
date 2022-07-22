using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StorylineUI : MonoBehaviour
{
    public bool isSeen = false;
    public TMP_Text titleText;
    public TMP_Text questCountText;
    public RectTransform container;
    public Image thisFrame;
    public Image questFrame;
    public Image icon;
    public List<ItemUI> itemUIs;
    public GameObject seenFrame;
    
}
