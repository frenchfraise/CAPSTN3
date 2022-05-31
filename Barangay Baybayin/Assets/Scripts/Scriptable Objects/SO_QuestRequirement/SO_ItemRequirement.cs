using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item Quest Requirement Scriptable Object", menuName = "Scriptable Objects/Quest Requirement/Item")]

public class SO_ItemRequirement : SO_QuestRequirement
{
    public SO_Item so_Item;
    public int requiredAmount;
}
