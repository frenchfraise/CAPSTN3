using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Resource Node Scriptable Object", menuName = "Scriptable Objects/Resource Node")]
public class SO_ResourceNode : ScriptableObject
{
    public string nodeName;
    public string toolRequired; // change this class to tool when Emman made it already
    public int levelRequirement;

    public float maxHealth;
    public float regenerationTime;


}
