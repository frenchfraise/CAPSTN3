using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CraftUpgradeItemRequirementsData
{
    [NonReorderable] public List<ItemUpgradeRequirement> itemRequirements;
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
    [NonReorderable] public List<SO_ResourceNode> useForResourceNode;

    public Sprite lockedFrame;
    public Sprite lockedIcon;
    public Sprite unlockedFrame;
    public Sprite unlockedIcon;
    public Sprite equippedFrame;
    public List<Sprite> equippedIcon;

    [NonReorderable] public List<float> useRate;

    [NonReorderable] public List<int> damage;

    [NonReorderable] public List<float> staminaCost;

    public float chargeSpeedRate;
    public float maxToolCharge;
    public int maxCraftLevel;

    [NonReorderable] public List<CraftUpgradeItemRequirementsData> craftUpgradeItemRequirementsDatas;//= new List<RequiredItem>();

    [NonReorderable] public List<float> maxSpecialPoints;
    [NonReorderable] public List<int> maxSpecialCharges;
    [NonReorderable] public List<float> specialPointReward;
    [NonReorderable] public List<float> specialPointUse;

    [NonReorderable] public List<float> maxProficiencyAmount;
    [NonReorderable] public List<float> proficiencyAmountReward;



}
