using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum SpeechTransitionType
{
    Typewriter,
    None,
}

public enum CharacterEmotionType
{
    happy,
    thinking,
    yawn,
    exclamation,
    lightbulb,
    question,
    heart,
    sad,
    mad,
    misc,
    none,
}

[System.Serializable]
public class Dialogue
{
    public SO_Character character;
    public CharacterEmotionType emotion;

    [TextArea]
    public string words;
    public SpeechTransitionType speechTransitionType;
}

[CreateAssetMenu(fileName = "New Dialogues Scriptable Object", menuName = "Scriptable Objects/Dialogues")]
public class SO_Dialogues : ScriptableObject
{
    public List<Dialogue> dialogues = new List<Dialogue>();

    
}
