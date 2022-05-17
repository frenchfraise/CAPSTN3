using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AvatarTransitionType
{ 
    None,
}


public enum SpeechTransitionType
{
    None,
}

public enum CharacterEmotionType
{
    happy,
    thinking,
    neutral,
    sad,
    mad,
    misc,
    none,
}

[System.Serializable]
public class Speech
{
    public SO_Character character;
    public CharacterEmotionType emotion;
    public int bodyIndex;
    public int faceIndex;
    public AvatarTransitionType avatarTransitionType;

    public string speech;
    public SpeechTransitionType speechTransitionType;
}

[CreateAssetMenu(fileName = "New Dialogue Scriptable Object", menuName = "Scriptable Objects/Dialogue")]
public class SO_Dialogue : ScriptableObject
{
    public List<Speech> speeches = new List<Speech>();

}
