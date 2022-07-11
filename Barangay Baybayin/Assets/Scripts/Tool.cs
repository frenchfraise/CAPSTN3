using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Tool
{

    public SO_Tool so_Tool;
    public string toolName;
    public int toolNum;
    public int craftLevel = 1;

    public int proficiencyLevel = 1;
    public float proficiencyAmount = 0;
    public float specialPoints = 0;
    public int specialChargesCounter = 0;

    public void ModifySpecialAmount(float p_modifierAmount)
    {

        //Debug.Log("SPECIAL POINTS MODIFIED");
        
        specialPoints += p_modifierAmount;
       
        

        if (specialPoints >= so_Tool.maxSpecialPoints[craftLevel-1])
        {
            // specialPoints = so_Tool.maxSpecialPoints[craftLevel];
            specialPoints = 0;
            specialChargesCounter++;
            ToolManager.onSpecialPointsFilledEvent.Invoke();
        }
        else
        {
            ToolManager.onSpecialPointsModifiedEvent.Invoke(specialPoints, so_Tool.maxSpecialPoints[craftLevel-1]);
        }


        
    }

    public void ModifyProficiencyAmount(float p_modifierAmount)
    {
        //Debug.Log("PROFICIENCY MODIFIED");
        if (proficiencyLevel < so_Tool.maxProficiencyAmount.Count)
        {

            if (proficiencyAmount < so_Tool.maxProficiencyAmount[proficiencyLevel])
            {
                
                proficiencyAmount += p_modifierAmount;
                Debug.Log("EXP: " + proficiencyAmount + " - " + so_Tool.maxProficiencyAmount[proficiencyLevel] + " _ " + proficiencyLevel);
              
            }
            if (proficiencyAmount >= so_Tool.maxProficiencyAmount[proficiencyLevel])
            {
                Debug.Log("EXP: " + proficiencyAmount + " - " + so_Tool.maxProficiencyAmount[proficiencyLevel] + " _ " + proficiencyLevel);
                LevelUp();
            }
            else
            {
                Debug.Log("EXP: " + proficiencyAmount + " - " + so_Tool.maxProficiencyAmount[proficiencyLevel] + " _ " + proficiencyLevel);
                ToolManager.onProficiencyAmountModifiedEvent.Invoke(proficiencyAmount, so_Tool.maxProficiencyAmount[proficiencyLevel]);
            }
            
            
        } 
    }

    public void LevelUp()
    {
        // reset XP
        proficiencyAmount = proficiencyAmount - so_Tool.maxProficiencyAmount[proficiencyLevel];
        Debug.Log("EXP: " + proficiencyAmount + " - " + so_Tool.maxProficiencyAmount[proficiencyLevel] + " _ " + proficiencyLevel);
        //If max level, dont level up
        if (proficiencyLevel >=  so_Tool.maxProficiencyAmount.Count)
        {
            Debug.Log("HIT MAX LEVEL");
        }
        else  //Else level up
        {
            Debug.Log(proficiencyLevel);
            proficiencyLevel++;
            Debug.Log(proficiencyLevel);
            ToolManager.onProficiencyLevelModifiedEvent.Invoke(proficiencyLevel);
        }
 
    }
    
    public void CheckUpgradeCraftLevel(SO_Item p_)
    {
        if (proficiencyLevel >= craftLevel)
        {
            List<ItemData> p_itemDatas = new List<ItemData>();
            List<int> p_amount = new List<int>();
            p_itemDatas.Add(InventoryManager.GetItem(p_));
            p_amount.Add(1);
            InventoryManager.ReduceItems(p_itemDatas, p_amount,ToolManager.onToolUpgradedEvent);
            
            
        }
    }

    public void UpgradeCraftLevel()
    {
        
        craftLevel++;
        
    }
   

}
