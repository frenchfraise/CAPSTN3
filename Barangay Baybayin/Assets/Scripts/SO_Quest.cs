using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Quest Scriptable Object", menuName = "Scriptable Objects/Quest")]
public class SO_Quest : ScriptableObject
{
    public bool isActive;

    public string title;
    public string description;
    public int reward;
    public List<QuestRequirement> requirements = new List<QuestRequirement>();

    public QuestGoal questGoal;
}


[System.Serializable]
public class QuestRequirement
{
    public SO_Resource so_resource;
    public int amount;
}