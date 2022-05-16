using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{

    public bool isActive;

    public string title;
    public string description;
    public int reward;
    public int requirement; 

    public QuestGoal questGoal;
}
