using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSpokenToEvent : UnityEvent<SO_Dialogues> { }
public class Character : InteractibleObject
{
    private CharacterDialogueUI characterDialogueUI;
    //[SerializeField] private SO_StoryLine so_StoryLine;
    [SerializeField]
    private int index;
    private bool isFirstTime = true;
    
    public CharacterSpokenToEvent onCharacterSpokenToEvent = new CharacterSpokenToEvent();
    
    protected override void OnEnable()
    {
        base.OnEnable();
        characterDialogueUI = UIManager.instance.characterDialogueUI ? UIManager.instance.characterDialogueUI
          : FindObjectOfType<CharacterDialogueUI>();
        onCharacterSpokenToEvent.AddListener(characterDialogueUI.OnCharacterSpokenTo); 
        
        

    }
    protected override void OnDisable()
    {
        base.OnDisable();
        
        onCharacterSpokenToEvent.RemoveListener(characterDialogueUI.OnCharacterSpokenTo);
        
        

    }
    protected override void OnInteract()
    {
        // Debug.Log("wee");
        bool isQuestCompleted = false;
        Debug.Log("TEST");
        isQuestCompleted = CheckIfQuestComplete();
        StorylineData storylineData = StorylineManager.instance.storyLines[index];
        SO_StoryLine so_StoryLine = storylineData.so_StoryLine;
        int currentStorylineIndex = storylineData.currentStorylineIndex;
        int currentQuestlinePartIndex = storylineData.currentQuestlinePartIndex;
        if (currentStorylineIndex < so_StoryLine.questLines.Count)
        {
      
            SO_Questline so_Questline = so_StoryLine.questLines[currentStorylineIndex];
            Debug.Log("PHASE " + currentQuestlinePartIndex + " - " + so_Questline.questlineData.Count);
            if (currentQuestlinePartIndex < so_Questline.questlineData.Count)
            {
                Debug.Log("PHASE 1");
                QuestlineData questlineData = so_Questline.questlineData[currentQuestlinePartIndex];
                if (isQuestCompleted)
                {
                    Debug.Log("PHASE 2");
                    onCharacterSpokenToEvent.Invoke(questlineData.questCompleteSO_Dialogues);
                    currentStorylineIndex++;
                    isFirstTime = true;
                }
                else
                {
                    if (isFirstTime)
                    {
                        isFirstTime = false;
                        onCharacterSpokenToEvent.Invoke(questlineData.initialSO_Dialogues);

                    }
                    else
                    {
                        onCharacterSpokenToEvent.Invoke(questlineData.questInProgressSO_Dialogues);
                    }
                }
            }
        }

                
        

    }

    public bool CheckIfQuestComplete()
    {

        StorylineData storylineData = StorylineManager.instance.storyLines[index];
        SO_StoryLine so_StoryLine = storylineData.so_StoryLine;
        int currentStorylineIndex = storylineData.currentStorylineIndex;
        int currentQuestlinePartIndex = storylineData.currentQuestlinePartIndex;
        if (currentStorylineIndex < so_StoryLine.questLines.Count)
        {
            SO_Questline so_Questline = so_StoryLine.questLines[currentStorylineIndex];
            if (currentQuestlinePartIndex < so_Questline.questlineData.Count)
            {
                QuestlineData questlineData = so_Questline.questlineData[currentQuestlinePartIndex];
                SO_Quest currentQuest = questlineData.quest;
                int questResourcesFound = 0;

                foreach (QuestRequirement currentQuestRequirement in currentQuest.requirements)
                {

                    //Check quest requirement type

                    if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_ItemRequirement))
                    {
                        SO_ItemRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_ItemRequirement;
                        for (int i = 0; i < specificQuestRequirement.so_Item.Count; i++)
                        {
                            SO_Item currentSOItemQuestRequirement = specificQuestRequirement.so_Item[i];
                            //look for item in inventory
                            ItemData itemData = InventoryManager.GetItem(currentSOItemQuestRequirement);
                            if (itemData != null)
                            {
                                if (itemData.amount >= specificQuestRequirement.requiredAmount[i])
                                {

                                    questResourcesFound++;

                                }
                                else
                                {

                                }
                            }
                        }
                     
                       
                    }
                    else if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_InfrastructureRequirement))
                    {
                        SO_InfrastructureRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_InfrastructureRequirement;
                        SO_Infrastructure currentSOInrastructureQuestRequirement = specificQuestRequirement.so_infrastructure;
                        //look for item in inventory
                        Infrastructure itemData = InfrastructureManager.GetInfrastructure(currentSOInrastructureQuestRequirement);
                        if (itemData != null)
                        {
                            if (itemData.currentLevel >= specificQuestRequirement.requiredLevel)
                            {
                                questResourcesFound++;

                            }
                            else
                            {

                            }
                        }
                    }

                }
                if (questResourcesFound == currentQuest.requirements.Count) //this means it has everything
                {

                    foreach (QuestRequirement currentQuestRequirement in currentQuest.requirements)
                    {

                        //Check quest requirement type

                        if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_ItemRequirement))
                        {
                            SO_ItemRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_ItemRequirement;
                            for (int i = 0; i < specificQuestRequirement.so_Item.Count; i++)
                            {
                                SO_Item currentSOItemQuestRequirement = specificQuestRequirement.so_Item[i];
                                //look for item in inventory
                                ItemData itemData = InventoryManager.GetItem(currentSOItemQuestRequirement);
                                if (itemData != null)
                                {
                                    if (itemData.amount >= specificQuestRequirement.requiredAmount[i])
                                    {
                                        itemData.amount -= specificQuestRequirement.requiredAmount[i];

                                    }
                                    else
                                    {

                                    }
                                }
                            }
                        }
                        else if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_InfrastructureRequirement))
                        {
                            SO_InfrastructureRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_InfrastructureRequirement;
                            SO_Infrastructure currentSOInrastructureQuestRequirement = specificQuestRequirement.so_infrastructure;
                            //look for item in inventory
                            Infrastructure itemData = InfrastructureManager.GetInfrastructure(currentSOInrastructureQuestRequirement);
                            if (itemData != null)
                            {
                                if (itemData.currentLevel >= specificQuestRequirement.requiredLevel)
                                {


                                }
                                else
                                {

                                }
                            }
                        }

                    }

                    return true;
                }
            }
        }
        
        
        return false;
    }

    public void QuestCompleted()
    {
        StorylineData storylineData = StorylineManager.instance.storyLines[index];
        SO_StoryLine so_StoryLine = storylineData.so_StoryLine;
        int currentStorylineIndex = storylineData.currentStorylineIndex;
        int currentQuestlinePartIndex = storylineData.currentQuestlinePartIndex;
      
        if (currentStorylineIndex < so_StoryLine.questLines.Count - 1)
        {
            SO_Questline so_Questline = so_StoryLine.questLines[currentStorylineIndex];
            if (currentQuestlinePartIndex < so_Questline.questlineData.Count - 1)
            {
                currentQuestlinePartIndex++;

            }
            else
            {
               
                currentQuestlinePartIndex = 0;
                if (currentStorylineIndex < so_StoryLine.questLines.Count - 1)
                {
                    currentStorylineIndex++;
                }
                else
                {
                    Debug.Log("STORYLINE FINISHED");
                }
            }
            
        }
    }
       


}
