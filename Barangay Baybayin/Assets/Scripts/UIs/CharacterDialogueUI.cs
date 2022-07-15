using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;


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

    private GameObject emoticonObject;
    private Image emoticonImage;
    private RectTransform emoticonRectTransform;
    [SerializeField] private Image emoticonBubbleImage;
    private RectTransform emoticonBubbleRectTransform;

    [SerializeField] private Animator emoticonAnim;

    [SerializeField] public GameObject nextDialogueButton;
    [SerializeField] private GameObject choiceUIsContainer;
    [SerializeField] private ChoiceUI choiceUIPrefab;

    [HeaderAttribute("ADJUSTABLE VALUES")]
    [SerializeField] private float typewriterSpeed = 0.1f;

    [SerializeField]
    private float avatarFadeTime;
    [SerializeField]
    private float avatarDelayTime;

    [SerializeField]
    private float emoticonFadeTime;
    [SerializeField] private RectTransform defaultEmoticonSize;
    [SerializeField] private RectTransform targetEmoticonSize;
    [SerializeField] private float emoticonSizeTime;

    [SerializeField]
    private float emoticonBubbleFadeTime;
    [SerializeField] private RectTransform defaultEmoticonBubbleSize;
    [SerializeField] private RectTransform targetEmoticonBubbleSize;
    [SerializeField] private float emoticonBubbleSizeTime;


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
    bool firstTime = true;

    bool hasChoices = false;
    public IEnumerator runningCoroutine;
    public IEnumerator runningEmotionCoroutine;
    public static CharacterSpokenToEvent onCharacterSpokenToEvent = new CharacterSpokenToEvent();
    public static SetButtonEnabledEvent onSetButtonEnabledEvent = new SetButtonEnabledEvent();
    public static SetIsAdvancedonWorldEventEndedEvent onSetIsAdvancedonWorldEventEndedEvent = new SetIsAdvancedonWorldEventEndedEvent();
    public static SetIsPausedEvent onSetIsPausedEvent = new SetIsPausedEvent();
    public static SetEndTransitionEnabledEvent onSetEndTransitionEnabledEvent = new SetEndTransitionEnabledEvent();
    public static SetStartTransitionEnabledEvent onSetStartTransitionEnabledEvent = new SetStartTransitionEnabledEvent();
    public static SetIsCloseOnEndEvent onSetIsCloseOnEndEvent = new SetIsCloseOnEndEvent();
    public static SetChoicesEvent onSetChoicesEvent = new SetChoicesEvent();
    private void Awake()
    {
        emoticonRectTransform = emoticonAnim.GetComponent<RectTransform>();
        emoticonObject = emoticonAnim.gameObject;
        emoticonImage = emoticonAnim.GetComponent<Image>();

        emoticonBubbleRectTransform = emoticonBubbleImage.GetComponent<RectTransform>();

    }
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

        

        //emoticonAnim.stop;
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


    public void ResetEmotionUI()
    {

        emoticonImage.color = new Color(emoticonImage.color.r, emoticonImage.color.g, emoticonImage.color.b,0);

        emoticonRectTransform.sizeDelta = defaultEmoticonSize.sizeDelta;

        emoticonObject.SetActive(false);

        emoticonBubbleImage.color = new Color(emoticonBubbleImage.color.r, emoticonBubbleImage.color.g, emoticonBubbleImage.color.b, 0);

        emoticonBubbleRectTransform.sizeDelta = defaultEmoticonBubbleSize.sizeDelta;
    }
    public void ResetCharacterDialogueUI()
    {
        firstTime = true;
        currentDialogueIndex =0;
        allowNext = false;
        isAlreadyEnded = false;
        nextDialogueButton.SetActive(true);
        choiceUIsContainer.SetActive(false);


        avatarImage.color = new Color(avatarImage.color.r, avatarImage.color.g, avatarImage.color.b, 0);

 

        ResetEmotionUI();
        OnNextButtonUIPressed();
    }

    void NextDialogue()
    {
        currentDialogueIndex++;
        //ResetEmotionUI();

  
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
        if (runningEmotionCoroutine != null)
        {
            StopCoroutine(runningEmotionCoroutine);
            runningEmotionCoroutine = null;
            runningEmotionCoroutine = Co_EmotionOut();
            StartCoroutine(runningEmotionCoroutine);
        }


        //Debug.Log(id + " EVENT WITH NAME " + currentSO_Dialogues.name + " IS CURRENT DIALOGUE " + currentDialogueIndex +  " IS CURRENT INDEX OUT OF " + currentSO_Dialogues.dialogues.Count);
        if (currentDialogueIndex < currentSO_Dialogues.dialogues.Count)
        {
            //Debug.Log("within");

            Dialogue currentDialogue = currentSO_Dialogues.dialogues[currentDialogueIndex];

            characterNameText.text = currentDialogue.character.name;


            if ((int)currentDialogue.emotion == 10)
            {
                emoticonObject.SetActive(false);
                emoticonAnim.SetInteger("enum", (int)currentDialogue.emotion);
                avatarImage.gameObject.SetActive(true);
                if (firstTime)
                {
                    avatarImage.DOFade(1,0.1f);
       
                    avatarImage.sprite = currentDialogue.character.avatar;// currentDialogue.character.avatars[(int)currentDialogue.emotion];

                }

            }
            else
            {
                runningEmotionCoroutine = Co_EmotionIn(currentDialogue.character.avatar, currentDialogue.emotion);
                StartCoroutine(runningEmotionCoroutine);
            }
            if (firstTime)
            {
                firstTime = false;
                
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
    IEnumerator Co_EmotionIn(Sprite p_avatar, CharacterEmotionType p_emotion)
    {
        avatarImage.sprite = p_avatar;// currentDialogue.character.avatars[(int)currentDialogue.emotion];
        var sequence = DOTween.Sequence()
        .Append(avatarImage.DOFade(1, avatarFadeTime));
        sequence.Play();
        yield return sequence.WaitForCompletion();
        yield return new WaitForSeconds(avatarDelayTime);
        var sequenceTwo = DOTween.Sequence()
        .Append(emoticonBubbleImage.DOFade(1, emoticonBubbleFadeTime));
        sequenceTwo.Join(emoticonBubbleRectTransform.DOSizeDelta(targetEmoticonBubbleSize.sizeDelta, emoticonBubbleSizeTime, false));
        sequenceTwo.Play();
        yield return new WaitForSeconds(emoticonSizeTime / 2);//sequence.WaitForCompletion();
        emoticonObject.SetActive(true);
        var sequenceThree = DOTween.Sequence()
        .Append(emoticonImage.DOFade(1, emoticonFadeTime));
        sequenceThree.Join(emoticonRectTransform.DOSizeDelta(targetEmoticonSize.sizeDelta, emoticonSizeTime, false));
        sequenceThree.Play();
        emoticonAnim.SetInteger("enum", (int)p_emotion);
    

    }


    IEnumerator Co_EmotionOut()
    {
        var sequenceThree = DOTween.Sequence()
         .Append(emoticonImage.DOFade(0, emoticonFadeTime));
        sequenceThree.Join(emoticonRectTransform.DOSizeDelta(defaultEmoticonSize.sizeDelta, emoticonSizeTime, false));
        sequenceThree.Play();
        yield return new WaitForSeconds(emoticonSizeTime / 2);//sequence.WaitForCompletion();

        var sequenceTwo = DOTween.Sequence()
         .Append(emoticonBubbleImage.DOFade(0, emoticonBubbleFadeTime));
        sequenceTwo.Join(emoticonBubbleRectTransform.DOSizeDelta(defaultEmoticonBubbleSize.sizeDelta, emoticonBubbleSizeTime, false));
        sequenceTwo.Play();
        yield return sequenceTwo.WaitForCompletion();
        emoticonObject.SetActive(false);
    }
}
