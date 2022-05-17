using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tool
{
    public SO_Tool so_Tool;
    public int craftLevel = 1;

    public int expLevel = 1;
    public float expAmount = 0;
    public float specialPoints = 0;

    public void AddXP(float p_exp)
    {
        
        if (expLevel < so_Tool.maxExpAmount.Count)
        {
            if (expAmount < so_Tool.maxExpAmount[expLevel - 1])
            {
                expAmount += p_exp;
                
            }
            if (expAmount >= so_Tool.maxExpAmount[expLevel - 1])
            {
                LevelUp();
            }
            else
            {
                ToolManager.OnExpIncrease.Invoke(expAmount, so_Tool.maxExpAmount[expLevel - 1]);
            }
            
            
        }
      
        
    }

    public void LevelUp()
    {
        // reset XP
        expAmount = expAmount - so_Tool.maxExpAmount[expLevel-1];
       
        Debug.Log("[Reset XP] " + expAmount);
       

        //If max level, dont level up
        if (expLevel >=  so_Tool.maxExpAmount.Count)
        {
            Debug.Log("HIT MAX LEVEL");
        }
        else  //Else level up
        {
            expLevel++;
            ToolManager.OnExpLevelIncrease.Invoke(expLevel);
        }
        ToolManager.OnExpLevelExpIncrease.Invoke(expAmount, so_Tool.maxExpAmount[expLevel - 1]);
        Debug.Log("Player is now Level " + expLevel + "!");




    }
}
