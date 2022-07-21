using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Infrastructure Scriptable Object", menuName = "Scriptable Objects/Infrastructure")]
public class SO_Infrastructure : ScriptableObject
{
    public new string name;
    public int maxHealth;
    public List<Vector2> boxColliderSize = new List<Vector2>();
    public List<Vector2> boxColliderOffSet = new List<Vector2>();
    public List<Sprite> sprites = new List<Sprite>();
}
