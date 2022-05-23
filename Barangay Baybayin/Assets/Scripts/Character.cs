using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class CharacterData
{
    public SO_Dialogues initialSO_Dialogues; //first time talk
    public SO_Dialogues questInProgressSO_Dialogues; //first time talk//add variable for lines if quest isnt compelted yet
    public Quest quest;
    
}
public class OnBedInteracted : UnityEvent<SO_Dialogues> { }
public class Character : InteractibleObject
{
    public List<CharacterData> characterDatas = new List<CharacterData>();

    public int currentCharacterDataIndex;
    public OnBedInteracted onCharacterSpokenTo = new OnBedInteracted();

    protected override void OnEnable()
    {
        base.OnEnable();
        onCharacterSpokenTo.AddListener(UIManager.instance.characterDialogueUI.OnCharacterSpokenTo);

    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if (UIManager.instance)
        {
            onCharacterSpokenTo.RemoveListener(UIManager.instance.characterDialogueUI.OnCharacterSpokenTo);
        }
        

    }
    protected override void OnInteract()
    {
        onCharacterSpokenTo.Invoke(characterDatas[currentCharacterDataIndex].initialSO_Dialogues);
        

    }

    public void QuestCompleted()
    {
        currentCharacterDataIndex++;
    }


}
