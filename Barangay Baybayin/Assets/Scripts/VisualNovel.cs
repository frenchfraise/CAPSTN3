using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualNovel : MonoBehaviour
{
    public SO_Dialogue so_Dialogue;//TEMPORARY, NEXT TIME PUT IN MANAGER
    public int currentSpeechIndex;
    public TMP_Text characterNameText;
    public TMP_Text speechText;

    //TEMPORARY, NEXT TIME INSTANTIATE THIS IF NEW CHARACTER
    public Image body;
    public Image face;

    public void Text() //temporary initilization
    {
       
        Speech currentSpeech = so_Dialogue.speeches[currentSpeechIndex];

        characterNameText.text = currentSpeech.character.name;
        speechText.text = currentSpeech.speech;
        if (currentSpeech.character.name != "Player")//TEMPORARY
        {
            body.gameObject.SetActive(true);
            face.gameObject.SetActive(true);
            body.sprite = currentSpeech.character.body[currentSpeech.bodyIndex];
            face.sprite = currentSpeech.character.expressions.emotion[(int)currentSpeech.emotion].faces[currentSpeech.faceIndex];

        }
        else
        {
            body.gameObject.SetActive(false);
            face.gameObject.SetActive(false);
        }

     
    }

    public void Next()
    {
        if (currentSpeechIndex + 1 < so_Dialogue.speeches.Count)
        {
            currentSpeechIndex++;
            Speech currentSpeech = so_Dialogue.speeches[currentSpeechIndex];

            characterNameText.text = currentSpeech.character.name;
            speechText.text = currentSpeech.speech;
            if (currentSpeech.character.name != "Player")//TEMPORARY
            {
                body.gameObject.SetActive(true);
                face.gameObject.SetActive(true);
                body.sprite = currentSpeech.character.body[currentSpeech.bodyIndex];
                face.sprite = currentSpeech.character.expressions.emotion[(int)currentSpeech.emotion].faces[currentSpeech.faceIndex];

            }
            else
            {
                body.gameObject.SetActive(false);
                face.gameObject.SetActive(false);
            }

        }
        else //TEMPORARY END CONVO, BUT EVENTUALLY SHOW AND GIVE QUEST
        {
            gameObject.SetActive(false);
        }
    }

    
}
