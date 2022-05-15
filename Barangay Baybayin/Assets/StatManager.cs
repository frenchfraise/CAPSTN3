using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XPIncrease : UnityEvent<StatManager> { }
public class StatManager : MonoBehaviour
{
    public static StatManager instance;

    public XPIncrease OnXPIncrease = new XPIncrease();

    [Header("Level Values")]    
    public int currentLevel;

    [Header("XP Values")]
    public float currentXP;
    public float targetXP;

    private void Awake()
    {
        instance = this;
    }

    public void AddXP(float xp)
    {
        if (currentXP >= targetXP)
        {
            LevelUp();
        }
        currentXP += xp;
    }

    public void LevelUp()
    {
        // reset XP
        currentXP = currentXP - targetXP;
        Debug.Log("[Reset XP] " + currentXP);
        // targetXP multiplier
        targetXP = targetXP * currentLevel; 
        // level up
        currentLevel++;
        Debug.Log("Player is now Level " + currentLevel + "!");
        OnXPIncrease.Invoke(this);
    }
}
