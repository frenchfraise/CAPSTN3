using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Tool Scriptable Object", menuName = "Scriptable Objects/Tool")]

public class SO_Tool : ScriptableObject
{
    public SO_ResourceNode useForResourceNode;
    public Sprite toolImage;
    public float useRate;

    public int damage;

    public float staminaCost;

    public int chargeSpeedRate;
    public float maxToolCharge;
    public int maxCraftLevel;

    public float maxSpecialPoints;
    public List<float> maxExpAmount;
    public float expUseReward;
}
