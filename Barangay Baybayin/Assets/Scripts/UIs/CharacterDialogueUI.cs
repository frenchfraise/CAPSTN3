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
    public SO_Dialogues currentSO_Dialogues;
    [SerializeField] private int currentDialogueIndex;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image avatarImage;

    public CharacterDialogueUIClose onCharacterDialogueUIClose = new CharacterDialogueUIClose();
   
    public void OnCharacterSpokenTo(SO_Dialogues p_SO_Dialogues)
    {
        currentSO_Dialogues = p_SO_Dialogues;
        UIManager.TransitionPreFadeAndPostFade(1,0.5f,1, 0, 0.5f, OnOpenCharacterDialogueUI);

    }

    public void OnOpenCharacterDialogueUI()
    {
        
        UIManager.instance.gameplayHUD.SetActive(false);
        UIManager.instance.overlayCanvas.SetActive(false);
        
        ResetCharacterDialogueUI();
        onCharacterDialogueUIClose.Invoke(false);
    }
    public void OnCloseCharacterDialogueUI()
    {

        UIManager.instance.gameplayHUD.SetActive(true);
        UIManager.instance.overlayCanvas.SetActive(true);
        gameObject.SetActive(false);

        onCharacterDialogueUIClose.Invoke(true);
    }

    public IEnumerator Co_TypeWriterEffect(TMP_Text p_textUI,string p_fullText)
    {
        string p_currentText;
        for (int i=0; i <= p_fullText.Length; i++)
        {
            p_currentText = p_fullText.Substring(0, i);
            p_textUI.text = p_currentText;
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void ResetCharacterDialogueUI()
    {
        currentDialogueIndex=0;
        if (currentDialogueIndex < currentSO_Dialogues.dialogues.Count)
        {
            
            Dialogue currentDialogue = currentSO_Dialogues.dialogues[currentDialogueIndex];

            characterNameText.text = currentDialogue.character.name;
            if (UIManager.instance.runningCoroutine != null)
            {
                UIManager.instance.StopCoroutine(UIManager.instance.runningCoroutine);
            }
            //temporary
            if (currentDialogue.speechTransitionType == SpeechTransitionType.Typewriter)
            {

                UIManager.instance.runningCoroutine = UIManager.instance.StartCoroutine(Co_TypeWriterEffect(dialogueText,currentDialogue.words));
            }
            else
            {
                dialogueText.text = currentDialogue.words;
            }
         
            if (currentDialogue.character.name != "Player")//TEMPORARY
            {
                avatarImage.gameObject.SetActive(true);

                avatarImage.sprite = currentDialogue.character.avatars[(int)currentDialogue.emotion];

            }
            else
            {
                avatarImage.gameObject.SetActive(false);

            }
            gameObject.SetActive(true);

        }
        //else //TEMPORARY END CONVO, BUT EVENTUALLY SHOW AND GIVE QUEST
        //{
        //    gameObject.SetActive(false);
        //}
    }

    public void OnNextButtonUIPressed()
    {
    
        if (currentDialogueIndex + 1 < currentSO_Dialogues.dialogues.Count)
        {
            currentDialogueIndex++;
            Dialogue currentDialogue = currentSO_Dialogues.dialogues[currentDialogueIndex];

            characterNameText.text = currentDialogue.character.name;
            //temporary
            if (UIManager.instance.runningCoroutine != null)
            {
                UIManager.instance.StopCoroutine(UIManager.instance.runningCoroutine);
            }
            if (currentDialogue.speechTransitionType == SpeechTransitionType.Typewriter)
            {
                UIManager.instance.runningCoroutine = UIManager.instance.StartCoroutine(Co_TypeWriterEffect(dialogueText, currentDialogue.words));
            }
            else
            {
                dialogueText.text = currentDialogue.words;
            }
            if (currentDialogue.character.name != "Player")//TEMPORARY
            {
                avatarImage.gameObject.SetActive(true);

                avatarImage.sprite = currentDialogue.character.avatars[(int)currentDialogue.emotion];

            }
            else
            {
                avatarImage.gameObject.SetActive(false);

            }

        }
        else //TEMPORARY END CONVO, BUT EVENTUALLY SHOW AND GIVE QUEST
        {

            UIManager.TransitionPreFadeAndPostFade(1, 0.5f, 1, 0, 0.5f, OnCloseCharacterDialogueUI);
        }
    }
}
