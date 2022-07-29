using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;
public class CharacterSpokenToEvent : UnityEvent<string, SO_Dialogues> { }
public class FirstTimeFoodOnEndEvent : UnityEvent { }

public class CharacterDialogueUI : MonoBehaviour
{
    [HeaderAttribute("REQUIRED COMPONENTS")]
    [SerializeField] private GameObject frame;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image avatarImage;
    [SerializeField] private Image backgroundImage;

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

    bool isAdvancedonWorldEventEndedEvent = false;

    bool isAlreadyEnded = false;
    //bool firstTime = true;
    Sprite cachedSprite;

    bool firstfirstTimeTutorial = false;
    public bool firstTimeTutorial = false;
    public bool firstTimeTut = false;
    bool hasChoices = false;
    public IEnumerator runningCoroutine;
    public IEnumerator runningEmotionCoroutine;
    public IEnumerator runningAvatarCoroutine;
    public static CharacterSpokenToEvent onCharacterSpokenToEvent = new CharacterSpokenToEvent();
    public static FirstTimeFoodOnEndEvent onFirstTimeFoodOnEndEvent = new FirstTimeFoodOnEndEvent();
    bool canDo = false;

   [SerializeField] private bool isFoodFirstTime = false;
    public void Skip()
    {
        firstfirstTimeTutorial = true;
    }
    void FirstTimeFoodOnEndEvent()
    {
        isFoodFirstTime = true;
    }
    private void Awake()
    {
        emoticonRectTransform = emoticonAnim.GetComponent<RectTransform>();
        emoticonObject = emoticonAnim.gameObject;
        emoticonImage = emoticonAnim.GetComponent<Image>();
        onFirstTimeFoodOnEndEvent.AddListener(FirstTimeFoodOnEndEvent);
        emoticonBubbleRectTransform = emoticonBubbleImage.GetComponent<RectTransform>();
 
        firstfirstTimeTutorial = false;
        isFoodFirstTime = false;
        onCharacterSpokenToEvent.AddListener(OnCharacterSpokenTo);
   
        Panday.onPandaySpokenToEvent.AddListener(UniqueGameplayModeChangedEvent);
        UIManager.onGameplayModeChangedEvent.AddListener(GameplayModeChangedEvent);

    }

    private void OnDestroy()
    {
        onFirstTimeFoodOnEndEvent.RemoveListener(FirstTimeFoodOnEndEvent);
        onCharacterSpokenToEvent.RemoveListener(OnCharacterSpokenTo);

        Panday.onPandaySpokenToEvent.RemoveListener(UniqueGameplayModeChangedEvent);
        UIManager.onGameplayModeChangedEvent.RemoveListener(GameplayModeChangedEvent);

    }

    public void UniqueGameplayModeChangedEvent()
    {
        frame.SetActive(false);

    }
    public void GameplayModeChangedEvent(bool p_set)
    {   

    }

    public void SetChoicesEvent(bool p_bool)
    {
        hasChoices = p_bool;
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
            TransitionUI.onFadeInAndOutTransition.Invoke(1, 0.5f, 1, 0, 0.5f, OnOpenCharacterDialogueUI);
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
        canDo = true;
       // Debug.Log("WHO IS CALLING");
        frame.SetActive(true);
        //Debug.Log(id + " EVENT WITH NAME " + currentSO_Dialogues.name + " IS CURRENT DIALOGUE " + " OPENING");
        ResetCharacterDialogueUI();
        //TimeManager.onPauseGameTime.Invoke(false);

        UIManager.onGameplayModeChangedEvent.Invoke(true);
    }
    public void OnCloseCharacterDialogueUI()
    {
        if (!TimeManager.instance.tutorialOn) // TEMPORARY; Eli insisted
        {
            
            //string song1 = AudioManager.instance.GetSoundByName("Quest Get").name;
            //string song2 = AudioManager.instance.GetSoundByName("Town").name;

            //AudioManager.instance.StartCoFade(song1, song2);
            AudioManager.instance.PlayOnRoomEnterString("Town");
            
           
            //  Debug.Log("TRYING TO QUEST TEACH");
            if (firstTimeTutorial)
            {
              //  Debug.Log("TEACH QUEST IN");
                //if (firstfirstTimeTutorial)
                //{
                    firstTimeTutorial = false;
                    //   Debug.Log("IT SHUD BE WORKING TEACH QUEST");
                    TutorialManager.instance.tutorialUI.RemindTutorialEvent(0);

                //}
                //firstfirstTimeTutorial = true;
            }

            if (isFoodFirstTime)
            {
                isFoodFirstTime = false;
                Food.onFirstTimeFood.Invoke();
            }
            if (TimeManager.instance.dayCount == 3)
            {
                if (!firstTimeTut)
                {
                    firstTimeTut = true;
                    TutorialManager.instance.tutorialUI.RemindTutorialEvent(1);
                }

            }

        }
      
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
        cachedSprite = null;
        //  firstTime = true;
        currentDialogueIndex =0;
        allowNext = false;
        isAlreadyEnded = false;
        nextDialogueButton.SetActive(true);
        choiceUIsContainer.SetActive(false);
       // Debug.Log("WHO IS CALLING");

        avatarImage.color = new Color(avatarImage.color.r, avatarImage.color.g, avatarImage.color.b, 0);

 

        ResetEmotionUI();
        OnNextButtonUIPressed();
    }

    void NextDialogue()
    {
        currentDialogueIndex++;
        //ResetEmotionUI();
      //  Debug.Log("WHO IS CALLING");

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

    public IEnumerator AvatarFade(Sprite p_sprite)
    {
        var sequence = DOTween.Sequence()
       .Append(avatarImage.DOFade(0, avatarFadeTime));
        sequence.Play();

        yield return sequence.WaitForCompletion();
        avatarImage.sprite = p_sprite;
        var sequence2 = DOTween.Sequence()
        .Append(avatarImage.DOFade(1, avatarFadeTime));
        sequence2.Play();

        yield return sequence2.WaitForCompletion();
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
        //Debug.Log("WHO IS CALLING");

        //Debug.Log(id + " EVENT WITH NAME " + currentSO_Dialogues.name + " IS CURRENT DIALOGUE " + currentDialogueIndex +  " IS CURRENT INDEX OUT OF " + currentSO_Dialogues.dialogues.Count);
        if (currentDialogueIndex < currentSO_Dialogues.dialogues.Count)
        {
           
            //Debug.Log("within");

            Dialogue currentDialogue = currentSO_Dialogues.dialogues[currentDialogueIndex];

            characterNameText.text = currentDialogue.character.name;
            if (currentDialogue.backgroundSprite != null)
            {
                backgroundImage.sprite = currentDialogue.backgroundSprite;
                backgroundImage.color = new Color32(255, 255, 255, 255);
            }
            else if (currentDialogue.backgroundSprite == null)
            {
                backgroundImage.color = new Color32(0, 0, 0, 0);
            }

            if ((int)currentDialogue.emotion == 10)
            {
                emoticonObject.SetActive(false);
                emoticonAnim.SetInteger("enum", (int)currentDialogue.emotion);
                avatarImage.gameObject.SetActive(true);
                if (cachedSprite != currentDialogue.character.avatar)
                {
                   
                   // avatarImage.DOFade(1,0.5f);
                    if (runningAvatarCoroutine != null)
                    {
                        StopCoroutine(runningAvatarCoroutine);
                        runningAvatarCoroutine = null;
                    }
                    runningAvatarCoroutine = AvatarFade(currentDialogue.character.avatar);
                    StartCoroutine(runningAvatarCoroutine);
                    cachedSprite = currentDialogue.character.avatar;
                }

            }
            else
            {
                //if (runningEmotionCoroutine != null)
                //{
                //    StopCoroutine(runningEmotionCoroutine);
                //    runningEmotionCoroutine = null;
                //}
                runningEmotionCoroutine = Co_EmotionIn(currentDialogue.character.avatar, currentDialogue.emotion);
                StartCoroutine(runningEmotionCoroutine);
            }
            //if (firstTime)
            //{
            //    firstTime = false;
                
            //}

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

                    if (runningCoroutine != null)
                    {
                        StopCoroutine(runningCoroutine);
                        runningCoroutine = null;
                    }
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
            if (canDo)
            {
                canDo = false;
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
                        
                            Debug.Log("AUTO CLOSE BEING DONE");
                            if (isEndTransitionEnabled)
                            {

                                Debug.Log("END TRANSIONING");
                               // TransitionUI.onFadeInAndOutTransition.Invoke(1, 0.5f, 1, 0, 0.5f, OnCloseCharacterDialogueUI);
                                TransitionUI.onFadeInAndOutTransition.Invoke(1, 0.25f, 1, 0, 0.25f, OnCloseCharacterDialogueUI);

                            }
                            else
                            {
                                Debug.Log("END WITHOUT TRANSIONING");
                                OnCloseCharacterDialogueUI();
                            }

                       
                        
                    }

                }
            }
            
          
            
        }
    }
    IEnumerator Co_EmotionIn(Sprite p_avatar, CharacterEmotionType p_emotion)
    {
        avatarImage.color = new Color(avatarImage.color.r, avatarImage.color.g, avatarImage.color.b, 0);
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
