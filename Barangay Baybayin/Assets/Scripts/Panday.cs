using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PandaySpokenToEvent : UnityEvent { }
public class Panday : InteractibleObject
{
    [SerializeField] private string id;
    public static PandaySpokenToEvent onPandaySpokenToEvent = new PandaySpokenToEvent();
    private bool isTalking;
    private int chosenCurrentPandaySODialoguesIndex;




    [SerializeField]
    private string questId;
    [SerializeField] private bool isFirstTime = true;
    public bool isQuestMode;

    protected override void OnEnable()
    {
        base.OnEnable();
    

        StorylineManager.onWorldEventEndedEvent.AddListener(WorldEvent);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
   
    }
    protected override void OnInteract()
    {
        //onPandaySpokenToEvent.Invoke();
        if (!isQuestMode)
        {
            int chosenInitialPandayDialogueIndex = Random.Range(0, StorylineManager.instance.initialPandayDialogue.Count);
            SO_Dialogues chosenInitialPandayDialogue = StorylineManager.instance.initialPandayDialogue[chosenInitialPandayDialogueIndex];
            CharacterDialogueUI.onSetEndTransitionEnabledEvent.Invoke(false);
            CharacterDialogueUI.onSetIsCloseOnEndEvent.Invoke(false);
            CharacterDialogueUI.onSetChoicesEvent.Invoke(true);
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, chosenInitialPandayDialogue);
            CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(false);
        }
        else
        {
            Debug.Log("wee");


            StorylineData storylineData = StorylineManager.GetStorylineDataFromID(questId);
            SO_StoryLine so_StoryLine = storylineData.so_StoryLine;

            if (storylineData.currentQuestChainIndex < so_StoryLine.questLines.Count)
            {
                Debug.Log("wee");
                SO_Questline so_Questline = so_StoryLine.questLines[storylineData.currentQuestChainIndex];
                Debug.Log("PHASE " + storylineData.currentQuestLineIndex + " - " + so_Questline.questlineData.Count);
                if (storylineData.currentQuestLineIndex < so_Questline.questlineData.Count)
                {
                    Debug.Log("PHASE 1");
                    QuestlineData questlineData = so_Questline.questlineData[storylineData.currentQuestLineIndex];

                    if (isFirstTime)
                    {
                        isFirstTime = false;
                        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(questId, questlineData.initialSO_Dialogues);

                    }
                    else
                    {
                        bool isQuestCompleted = StorylineManager.instance.CheckIfQuestComplete(questId);

                        if (isQuestCompleted)
                        {
                            Debug.Log("PHASE 2");

                            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(questId, questlineData.questCompleteSO_Dialogues);

                            StorylineManager.instance.QuestCompleted(storylineData);
                            isFirstTime = true;
                        }
                        else
                        {
                            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(questId, questlineData.questInProgressSO_Dialogues);
                        }

                    }

                }
            }
        }
       
    }
    public void WorldEvent(string p_id, int p_event, int p_eventTwo)
    {

        if (p_id == id)
        {
            if (isTalking)
            {
                if (StorylineManager.instance.currentPandaySODialogues.Count > 0)
                {
                    StorylineManager.instance.currentPandaySODialogues.RemoveAt(chosenCurrentPandaySODialoguesIndex);
                }
                isTalking = false;
            }
        }
      

    }
    public void Talk()
    {

        isTalking = true;

        if (StorylineManager.instance.currentPandaySODialogues.Count > 0)
        {
            chosenCurrentPandaySODialoguesIndex = Random.Range(0, StorylineManager.instance.currentPandaySODialogues.Count);
            SO_Dialogues chosenCurrentPandaySODialogues = StorylineManager.instance.currentPandaySODialogues[chosenCurrentPandaySODialoguesIndex];
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, chosenCurrentPandaySODialogues);
      

        }
        else
        {
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, StorylineManager.instance.finishedPandayDialogue);
        }
        CharacterDialogueUI.onSetEndTransitionEnabledEvent.Invoke(true);
        CharacterDialogueUI.onSetIsCloseOnEndEvent.Invoke(true);
        CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(true);

    }

    public void OpenUI()
    {
        CharacterDialogueUI.onSetEndTransitionEnabledEvent.Invoke(true);
        CharacterDialogueUI.onSetIsCloseOnEndEvent.Invoke(true);
        CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(true);
        onPandaySpokenToEvent.Invoke();


    }
}
