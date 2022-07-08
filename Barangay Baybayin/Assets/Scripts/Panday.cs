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
        int chosenInitialPandayDialogueIndex = Random.Range(0,StorylineManager.instance.initialPandayDialogue.Count);
        SO_Dialogues chosenInitialPandayDialogue = StorylineManager.instance.initialPandayDialogue[chosenInitialPandayDialogueIndex];
        CharacterDialogueUI.onSetEndTransitionEnabledEvent.Invoke(false);
        CharacterDialogueUI.onSetIsCloseOnEndEvent.Invoke(false);
        CharacterDialogueUI.onSetChoicesEvent.Invoke(true);
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, chosenInitialPandayDialogue);
        CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(false);
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
