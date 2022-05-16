using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;
    public int requirementAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requirementAmount);
    }

    public void EnemyKilled()
    {
        if (goalType == GoalType.Kill)
        {
            currentAmount++;
        }
 
    }

    public void ItemCollected()
    {
        if (goalType == GoalType.Gathering)
        {
            currentAmount++;
        }

    }
}

public enum GoalType
{ 
    Kill, 
    Gathering 
}

