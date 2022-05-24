using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class CharacterData
{
    public SO_Dialogues initialSO_Dialogues; //first time talk
    public SO_Dialogues questInProgressSO_Dialogues; //first time talk//add variable for lines if quest isnt compelted yet
    public SO_Dialogues questCompleteSO_Dialogues; //first time talk
    public SO_Quest quest;
    
}
public class OnBedInteracted : UnityEvent<SO_Dialogues> { }
public class Character : InteractibleObject
{
    public List<CharacterData> characterDatas = new List<CharacterData>();
    private bool isFirstTime = true;
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
        bool isQuestCompleted = false;
        isQuestCompleted = CheckIfQuestComplete();
        if (isQuestCompleted)
        {
            currentCharacterDataIndex++;
            onCharacterSpokenTo.Invoke(characterDatas[currentCharacterDataIndex].questCompleteSO_Dialogues);
            isFirstTime = true;
        }
        else
        {
            if (isFirstTime)
            {
                isFirstTime = false;
                onCharacterSpokenTo.Invoke(characterDatas[currentCharacterDataIndex].initialSO_Dialogues);

            }
            else
            {
                onCharacterSpokenTo.Invoke(characterDatas[currentCharacterDataIndex].questInProgressSO_Dialogues);
            }
        }
        

    }

    public bool CheckIfQuestComplete()
    {
        SO_Quest currentQuest = characterDatas[currentCharacterDataIndex].quest;
        int questResourcesFound = 0;
        foreach (QuestRequirement currentQuestRequirement in currentQuest.requirements)
        {
            SO_Resource currentQuestRequirementSOResource = currentQuestRequirement.so_resource;
            //look for resouce in inventory
            Resource resource = InventoryManager.GetResource(currentQuestRequirementSOResource);
            if (resource != null)
            {
                if (resource.amount >= currentQuestRequirement.amount)
                {
                    questResourcesFound++;
    
                }
                else
                {

                }
            }
        }
        if (questResourcesFound == currentQuest.requirements.Count) //this means it has everything
        {
            
            foreach (QuestRequirement currentQuestRequirement in currentQuest.requirements)
            {
                SO_Resource currentQuestRequirementSOResource = currentQuestRequirement.so_resource;
                //look for resouce in inventory
                Resource resource = InventoryManager.GetResource(currentQuestRequirementSOResource);
                if (resource != null)
                {
                    if (resource.amount >= currentQuestRequirement.amount)
                    {
                        resource.amount -= currentQuestRequirement.amount;

                    }
                    else
                    {

                    }
                }
            }
    
            return true;
        }
        
        return false;
    }

    public void QuestCompleted()
    {
        currentCharacterDataIndex++;
    }


}
