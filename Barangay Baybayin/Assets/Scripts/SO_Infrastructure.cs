using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Infrastructure Scriptable Object", menuName = "Scriptable Objects/Infrastructure")]
public class SO_Infrastructure : ScriptableObject
{
    public new string name;
    public int currentLevel;
    public List<Sprite> sprites = new List<Sprite>();
}
