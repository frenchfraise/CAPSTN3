using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Character : InteractibleObject
{

    //[SerializeField] private SO_StoryLine so_StoryLine;
    //[SerializeField]
    //private int storylineIndex;
    [SerializeField]
    private string id;
    [SerializeField] private bool isFirstTime = true;
    

    
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
        // Debug.Log("wee");
        bool isQuestCompleted = StorylineManager.instance.CheckIfQuestComplete(id);
   

        StorylineData storylineData = StorylineManager.GetStorylineDataFromID(id);
        SO_StoryLine so_StoryLine = storylineData.so_StoryLine;

        if (storylineData.currentQuestChainIndex < so_StoryLine.questLines.Count)
        {
      
            SO_Questline so_Questline = so_StoryLine.questLines[storylineData.currentQuestChainIndex];
            Debug.Log("PHASE " + storylineData.currentQuestLineIndex + " - " + so_Questline.questlineData.Count);
            if (storylineData.currentQuestLineIndex < so_Questline.questlineData.Count)
            {
                Debug.Log("PHASE 1");
                QuestlineData questlineData = so_Questline.questlineData[storylineData.currentQuestLineIndex];
               
                if (isFirstTime)
                {
                    isFirstTime = false;
                    CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id,questlineData.initialSO_Dialogues);

                }
                else
                {
                    if (isQuestCompleted)
                    {
                        Debug.Log("PHASE 2");
                        
                        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, questlineData.questCompleteSO_Dialogues);
                        
                        isFirstTime = true;
                    }
                    else
                    {
                        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, questlineData.questInProgressSO_Dialogues);
                    }
                        
                }
                
            }
        }

                
        

    }

    

    
       


}
