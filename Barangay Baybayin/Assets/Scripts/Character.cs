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
                    QuestCompleted();
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
                    Debug.Log("PH: 1");
                    if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_ItemRequirement))
                    {
                        Debug.Log("PH: 2");
                        SO_ItemRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_ItemRequirement;
                        for (int i = 0; i < specificQuestRequirement.so_Item.Count; i++)
                        {
                            Debug.Log("PH: 3");
                            SO_Item currentSOItemQuestRequirement = specificQuestRequirement.so_Item[i];
                            //look for item in inventory
                            ItemData itemData = InventoryManager.GetItem(currentSOItemQuestRequirement);
                            if (itemData != null)
                            {
                                Debug.Log("PH: 4");
                                if (itemData.amount >= specificQuestRequirement.requiredAmount[i])
                                {
                                    Debug.Log("PH: 5");
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
                Debug.Log(questResourcesFound + " " + currentQuest.requirements.Count);

                bool compel = false;
                Debug.Log("PH: 8");
                foreach (QuestRequirement currentQuestRequirement in currentQuest.requirements)
                {

                    //Check quest requirement type
                    Debug.Log("PH: 9");
                    if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_ItemRequirement))
                    {
                        Debug.Log("PH: 10");
                        SO_ItemRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_ItemRequirement;
                        if (questResourcesFound == specificQuestRequirement.so_Item.Count)
                        {
                            for (int i = 0; i < specificQuestRequirement.so_Item.Count; i++)
                            {
                                Debug.Log("PH: 11");
                                SO_Item currentSOItemQuestRequirement = specificQuestRequirement.so_Item[i];
                                //look for item in inventory
                                ItemData itemData = InventoryManager.GetItem(currentSOItemQuestRequirement);
                                if (itemData != null)
                                {
                                    Debug.Log("PH: 12");
                                    if (itemData.amount >= specificQuestRequirement.requiredAmount[i])
                                    {
                                        Debug.Log("PH: 13");
                                        itemData.amount -= specificQuestRequirement.requiredAmount[i];
                                        compel = true;
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                        }
                       
                    }
                    else if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_InfrastructureRequirement))
                    {
                        SO_InfrastructureRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_InfrastructureRequirement;
                        if (questResourcesFound == 1)
                        {
                            
                            SO_Infrastructure currentSOInrastructureQuestRequirement = specificQuestRequirement.so_infrastructure;
                            //look for item in inventory
                            Infrastructure itemData = InfrastructureManager.GetInfrastructure(currentSOInrastructureQuestRequirement);
                            if (itemData != null)
                            {
                                if (itemData.currentLevel >= specificQuestRequirement.requiredLevel)
                                {
                                    compel = true;

                                }
                                else
                                {

                                }
                            }
                        }
                           
                    }

                }

                return compel;
                
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
                for (int i =0; i < so_Questline.questlineData[storylineData.currentQuestlinePartIndex].quest.rewards.Count; i++)
                {
                    InventoryManager.AddItem(so_Questline.questlineData[storylineData.currentQuestlinePartIndex].quest.rewards[i].so_Item, so_Questline.questlineData[storylineData.currentQuestlinePartIndex].quest.rewards[i].amount);
                }
                storylineData.currentQuestlinePartIndex++;
                //give reward
        

            }
            else
            {
               
                currentQuestlinePartIndex = 0;
                if (currentStorylineIndex < so_StoryLine.questLines.Count - 1)
                {
                    storylineData.currentStorylineIndex++;
                }
                else
                {
                    Debug.Log("STORYLINE FINISHED");
                }
            }
            
        }
    }
       


}
