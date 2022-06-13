using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnBedInteractedEvent : UnityEvent<SO_Dialogues> { }
public class Character : InteractibleObject
{
    private CharacterDialogueUI characterDialogueUI;
    //[SerializeField] private SO_StoryLine so_StoryLine;
    [SerializeField]
    private int index;
    private bool isFirstTime = true;
    
    public OnBedInteractedEvent onCharacterSpokenToEvent = new OnBedInteractedEvent();
    
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
        int currentCharacterDataIndex = storylineData.currentCharacterDataIndex;
        if (isQuestCompleted)
        {
            
            onCharacterSpokenToEvent.Invoke(so_StoryLine.questLines[currentCharacterDataIndex].questCompleteSO_Dialogues);
            currentCharacterDataIndex++;
            isFirstTime = true;
        }
        else
        {
            if (isFirstTime)
            {
                isFirstTime = false;
                onCharacterSpokenToEvent.Invoke(so_StoryLine.questLines[currentCharacterDataIndex].initialSO_Dialogues);

            }
            else
            {
                onCharacterSpokenToEvent.Invoke(so_StoryLine.questLines[currentCharacterDataIndex].questInProgressSO_Dialogues);
            }
        }
        

    }

    public bool CheckIfQuestComplete()
    {

        StorylineData storylineData = StorylineManager.instance.storyLines[index];
        SO_StoryLine so_StoryLine = storylineData.so_StoryLine;
        int currentCharacterDataIndex = storylineData.currentCharacterDataIndex;
        SO_Quest currentQuest = so_StoryLine.questLines[currentCharacterDataIndex].quest;
        int questResourcesFound = 0;
        //if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_ItemRequirement))
        //{
        //    SO_ItemRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_ItemRequirement;
        //    SO_Item currentSOItemQuestRequirement = specificQuestRequirement.so_Item;
        //    //look for item in inventory
        //    ItemData itemData = InventoryManager.GetItem(currentSOItemQuestRequirement);


        //    itemDatas.Add(itemData);
        //    amounts.Add(specificQuestRequirement.requiredAmount);
        //    questResourcesFound++;


        //}
        foreach (QuestRequirement currentQuestRequirement in currentQuest.requirements)
        {
            
            //Check quest requirement type
            
            if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_ItemRequirement))
            {
                SO_ItemRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_ItemRequirement;
                SO_Item currentSOItemQuestRequirement = specificQuestRequirement.so_Item;
                //look for item in inventory
                ItemData itemData = InventoryManager.GetItem(currentSOItemQuestRequirement);
                if (itemData != null)
                {
                    if (itemData.amount >= specificQuestRequirement.requiredAmount)
                    {

                        questResourcesFound++;

                    }
                    else
                    {

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
                    SO_Item currentSOItemQuestRequirement = specificQuestRequirement.so_Item;
                    //look for item in inventory
                    ItemData itemData = InventoryManager.GetItem(currentSOItemQuestRequirement);
                    if (itemData != null)
                    {
                        if (itemData.amount >= specificQuestRequirement.requiredAmount)
                        {
                            itemData.amount -= specificQuestRequirement.requiredAmount;

                        }
                        else
                        {

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
        
        return false;
    }

    public void QuestCompleted()
    {
        StorylineData storylineData = StorylineManager.instance.storyLines[index];
        SO_StoryLine so_StoryLine = storylineData.so_StoryLine;
        storylineData.currentCharacterDataIndex++;
    }


}
