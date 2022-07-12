using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;


public class SetChoicesEvent : UnityEvent<bool> { }
public class SetIsCloseOnEndEvent : UnityEvent<bool> { }
public class SetStartTransitionEnabledEvent : UnityEvent<bool> { }
public class SetEndTransitionEnabledEvent : UnityEvent<bool> { }
public class SetButtonEnabledEvent : UnityEvent<bool> { }
public class SetIsPausedEvent : UnityEvent<bool> { }
public class SetIsAdvancedonWorldEventEndedEvent : UnityEvent<bool> { }
public class CharacterSpokenToEvent : UnityEvent<string, SO_Dialogues> { }

public class CharacterDialogueUI : MonoBehaviour
{
    [HeaderAttribute("REQUIRED COMPONENTS")]
    [SerializeField] private GameObject frame;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image avatarImage;

    [SerializeField] private GameObject emoticon;
    [SerializeField] private Animator anim;

    [SerializeField] public GameObject nextDialogueButton;
    [SerializeField] private GameObject choiceUIsContainer;
    [SerializeField] private ChoiceUI choiceUIPrefab;

    [HeaderAttribute("ADJUSTABLE VALUES")]
    [SerializeField] private float typewriterSpeed = 0.1f;

    [HideInInspector]
    public SO_Character character;

    [HideInInspector]
    public SO_Dialogues currentSO_Dialogues;

    private int currentDialogueIndex;

    private bool isPaused = false;
    private bool allowNext;

    private string id;

    bool isStartTransitionEnabled = true;
    bool isEndTransitionEnabled = true;
    bool isCloseOnEnd = true;

    bool isAdvancedonWorldEventEndedEvent = false;

    bool isAlreadyEnded = false;


    bool hasChoices = false;
    public IEnumerator runningCoroutine;
    public static CharacterSpokenToEvent onCharacterSpokenToEvent = new CharacterSpokenToEvent();
    public static SetButtonEnabledEvent onSetButtonEnabledEvent = new SetButtonEnabledEvent();
    public static SetIsAdvancedonWorldEventEndedEvent onSetIsAdvancedonWorldEventEndedEvent = new SetIsAdvancedonWorldEventEndedEvent();
    public static SetIsPausedEvent onSetIsPausedEvent = new SetIsPausedEvent();
    public static SetEndTransitionEnabledEvent onSetEndTransitionEnabledEvent = new SetEndTransitionEnabledEvent();
    public static SetStartTransitionEnabledEvent onSetStartTransitionEnabledEvent = new SetStartTransitionEnabledEvent();
    public static SetIsCloseOnEndEvent onSetIsCloseOnEndEvent = new SetIsCloseOnEndEvent();
    public static SetChoicesEvent onSetChoicesEvent = new SetChoicesEvent();

    private void OnEnable()
    {

        onCharacterSpokenToEvent.AddListener(OnCharacterSpokenTo);
        onSetIsAdvancedonWorldEventEndedEvent.AddListener(SetIsAdvancedonWorldEventEndedEvent);
        onSetIsPausedEvent.AddListener(SetIsPausedEvent);
        onSetButtonEnabledEvent.AddListener(SetButtonEnabledEvent);
        onSetStartTransitionEnabledEvent.AddListener(SetStartTransitionEnabledEvent);
        onSetEndTransitionEnabledEvent.AddListener(SetEndTransitionEnabledEvent);
        onSetChoicesEvent.AddListener(SetChoicesEvent);
        Panday.onPandaySpokenToEvent.AddListener(GameplayModeChangedEvent);
    }
    private void OnDisable()
    {
        onCharacterSpokenToEvent.RemoveListener(OnCharacterSpokenTo);
    }

    public void GameplayModeChangedEvent()
    {
        
            
        frame.SetActive(false);
     
        
    
    }

    public void SetChoicesEvent(bool p_bool)
    {
        hasChoices = p_bool;
    }
    public void SetIsCloseOnEndEvent(bool p_bool)
    {
        isCloseOnEnd = p_bool;
    }
    public void SetButtonEnabledEvent(bool p_bool)
    {
        nextDialogueButton.SetActive(p_bool);
    }

    public void SetIsAdvancedonWorldEventEndedEvent(bool p_bool)
    {
        isAdvancedonWorldEventEndedEvent = p_bool;


    }
    public void SetIsPausedEvent(bool p_bool)
    {
        isPaused = p_bool;


    }

    public void SetStartTransitionEnabledEvent(bool p_bool)
    {
        isStartTransitionEnabled = p_bool;


    }
    public void SetEndTransitionEnabledEvent(bool p_bool)
    {
        isEndTransitionEnabled = p_bool;


    }

    public void OnCharacterSpokenTo(string p_id, SO_Dialogues p_SO_Dialogue)
    {
        id = p_id;
        currentSO_Dialogues = p_SO_Dialogue;
       // Debug.Log(id + " EVENT WITH NAME " + currentSO_Dialogues.name + " IS CURRENT DIALOGUE " + " IS CHARACTERSPOKENTO CALLED");
        
        
       
        if (isStartTransitionEnabled)
        {
            UIManager.TransitionPreFadeAndPostFade(1, 0.5f, 1, 0, 0.5f, OnOpenCharacterDialogueUI);
        }
        else
        {
           
            OnOpenCharacterDialogueUI();
        }
        

    }

    public void SetChoices()
    {
        
    }

    public void OnOpenCharacterDialogueUI()
    {
        frame.SetActive(true);
        //Debug.Log(id + " EVENT WITH NAME " + currentSO_Dialogues.name + " IS CURRENT DIALOGUE " + " OPENING");
        ResetCharacterDialogueUI();
        //TimeManager.onPauseGameTime.Invoke(false);
        UIManager.onGameplayModeChangedEvent.Invoke(true);
    }
    public void OnCloseCharacterDialogueUI()
    {
        //Debug.Log(id + " EVENT WITH NAME " + currentSO_Dialogues.name + " IS CURRENT DIALOGUE " + " CLOSING");
        frame.SetActive(false);
        //TimeManager.onPauseGameTime.Invoke(true);
        UIManager.onGameplayModeChangedEvent.Invoke(false);
     
        //onCharacterDialogueUIClose.Invoke(true);
    }

    public IEnumerator Co_TypeWriterEffect(TMP_Text p_textUI,string p_fullText)
    {
        string p_currentText;
        for (int i=0; i <= p_fullText.Length; i++)
        {
            p_currentText = p_fullText.Substring(0, i);
            p_textUI.text = p_currentText;
            yield return new WaitForSeconds(typewriterSpeed);
        }
    }

    public void ResetCharacterDialogueUI()
    {
        currentDialogueIndex=0;
        allowNext = false;
        isAlreadyEnded = false;
        nextDialogueButton.SetActive(true);
        choiceUIsContainer.SetActive(false);
        OnNextButtonUIPressed();
    }

    void NextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex == currentSO_Dialogues.dialogues.Count)
        {
            if (isAdvancedonWorldEventEndedEvent)
            {
                StorylineData storylineData = StorylineManager.GetStorylineDataFromID(id);
                int currentQuestChainIndex = storylineData.currentQuestChainIndex;
                int currentQuestLineIndex = storylineData.currentQuestLineIndex;
                StorylineManager.onWorldEventEndedEvent.Invoke(id, currentQuestChainIndex, currentQuestLineIndex);
                //Debug.Log("ADVANCE CALLING");
                isAlreadyEnded = true;


            }
        }
    }
    public void OnNextButtonUIPressed()
    {

        //Debug.Log(id + " EVENT WITH NAME " + currentSO_Dialogues.name + " IS CURRENT DIALOGUE " + currentDialogueIndex +  " IS CURRENT INDEX OUT OF " + currentSO_Dialogues.dialogues.Count);
        if (currentDialogueIndex < currentSO_Dialogues.dialogues.Count)
        {
            //Debug.Log("within");

            Dialogue currentDialogue = currentSO_Dialogues.dialogues[currentDialogueIndex];

            characterNameText.text = currentDialogue.character.name;


            avatarImage.gameObject.SetActive(true);

            avatarImage.sprite = currentDialogue.character.avatar;// currentDialogue.character.avatars[(int)currentDialogue.emotion];

         
            if ((int)currentDialogue.emotion == 10)
            {
                emoticon.SetActive(false);
                anim.SetInteger("enum", (int)currentDialogue.emotion);
            }
            else
            {
                emoticon.SetActive(true);
                anim.SetInteger("enum", (int)currentDialogue.emotion);
            }

         


            if (runningCoroutine != null)
            {
                StopCoroutine(runningCoroutine);
                runningCoroutine = null;
            }
            if (currentDialogue.speechTransitionType == SpeechTransitionType.Typewriter)
            {
                if (runningCoroutine == null)
                {
                    
                    dialogueText.text = currentDialogue.words;
                   
                    if (allowNext == true)
                    {
                        
                        NextDialogue();
                        
                    }
                
                }
                else
                {
             

                    runningCoroutine = Co_TypeWriterEffect(dialogueText, currentDialogue.words);
                    StartCoroutine(runningCoroutine);
                }
                
            }
            else
            {
                dialogueText.text = currentDialogue.words;
       
                if (allowNext == true)
                {

                    NextDialogue();
                }

            }
            
          

            if(allowNext == false) //check this ITS HAPPENS FIRST TIME
            {


                frame.SetActive(true);
                allowNext = true;
                NextDialogue();
          

            }

        }
        else if (currentDialogueIndex >= currentSO_Dialogues.dialogues.Count)        //TEMPORARY END CONVO, BUT EVENTUALLY SHOW AND GIVE QUEST
        {
            if (!isAlreadyEnded)
            {
                //Debug.Log("OUTSIDE");

                if (!isAdvancedonWorldEventEndedEvent)
                {
                    //Debug.Log("NORMAL");
                    StorylineData storylineData = StorylineManager.GetStorylineDataFromID(id);
                    int currentQuestChainIndex = storylineData.currentQuestChainIndex;
                    int currentQuestLineIndex = storylineData.currentQuestLineIndex;
                    StorylineManager.onWorldEventEndedEvent.Invoke(id, currentQuestChainIndex, currentQuestLineIndex);
                }
                if (hasChoices)
                {
                    hasChoices = false;
                    nextDialogueButton.SetActive(false);
                    choiceUIsContainer.SetActive(true);
                }
                else
                {
                    if (isCloseOnEnd)
                    {
                        //Debug.Log("AUTO CLOSE BEING DONE");
                        if (isEndTransitionEnabled)
                        {

                            //Debug.Log("END TRANSIONING");
                            UIManager.TransitionPreFadeAndPostFade(1, 0.5f, 1, 0, 0.5f, OnCloseCharacterDialogueUI);
                        }
                        else
                        {
                            // Debug.Log("END WITHOUT TRANSIONING");
                            OnCloseCharacterDialogueUI();
                        }

                    }
                    else
                    {
                        //Debug.Log("MANUAL CLOSED NEEDED");
                    }
                }
                
            }
          
            
        }
    }
}
