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
    [SerializeField]
    private bool dontUseStoryline = false;

    [SerializeField]
    List<SO_Dialogues> currenDialogueOptions;

    public GameObject questHint;
    [SerializeField]
    private List<SO_Dialogues> dialogueOptions;
    private void Awake()
    {
        ResetDialogueOptions();
        StorylineManager.onLastTimeStoryline.AddListener(LastTimeStorylineEvent);        
    }
    private void OnDestroy()
    {
        StorylineManager.onLastTimeStoryline.RemoveListener(LastTimeStorylineEvent);
    }
    protected override void OnEnable()
    {
        base.OnEnable();

    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }

    void PlaySound()
      
    {
        if (!TimeManager.instance.tutorialOn) 
        {
    
            AudioManager.instance.PlayOnRoomEnterString("Quest Get");
        }
    }

    void ResetDialogueOptions()
    {
        currenDialogueOptions = new List<SO_Dialogues>(dialogueOptions);
    }

    protected override void OnInteract()
    {
        if (!dontUseStoryline)
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
  
                if (storylineData.currentQuestLineIndex < so_Questline.questlineData.Count)
                {
                   
                    QuestlineData questlineData = so_Questline.questlineData[storylineData.currentQuestLineIndex];
                    if (storylineData.completed)
                    {
                        PlaySound();
                        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, so_StoryLine.finishedDialogue);

                    }
                    else
                    {
                        if (isFirstTime)
                        {
                            isFirstTime = false;
                            PlaySound();
                            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, questlineData.initialSO_Dialogues);

                        }
                        else
                        {
                            bool isQuestCompleted = StorylineManager.instance.CheckIfQuestComplete(id);
                         
                            if (isQuestCompleted)
                            {
                              
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
                                    AudioManager.instance.PlayOnRoomEnterString("QuestComplete");
                                    StorylineManager.instance.amountQuestComplete++;
                                    isFirstTime = true;
                                    if (TutorialManager.instance.isFirstTimeFood)
                                    {
                                        TutorialManager.instance.isFirstTimeFood = false;
                                        CharacterDialogueUI.onFirstTimeFoodOnEndEvent.Invoke();
                                    }
                                }
                               
                                StorylineManager.instance.QuestCompleted(storylineData);

                            }
                            else
                            {
                                PlaySound();
                                CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, questlineData.questInProgressSO_Dialogues);
                            }

                        }
                    }


                }
            }
        }
        else
        {
            int chosenIndex = 0;
            if (currenDialogueOptions.Count > 1)
            {
                chosenIndex = Random.Range(0, currenDialogueOptions.Count);
            }
            PlaySound();
            SO_Dialogues chosenDialogue = currenDialogueOptions[chosenIndex];
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("FAKEDIALOGUES", chosenDialogue);
            currenDialogueOptions.Remove(chosenDialogue);
            if (currenDialogueOptions.Count <= 0)
            {
                ResetDialogueOptions();
            }
        }
    }

    public void LastTimeStorylineEvent(int p_int)
    {
        int storylineDataIndex = StorylineManager.GetIndexFromID(id);
        StorylineData storylineData = StorylineManager.GetStorylineDataFromID(id);

        if (StorylineManager.GetIndexFromStorylineData(storylineData) != 4)
        {
            if (questHint)
            {
                questHint.SetActive(false);
            }
        }

    }    
}
