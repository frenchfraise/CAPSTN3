using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/ElementalType")]
public class ElementalTypeSO : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public List<ElementalTypeSO> weakAgainst; // need better name
}
