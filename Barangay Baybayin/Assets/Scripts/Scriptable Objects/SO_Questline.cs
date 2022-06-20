using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestlineData
{
    public SO_Dialogues initialSO_Dialogues; //first time talk
    public SO_Dialogues questInProgressSO_Dialogues; //first time talk//add variable for lines if quest isnt compelted yet
    public SO_Dialogues questCompleteSO_Dialogues; //first time talk
    public SO_Quest quest;
}

[CreateAssetMenu(fileName = "New Questline Scriptable Object", menuName = "Scriptable Objects/Questline")]
public class SO_Questline : ScriptableObject
{
    [NonReorderable] public List<QuestlineData> questlineData = new List<QuestlineData>();
}
