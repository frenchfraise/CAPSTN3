using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSpokenToEvent : UnityEvent<string, SO_Dialogues> { }
public class Character : InteractibleObject
{
    private CharacterDialogueUI characterDialogueUI;
    //[SerializeField] private SO_StoryLine so_StoryLine;
    //[SerializeField]
    //private int storylineIndex;
    [SerializeField]
    private string id;
    [SerializeField] private bool isFirstTime = true;
    
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
        bool isQuestCompleted = CheckIfQuestComplete();
   

        StorylineData storylineData = StorylineManager.GetStorylineDataFromID(id);
        SO_StoryLine so_StoryLine = storylineData.so_StoryLine;

        if (storylineData.currentQuestChainIndex < so_StoryLine.questLines.Count)
        {
      
            SO_Questline so_Questline = so_StoryLine.questLines[storylineData.currentQuestChainIndex];
            Debug.Log("PHASE " + storylineData.currentQuestLineIndex + " - " + so_Questline.questlineData.Count);
            if (storylineData.currentQuestLineIndex < so_Questline.questlineData.Count)
            {
                Debug.Log("PHASE 1");
                QuestlineData questlineData = so_Questline.questlineData[storylineData.currentQuestLineIndex];
               
                if (isFirstTime)
                {
                    isFirstTime = false;
                    onCharacterSpokenToEvent.Invoke(id,questlineData.initialSO_Dialogues);

                }
                else
                {
                    if (isQuestCompleted)
                    {
                        Debug.Log("PHASE 2");
                        onCharacterSpokenToEvent.Invoke(id, questlineData.questCompleteSO_Dialogues);
                        QuestCompleted();
                        isFirstTime = true;
                    }
                    else
                    {
                        onCharacterSpokenToEvent.Invoke(id, questlineData.questInProgressSO_Dialogues);
                    }
                        
                }
                
            }
        }

                
        

    }

    public bool CheckIfQuestComplete()
    {

        StorylineData storylineData = StorylineManager.GetStorylineDataFromID(id);
        SO_StoryLine so_StoryLine = storylineData.so_StoryLine;
        SO_Questline so_Questline = so_StoryLine.questLines[storylineData.currentQuestChainIndex];
          
        QuestlineData questlineData = so_Questline.questlineData[storylineData.currentQuestLineIndex];
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

        bool isQuestCompleted = false;
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
                                isQuestCompleted = true;
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
                            isQuestCompleted = true;

                        }
                        else
                        {

                        }
                    }
                }
                           
            }

        }

        return isQuestCompleted;
                
            
        
        
        
    }

    public void QuestCompleted()
    {
        StorylineData storylineData = StorylineManager.GetStorylineDataFromID(id);
        SO_StoryLine so_StoryLine = storylineData.so_StoryLine;
        SO_Questline so_Questline = so_StoryLine.questLines[storylineData.currentQuestChainIndex];
        QuestlineData questlineData = so_Questline.questlineData[storylineData.currentQuestLineIndex];

        //give reward
        for (int i = 0; i < questlineData.quest.rewards.Count; i++)
        {
            InventoryManager.AddItem(questlineData.quest.rewards[i].so_Item, 
                questlineData.quest.rewards[i].amount);
        }

        if (storylineData.currentQuestLineIndex < so_Questline.questlineData.Count-1)
        {

            storylineData.currentQuestLineIndex++;

        }
        else if (storylineData.currentQuestLineIndex >= so_Questline.questlineData.Count-1) // No More Questline, move to questchain
        {

            storylineData.currentQuestLineIndex = 0;
            if (storylineData.currentQuestChainIndex < so_StoryLine.questLines.Count-1)
            {
                storylineData.currentQuestChainIndex++;
            }
            else
            {
                Debug.Log("STORYLINE FINISHED");
            }
        }

    }
       


}
