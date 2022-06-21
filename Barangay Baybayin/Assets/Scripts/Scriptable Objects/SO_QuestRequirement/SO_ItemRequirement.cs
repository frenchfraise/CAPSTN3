using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item Quest Requirement Scriptable Object", menuName = "Scriptable Objects/Quest Requirement/Item")]

public class SO_ItemRequirement : SO_QuestRequirement
{
    public List<SO_Item> so_Item;
    public List<int> requiredAmount;
}
