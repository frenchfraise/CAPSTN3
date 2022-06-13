using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CraftUpgradeItemRequirementsData
{
    public List<ItemUpgradeRequirement> itemRequirements;
    public int requiredProficiencyLevel;
}
[System.Serializable]
public class ItemUpgradeRequirement
{
    public SO_Item so_Item;
    public int requiredAmount;

}



[CreateAssetMenu(fileName = "New Tool Scriptable Object", menuName = "Scriptable Objects/Tool")]

public class SO_Tool : ScriptableObject
{
    public SO_ResourceNode useForResourceNode;

    public Sprite lockedFrame;
    public Sprite lockedIcon;
    public Sprite unlockedFrame;
    public Sprite unlockedIcon;
    public Sprite equippedFrame;
    public Sprite equippedIcon;

    public List<float> useRate;

    public List<int> damage;

    public List<float> staminaCost;

    public float chargeSpeedRate;
    public float maxToolCharge;
    public int maxCraftLevel;

    public List<CraftUpgradeItemRequirementsData> craftUpgradeItemRequirementsDatas;//= new List<RequiredItem>();

    public List<float> maxSpecialPoints;
    public List<float> specialPointReward;
    public List<float> specialPointUse;

    public List<float> maxProficiencyAmount;
    public List<float> proficiencyAmountReward;



}
