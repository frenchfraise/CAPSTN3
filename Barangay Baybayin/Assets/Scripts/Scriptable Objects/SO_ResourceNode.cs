using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Resource Node Scriptable Object", menuName = "Scriptable Objects/Resource Node")]
public class SO_ResourceNode : ScriptableObject
{
    public SO_Resource resource;
    public float maxHealth;
    public float regenerationTime;


}
