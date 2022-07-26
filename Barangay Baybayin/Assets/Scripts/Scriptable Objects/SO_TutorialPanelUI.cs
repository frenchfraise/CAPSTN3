using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TPUI
{
    public string tutorialTitle;
    public Sprite image;
    [TextArea]
    public string words;
}
[CreateAssetMenu(fileName = "New Tutorial Panels Scriptable Object", menuName = "Scriptable Objects/Tutorial Panel")]
public class SO_TutorialPanelUI : ScriptableObject
{
    [NonReorderable] public List<TPUI> panels = new List<TPUI>();
  
}

[System.Serializable]
public class TutorialPanelUIData
{
    [NonReorderable] public List<TPUI> panels = new List<TPUI>();
  
}


