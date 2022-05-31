using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Quest Scriptable Object", menuName = "Scriptable Objects/Quest")]
public class SO_Quest : ScriptableObject
{

    public Sprite questImage;
    public string title;
    public string description;
    public SO_Item reward; // turn this to item
    public List<QuestRequirement> requirements = new List<QuestRequirement>();
}


[System.Serializable]
public class QuestRequirement
{
    public SO_QuestRequirement so_requirement;
    public bool isCompleted;
}