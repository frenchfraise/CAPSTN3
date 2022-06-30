using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Infrastructure Quest Requirement Scriptable Object", menuName = "Scriptable Objects/Quest Requirement/Infrastructure")]
public class SO_InfrastructureRequirement : SO_QuestRequirement
{
    public SO_Infrastructure so_infrastructure;
    public int requiredLevel;
}
