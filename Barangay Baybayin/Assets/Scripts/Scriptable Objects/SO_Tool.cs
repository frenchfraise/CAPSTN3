using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tool Scriptable Object", menuName = "Scriptable Objects/Tool")]

public class SO_Tool : ScriptableObject
{
    public int upgradeLevel;
    public SO_ResourceNode useForResourceNode;
    public float useRate;

    public float staminaCost;

    public float maxSpecialPoints;

    public float xpUseReward;

}
