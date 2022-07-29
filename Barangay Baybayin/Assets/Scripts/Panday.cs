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


    public bool complete = false;

    [SerializeField]
    private string questId;
    [SerializeField] private bool isFirstTime = true;
    public bool isQuestMode;
    private void Awake()
    {
        StorylineManager.onWorldEventEndedEvent.AddListener(WorldEvent);
    }

    private void OnDestroy()
    {
        StorylineManager.onWorldEventEndedEvent.RemoveListener(WorldEvent);
    }
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
        //onPandaySpokenToEvent.Invoke();
        if (canInteract)
        {
            if (!isQuestMode)
            {
                int chosenInitialPandayDialogueIndex = Random.Range(0, StorylineManager.instance.initialPandayDialogue.Count);
                SO_Dialogues chosenInitialPandayDialogue = StorylineManager.instance.initialPandayDialogue[chosenInitialPandayDialogueIndex];
                UIManager.instance.characterDialogueUI.SetEndTransitionEnabledEvent(false);
                UIManager.instance.characterDialogueUI.SetIsCloseOnEndEvent(false);
                UIManager.instance.characterDialogueUI.SetChoicesEvent(true);
                CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, chosenInitialPandayDialogue);
                UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(false);
            }
            else
            {
                Debug.Log("wee");


                StorylineData storylineData = StorylineManager.GetStorylineDataFromID(questId);
                SO_StoryLine so_StoryLine = storylineData.so_StoryLine;

                if (storylineData.currentQuestChainIndex < so_StoryLine.questLines.Count)
                {
                    //Debug.Log("wee");
                    SO_Questline so_Questline = so_StoryLine.questLines[storylineData.currentQuestChainIndex];
                    //Debug.Log("PHASE " + storylineData.currentQuestLineIndex + " - " + so_Questline.questlineData.Count);
                    if (storylineData.currentQuestLineIndex < so_Questline.questlineData.Count)
                    {
                       // Debug.Log("PHASE 1");
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
                                complete = true;
                              //  Debug.Log("PHASE 2");

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
        UIManager.instance.characterDialogueUI.SetEndTransitionEnabledEvent(true);
        UIManager.instance.characterDialogueUI.SetIsCloseOnEndEvent(true);
        UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(true);

    }

    public void OpenUI()
    {
        UIManager.instance.characterDialogueUI.SetEndTransitionEnabledEvent(true);
        UIManager.instance.characterDialogueUI.SetIsCloseOnEndEvent(true);
        UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(true);
        onPandaySpokenToEvent.Invoke();


    }
}
