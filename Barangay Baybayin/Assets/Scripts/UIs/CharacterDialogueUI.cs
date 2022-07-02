using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterDialogueUIClose : UnityEvent<bool> { }

public class CharacterDialogueUI : MonoBehaviour
{

    //[HideInInspector] 
    public SO_Character character;
    public SO_Dialogues currentSO_Dialogues;
    [SerializeField]
    private GameObject frame;
    private bool allowNext;
    [SerializeField] private int currentDialogueIndex;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image avatarImage;

    [SerializeField]
    private GameObject emoticon;
    [SerializeField]
    private Animator anim;

    [SerializeField] private string id;

    public static CharacterSpokenToEvent onCharacterSpokenToEvent = new CharacterSpokenToEvent();
    
    private void OnEnable()
    {
        onCharacterSpokenToEvent.AddListener(OnCharacterSpokenTo);

    }

    private void OnDisable()
    {
        onCharacterSpokenToEvent.RemoveListener(OnCharacterSpokenTo);
    }
    public void OnCharacterSpokenTo(string p_id, SO_Dialogues p_SO_Dialogue)
    {
        id = p_id;
        
        currentSO_Dialogues = p_SO_Dialogue;
        UIManager.TransitionPreFadeAndPostFade(1,0.5f,1, 0, 0.5f, OnOpenCharacterDialogueUI);

    }

    public void OnOpenCharacterDialogueUI()
    {

        ResetCharacterDialogueUI();
        UIManager.onGameplayModeChangedEvent.Invoke(true);
    }
    public void OnCloseCharacterDialogueUI()
    {

        frame.SetActive(false);
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
            yield return new WaitForSeconds(0.10f);
        }
    }

    public void ResetCharacterDialogueUI()
    {
        currentDialogueIndex=0;
        allowNext = false;
        OnNextButtonUIPressed();
    }

    public void OnNextButtonUIPressed()
    {
    
        if (currentDialogueIndex < currentSO_Dialogues.dialogues.Count)
        {
            
            Dialogue currentDialogue = currentSO_Dialogues.dialogues[currentDialogueIndex];

            characterNameText.text = currentDialogue.character.name;


            //if (currentDialogue.character.name != "Player")//TEMPORARY
            //{
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

            //}
            //else
            //{
            //    avatarImage.gameObject.SetActive(false);
            //    emoticon.SetActive(false);
            //}

            //temporary
            if (UIManager.instance.runningCoroutine != null)
            {
                UIManager.instance.StopCoroutine(UIManager.instance.runningCoroutine);
                UIManager.instance.runningCoroutine = null;
            }
            if (currentDialogue.speechTransitionType == SpeechTransitionType.Typewriter)
            {
                if (UIManager.instance.runningCoroutine == null)
                {
                    
                    dialogueText.text = currentDialogue.words;
                   
                    if (allowNext == true)
                    {
                        currentDialogueIndex++;
                    }
                
                }
                else
                {
             

                    UIManager.instance.runningCoroutine = Co_TypeWriterEffect(dialogueText, currentDialogue.words);
                    UIManager.instance.StartCoroutine(UIManager.instance.runningCoroutine);
                }
                
            }
            else
            {
                dialogueText.text = currentDialogue.words;
       
                if (allowNext == true)
                {
                    currentDialogueIndex++;
                }

            }
            
          

            if(allowNext == false)
            {


                frame.SetActive(true);
                allowNext = true;
                currentDialogueIndex++;
            }

        }
        else //TEMPORARY END CONVO, BUT EVENTUALLY SHOW AND GIVE QUEST
        {
            StorylineData storylineData = StorylineManager.GetStorylineDataFromID(id);
            int currentQuestChainIndex = storylineData.currentQuestChainIndex;
            int currentQuestLineIndex = storylineData.currentQuestLineIndex;
            StorylineManager.onWorldEventEndedEvent.Invoke(id, currentQuestChainIndex, currentQuestLineIndex);
            UIManager.TransitionPreFadeAndPostFade(1, 0.5f, 1, 0, 0.5f, OnCloseCharacterDialogueUI);
        }
    }
}
