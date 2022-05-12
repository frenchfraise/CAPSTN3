using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Items/ItemBase")]
public class ItemSO : ScriptableObject
{
    public new string name;
    public string description;
    public ItemType type;
    public Sprite uiSprite;
    public AnimatorOverrideController anim;

}
