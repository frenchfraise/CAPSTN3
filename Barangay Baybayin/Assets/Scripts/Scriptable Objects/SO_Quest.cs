using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemReward
{
    public SO_Item so_Item;
    public int amount;

}

[CreateAssetMenu(fileName = "New Quest Scriptable Object", menuName = "Scriptable Objects/Quest")]
public class SO_Quest : ScriptableObject
{

    public Sprite questImage;
    public string title;
    public string description;
    public List<ItemReward> rewards; // turn this to item
    public List<QuestRequirement> requirements = new List<QuestRequirement>();
}


[System.Serializable]
public class QuestRequirement
{
    public SO_QuestRequirement so_requirement;
    public bool isCompleted;
}