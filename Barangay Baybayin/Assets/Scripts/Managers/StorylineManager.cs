using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldEventEndedEvent : UnityEvent<string, int, int> { };

[System.Serializable]
public class StorylineData
{
    [SerializeField] public SO_StoryLine so_StoryLine;
    public int currentQuestChainIndex;
    public int currentQuestLineIndex;


}
public class StorylineManager : MonoBehaviour
{

    public static WorldEventEndedEvent onWorldEventEndedEvent = new WorldEventEndedEvent();
    private static StorylineManager _instance;
    public static StorylineManager instance

        
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<StorylineManager>();
            }

            return _instance;
        }
    }
    [NonReorderable][SerializeField] public List<StorylineData> storyLines;

    [HeaderAttribute("PANDAY")]
    
    [NonReorderable][SerializeField] public List<SO_Dialogues> initialPandayDialogue;
    [NonReorderable][HideInInspector] public List<SO_Dialogues> currentPandaySODialogues;
    [NonReorderable][SerializeField] public List<SO_Dialogues> potentialPandaySODialogues;
    [NonReorderable][SerializeField] public SO_Dialogues finishedPandayDialogue;
    private void Awake()
    {
        currentPandaySODialogues = potentialPandaySODialogues;

        _instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
    
    }

    private void OnDisable()
    {
        
    }
    public static int GetStorylineIndexFromID(string p_ID)
    {
        if (p_ID == "Q-LP")
        {
            return 0;
        }
        else  if (p_ID == "Q-KS") //temp
        {
            return 1;
        }
        else if (p_ID  == "Q-TA")
        {
            return 2;
        }
        else if (p_ID == "Q-BC") //temp
        {
            return 3;
        }
        else if (p_ID == "Q-P") //temp
        {
            Debug.Log("CALLED");
            return 4;
        }
        return 0;
    }

    public static StorylineData GetStorylineDataFromID(string p_ID)
    {
        int index = StorylineManager.GetStorylineIndexFromID(p_ID);
        return StorylineManager.instance.storyLines[index];

    }

    public bool CheckIfQuestComplete(string p_id)
    {

        StorylineData storylineData = StorylineManager.GetStorylineDataFromID(p_id);
        SO_StoryLine so_StoryLine = storylineData.so_StoryLine;
        SO_Questline so_Questline = so_StoryLine.questLines[storylineData.currentQuestChainIndex];

        QuestlineData questlineData = so_Questline.questlineData[storylineData.currentQuestLineIndex];
        SO_Quest currentQuest = questlineData.quest;
        int questResourcesFound = 0;

        foreach (QuestRequirement currentQuestRequirement in currentQuest.requirements)
        {

            //Check quest requirement type
            //Debug.Log("PH: 1");
            if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_ItemRequirement))
            {
               // Debug.Log("PH: 2");
                SO_ItemRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_ItemRequirement;
                for (int i = 0; i < specificQuestRequirement.so_Item.Count; i++)
                {
                 //   Debug.Log("PH: 3");
                    SO_Item currentSOItemQuestRequirement = specificQuestRequirement.so_Item[i];
                    //look for item in inventory
                    ItemData itemData = InventoryManager.GetItem(currentSOItemQuestRequirement);
                    if (itemData != null)
                    {
                     //   Debug.Log("PH: 4");
                        if (itemData.amount >= specificQuestRequirement.requiredAmount[i])
                        {
                         //   Debug.Log("PH: 5");
                            questResourcesFound++;

                        }
                        else
                        {

                        }
                    }
                }


            }
            else if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_InfrastructureRequirement))
            {
                SO_InfrastructureRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_InfrastructureRequirement;
                SO_Infrastructure currentSOInrastructureQuestRequirement = specificQuestRequirement.so_infrastructure;
                //look for item in inventory
                Infrastructure itemData = InfrastructureManager.GetInfrastructure(currentSOInrastructureQuestRequirement);
                if (itemData != null)
                {
                    if (itemData.currentLevel >= specificQuestRequirement.requiredLevel)
                    {
                        questResourcesFound++;

                    }
                    else
                    {

                    }
                }
            }

        }
       // Debug.Log(questResourcesFound + " " + currentQuest.requirements.Count);

        bool isQuestCompleted = false;
      //  Debug.Log("PH: 8");
        foreach (QuestRequirement currentQuestRequirement in currentQuest.requirements)
        {

            //Check quest requirement type
           // Debug.Log("PH: 9");
            if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_ItemRequirement))
            {
              //  Debug.Log("PH: 10");
                SO_ItemRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_ItemRequirement;
                if (questResourcesFound == specificQuestRequirement.so_Item.Count)
                {
                    for (int i = 0; i < specificQuestRequirement.so_Item.Count; i++)
                    {
                   //     Debug.Log("PH: 11");
                        SO_Item currentSOItemQuestRequirement = specificQuestRequirement.so_Item[i];
                        //look for item in inventory
                    //   
                    //    Debug.Log("PH: 12");

                        InventoryManager.ReduceItem(currentSOItemQuestRequirement,
                                specificQuestRequirement.requiredAmount[i]);
                        //QuestCompleted(storylineData);
                        isQuestCompleted = true;
                           
                        
                    }
                }

            }
            else if (currentQuestRequirement.so_requirement.GetType() == typeof(SO_InfrastructureRequirement))
            {
                SO_InfrastructureRequirement specificQuestRequirement = currentQuestRequirement.so_requirement as SO_InfrastructureRequirement;
                if (questResourcesFound == 1)
                {

                    SO_Infrastructure currentSOInrastructureQuestRequirement = specificQuestRequirement.so_infrastructure;
                    //look for item in inventory
                    Infrastructure itemData = InfrastructureManager.GetInfrastructure(currentSOInrastructureQuestRequirement);
                    if (itemData != null)
                    {
                        if (itemData.currentLevel >= specificQuestRequirement.requiredLevel)
                        {
                            isQuestCompleted = true;
                            //QuestCompleted(storylineData);
                        }
                        else
                        {

                        }
                    }
                }

            }

        }

        return isQuestCompleted;





    }

    public void QuestCompleted(StorylineData p_storylineData)
    {
        
  
        SO_StoryLine so_StoryLine = p_storylineData.so_StoryLine;
        SO_Questline so_Questline = so_StoryLine.questLines[p_storylineData.currentQuestChainIndex];
        QuestlineData questlineData = so_Questline.questlineData[p_storylineData.currentQuestLineIndex];

        //give reward
        for (int i = 0; i < questlineData.quest.rewards.Count; i++)
        {
            Debug.Log("REWARDING");
            InventoryManager.AddItem(questlineData.quest.rewards[i].so_Item,
                questlineData.quest.rewards[i].amount);
        }
        Debug.Log(p_storylineData.currentQuestLineIndex + " TESTIN" + (so_Questline.questlineData.Count -1).ToString());
        if (p_storylineData.currentQuestLineIndex < so_Questline.questlineData.Count - 1)
        {
            Debug.Log("WENT IN");

            p_storylineData.currentQuestLineIndex++;

        }
        else if (p_storylineData.currentQuestLineIndex >= so_Questline.questlineData.Count - 1) // No More Questline, move to questchain
        {
            Debug.Log("WENT INCREaaase");
            p_storylineData.currentQuestLineIndex = 0;
            if (p_storylineData.currentQuestChainIndex < so_StoryLine.questLines.Count - 1)
            {
                Debug.Log("WENT INCREaaaseAAAAAAAAAD");
                p_storylineData.currentQuestChainIndex++;
            }
            else
            {
                Debug.Log("STORYLINE FINISHED");
            }
        }
        
        
        
    }
}
