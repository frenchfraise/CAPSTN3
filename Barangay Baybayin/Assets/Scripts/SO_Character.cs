using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharacterExpression
{
    public List<Sprite> faces = new List<Sprite>();
   
}

[System.Serializable]
public class CharacterEmotion
{
   
    public List<CharacterExpression> emotion;

}

[CreateAssetMenu(fileName = "New Character Scriptable Object", menuName = "Scriptable Objects/Character")]
public class SO_Character : ScriptableObject
{
    public List<Sprite> body = new List<Sprite>();
    public CharacterEmotion expressions;
}
