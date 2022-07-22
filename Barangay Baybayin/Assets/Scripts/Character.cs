using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Character : InteractibleObject
{

    [SerializeField]
    private string id;
    [SerializeField] private bool isFirstTime = true;
    private bool isFirstTimeInEver = true;




    protected override void OnEnable()
    {
        base.OnEnable();

    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    protected override void OnInteract()
    {
        int storylineDataIndex = StorylineManager.GetIndexFromID(id);
        StorylineData storylineData = StorylineManager.GetStorylineDataFromID(id);
        SO_StoryLine so_StoryLine = storylineData.so_StoryLine;
        if (isFirstTimeInEver)
        {
            isFirstTimeInEver = false;
            StorylineManager.onFirstTimeStoryline.Invoke(storylineDataIndex);
        }
        if (storylineData.currentQuestChainIndex < so_StoryLine.questLines.Count)
        {
           
            SO_Questline so_Questline = so_StoryLine.questLines[storylineData.currentQuestChainIndex];
            Debug.Log("PHASE " + storylineData.currentQuestLineIndex + " - " + so_Questline.questlineData.Count);
            if (storylineData.currentQuestLineIndex < so_Questline.questlineData.Count)
            {
                Debug.Log("PHASE 1");
                QuestlineData questlineData = so_Questline.questlineData[storylineData.currentQuestLineIndex];
                if (storylineData.completed)
                {
                    CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, so_StoryLine.finishedDialogue);
                }
                else
                {
                    if (isFirstTime)
                    {
                        isFirstTime = false;
                        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, questlineData.initialSO_Dialogues);

                    }
                    else
                    {
                        bool isQuestCompleted = StorylineManager.instance.CheckIfQuestComplete(id);

                        if (isQuestCompleted)
                        {
                            Debug.Log("PHASE 2");
                            if (storylineData.currentQuestLineIndex == 0)
                            {

                                QuestlineData questlineDataNext = so_Questline.questlineData[storylineData.currentQuestLineIndex + 1];
                                SO_InfrastructureRequirement ir = questlineDataNext.quest.requirements[0].so_requirement as SO_InfrastructureRequirement;
                                InfrastructureManager.GetInfrastructure(ir.so_infrastructure).ForQuest();
                                CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, questlineDataNext.initialSO_Dialogues);
                            }
                            else
                            {
                                CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, questlineData.questCompleteSO_Dialogues);
                               
                                isFirstTime = true;
                                if (TutorialManager.instance.isFirstTimeFood)
                                {
                                    TutorialManager.instance.isFirstTimeFood = false;
                                    CharacterDialogueUI.onFirstTimeFoodOnEndEvent.Invoke();
                                }
                            }
                            //CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, questlineData.questCompleteSO_Dialogues);

                            StorylineManager.instance.QuestCompleted(storylineData);

                        }
                        else
                        {
                            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, questlineData.questInProgressSO_Dialogues);
                        }

                    }
                }
                
                
            }
        }

                
        

    }

    

    
       


}
