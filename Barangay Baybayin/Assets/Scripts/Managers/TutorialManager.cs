using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TutorialEventEndedEvent : UnityEvent<int> { };
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public Transform spawnLocation;
    public Transform panLimitUpperRightTransform;
    public Vector2 panLimit;
    public Transform startRoom;
    public int currentIndex = 0;
    public int currentDialogueIndex = 0;
    public List<SO_Dialogues> dialogues;

    public TutorialPanelUI tutorialUI;
    [SerializeField] private Stamina stamina;

    public Transform spawnPoint0;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;


    public ResourceNode resourceNode;

    public Infrastructure infrastructure;
    public Infrastructure infrastructureTwo;

    public SO_Dialogues equippingWrongTool;
    public SO_Dialogues swingingWrongTool;
    public SO_Dialogues swingingInAir;
    public SO_Dialogues upgradingWrong;
    public SO_Dialogues needToUpgradeAllTools;
    public SO_Dialogues cantGoThere;

    public TutorialBlocker tutorialBlocker;
    public Transform pandayNormalPosition;

    public bool isFirstTimeFood = true;
    public static TutorialEventEndedEvent onTutorialEventEndedEvent = new TutorialEventEndedEvent();

    public Panday panday;
    public bool firstTime = true;
    bool treeSpawn = true;
    bool oreSpawn = true;
    bool infrastructureSpawn = true;
    bool herbSpawn = true;
    bool allSpawn = true;
    bool oneToolRewardGiven = false;
    bool allToolsRewardGiven = false;
    bool level2tree = false;
    bool characterQuest = true;

    bool dayFirstTime = true;
    bool proceed = true;
    public GameObject upgradeIsOpen;
    public GameObject specialButton;
    private void Awake()
    {
        instance = this;
    
    }

    public void Next()
    {

        StorylineManager.onWorldEventEndedEvent.Invoke("O-" + currentIndex, 0, 0);

    }

    private void Start()
    {
        Setup();
        //DontUseTutorial();   
    }


    void Setup()
    {
        specialButton.SetActive(false);
        panday.canInteract = false;
        CameraManager.instance.tutorialOn = true;
        StorylineManager.onWorldEventEndedEvent.AddListener(TellStory);
        UIManager.onGameplayModeChangedEvent.AddListener(GameplayModeChangedEvent);
        infrastructure.InitializeValues();
        infrastructure.gameObject.SetActive(true);
        infrastructureTwo.InitializeValues();
        infrastructureTwo.gameObject.SetActive(true);
        infrastructureTwo.gameObject.SetActive(false);
        UIManager.instance.characterDialogueUI.SetEndTransitionEnabledEvent(false);
        //UIManager.instance.characterDialogueUI.SetIsCloseOnEndEvent(false);
        UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(false);
        TimeManager.instance.tutorialOn = true;
        TimeManager.onPauseGameTime.Invoke(false);
        PlayerManager.instance.playerStamina.ManualSetStaminaEvent(130);
        PlayerManager.instance.playerTransform.position = spawnLocation.position;
        panLimit = Vector2Abs(startRoom.position - panLimitUpperRightTransform.position);
        CameraManager.onCameraMovedEvent.Invoke(startRoom.position, panLimit);
        tutorialBlocker.gameObject.SetActive(true);
        AudioManager.instance.OnDayChangingEvent();
        StartLecture();
    }

    public void DontUseTutorial()
    {

        ToolCaster.onToolUsedEvent.RemoveListener(TeachUseTool);
        ToolCaster.onToolSpecialUsedEvent.RemoveListener(TeachSpecialUseTool);
  
     
        PlayerManager.instance.DayChanging();
  
        CameraManager.instance.ResetCamera();

        UIManager.instance.characterDialogueUI.Skip();

     
        tutorialBlocker.gameObject.SetActive(false);

        InventoryManager.onAddItemEvent.Invoke("Recipe 1", 4); //it happens 3 times
        InventoryManager.onAddItemEvent.Invoke("Metal 1", 40);

        dayFirstTime = false;
        Same();

    }

    void Unsetup()
    {

        TimeManager.onPauseGameTime.Invoke(true);
        Same();
    }

    void Same()
    {
        PlayerManager.instance.playerToolCaster.SetRequireCorrectToolEvent(false);
        PlayerManager.instance.playerToolCaster.SetIsPrecise(false);
        PlayerManager.instance.playerToolCaster.SetRequireCorrectTool(null);

        ToolsUI.onToolQuestSwitchEvent.Invoke(-1);

        infrastructure.gameObject.SetActive(false);
        infrastructureTwo.gameObject.SetActive(false);

        specialButton.SetActive(true);

        panday.canInteract = true;

        CameraManager.instance.tutorialOn = false;

        TimeManager.instance.tutorialOn = false;

        StorylineManager.onWorldEventEndedEvent.RemoveListener(TellStory);

        UIManager.onGameplayModeChangedEvent.RemoveListener(GameplayModeChangedEvent);
        UIManager.instance.characterDialogueUI.SetEndTransitionEnabledEvent(false);
        UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(true);

        tutorialUI.overheadUI.SetActive(false);

        tutorialBlocker.gameObject.SetActive(false);

        panday.isQuestMode = false;
        panday.transform.position = pandayNormalPosition.position;
        TimeManager.onDayEndedEvent.Invoke(false, 1);
    }
   
    Vector2 Vector2Abs(Vector2 p_vector2)
    {
        Vector2 answer = new Vector2(Mathf.Abs(p_vector2.x), Mathf.Abs(p_vector2.y));
        return answer;
    }
    void StartLecture()
    {
        proceed = true;
        Debug.Log("START LECTURE ID: O-" + currentIndex + " CURRENT INDEX: " + currentIndex + " CURRENT DIALOGUE: " + currentDialogueIndex);
        onTutorialEventEndedEvent.Invoke(currentIndex);
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);

    }
    void TellStory(string p_id, int p_intone, int p_intto)
    {
        tutorialUI.overheadUI.SetActive(false);
        Debug.Log("TELL STORY ID: " + p_id + " CURRENT INDEX: " + currentIndex + " CURRENT DIALOGUE: " + currentDialogueIndex);
   
        if (p_id == "EQUIPPINGWRONGTOOL")
        {

            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("RETURNTOCURRENTTUTORIAL", equippingWrongTool);

        }
        else if (p_id == "SWINGINGINAIR")
        {
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("RETURNTOCURRENTTUTORIAL", swingingInAir);

        }
        else if (p_id == "SWINGINGWRONGTOOL")
        {
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("RETURNTOCURRENTTUTORIAL", swingingWrongTool);

        }
        else if (p_id == "UPGRADINGWRONG")
        {
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("RETURNTOCURRENTTUTORIAL", upgradingWrong);

        }
        else if (p_id == "NEEDTOUPGRADEALLTOOLS")
        {
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("RETURNTOCURRENTTUTORIAL", needToUpgradeAllTools);

        }
        else if (p_id == "CANTGOTHERE")
        {
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("RETURNTOCURRENTTUTORIAL", cantGoThere);

        }
        else if (p_id == "RETURNTOCURRENTTUTORIAL")
        {
            tutorialUI.overheadUI.SetActive(false);
        }
        else if (p_id == "O-0")
        {
            EndStory();


        }
        else if (p_id == "O-1")
        {
            if (treeSpawn)
            {
                treeSpawn = false;
                //SPAWN
                resourceNode = TreeVariantOneNodePool.pool.Get();
                resourceNode.transform.position = spawnPoint0.position + new Vector3(0f, -2.35f, 0f);
                resourceNode.InitializeValues();
                resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachAppropriateToolForNode);

                //SPECIFIC
                ToolsUI.onToolQuestSwitchEvent.Invoke(3);
                PlayerManager.instance.playerToolCaster.SetIsPrecise(true);
                PlayerManager.instance.playerToolCaster.SetRequireCorrectTool(ToolManager.instance.tools[3]);
                //SPECFICI
                EndStory();
            }
          


        }
        else if (p_id == "O-2")
        {
            if (oreSpawn)
            {
                oreSpawn = false;
                //SPAWN
                resourceNode = OreVariantOneNodePool.pool.Get();
                resourceNode.transform.position = spawnPoint0.position + new Vector3(0f, -2.35f, 0f);
                resourceNode.InitializeValues();
                resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachAppropriateToolForNode);

                //SPECIFIC
                ToolsUI.onToolQuestSwitchEvent.Invoke(1);
                PlayerManager.instance.playerToolCaster.SetIsPrecise(true);
                PlayerManager.instance.playerToolCaster.SetRequireCorrectTool(ToolManager.instance.tools[1]);

                //SPECFICI
                EndStory();
            }
          

        }
        else if (p_id == "O-3") // change hint icon
        {
            if (infrastructureSpawn)
            {
                infrastructureSpawn = false; ;
                Debug.Log("SHOULD BE WORKING");
                //SPAWN
                infrastructure.GetComponent<Health>().OnDeathEvent.AddListener(TeachSix);
                infrastructure.canInteract = true;
  
            
                //SPECIFIC
                ToolsUI.onToolQuestSwitchEvent.Invoke(0);
               
                PlayerManager.instance.playerToolCaster.SetIsPrecise(true);
                PlayerManager.instance.playerToolCaster.SetRequireCorrectTool(ToolManager.instance.tools[0]);

                //SPECFICI
                EndStory();
            }
     
        }
        else if (p_id == "O-4")
        {
            if (herbSpawn)
            {
                herbSpawn = false;
                //SPAWN
                resourceNode = HerbVariantOneNodePool.pool.Get();
                resourceNode.transform.position = spawnPoint0.position + new Vector3(0f, -2.35f, 0f);
                resourceNode.InitializeValues();
                resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachAppropriateToolForNode);

                //SPECIFIC
                ToolsUI.onToolQuestSwitchEvent.Invoke(2);
                PlayerManager.instance.playerToolCaster.SetIsPrecise(true);
                PlayerManager.instance.playerToolCaster.SetRequireCorrectTool(ToolManager.instance.tools[2]);
                //SPECFICI
                EndStory();
            }
          
        }
        else if (p_id == "O-5")
        {
            if (allSpawn)
            {
                allSpawn = false;
                //SPAWN
                ResourceNode newResourceNode = TreeVariantOneNodePool.pool.Get();
                newResourceNode.transform.position = spawnPoint1.position + new Vector3(0f, -2.35f, 0f);
                newResourceNode.InitializeValues();

                newResourceNode = OreVariantOneNodePool.pool.Get();
                newResourceNode.transform.position = spawnPoint2.position + new Vector3(0f, -2.35f, 0f);
                newResourceNode.InitializeValues();

                newResourceNode = HerbVariantOneNodePool.pool.Get();
                newResourceNode.transform.position = spawnPoint3.position + new Vector3(0f, -2.35f, 0f);
                newResourceNode.InitializeValues();
                infrastructureTwo.gameObject.SetActive(true);
                infrastructureTwo.canInteract = true;

                //SPECIFIC
            
                PlayerManager.instance.playerToolCaster.SetIsPrecise(true);
                PlayerManager.instance.playerToolCaster.SetRequireCorrectToolEvent(true);
                //SPECFICI

                ToolManager.onProficiencyLevelModifiedEvent.AddListener(RequireMacheteProfLevel1);
                EndStory();
            }
         
        }
        else if (p_id == "O-6")
        {
            Debug.Log("IN");
            if (!oneToolRewardGiven)
            {
                Debug.Log("OUT");
                oneToolRewardGiven = true;
                panday.canInteract = true;
                UIManager.instance.characterDialogueUI.SetEndTransitionEnabledEvent(false);
               // UIManager.instance.characterDialogueUI.SetIsCloseOnEndEvent(true);
                UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(false);
                InventoryManager.onAddItemEvent.Invoke("Recipe 1", 1);
                InventoryManager.onAddItemEvent.Invoke("Metal 1", 10);
                UIManager.instance.upgradeToolsUI.SetSpecificToMachete(true);
                PlayerManager.instance.playerToolCaster.SetRequireCorrectToolEvent(false);
                ToolManager.onToolCraftLevelUpgradedEvent.AddListener(RequireMacheteCraftLevel1);
                EndStory();
            }
           



        }
        else if (p_id == "O-7")
        {
          
            Debug.Log("7 HAPPENEEEEEEEEEEEEEEEEEEEEEED");
            if (!allToolsRewardGiven)
            {
                allToolsRewardGiven = true;
                UIManager.instance.characterDialogueUI.SetEndTransitionEnabledEvent(false);
               // UIManager.instance.characterDialogueUI.SetIsCloseOnEndEvent(true);
                UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(false);
                InventoryManager.onAddItemEvent.Invoke("Recipe 1", 3); //it happens 3 times
                InventoryManager.onAddItemEvent.Invoke("Metal 1", 30);
                UIManager.instance.upgradeToolsUI.SetSpecificToMachete(false);
                UIManager.instance.upgradeToolsUI.SetSpecificToAllOther(true);
     
                ToolManager.onToolCraftLevelUpgradedEvent.AddListener(RequireAllToolsCraftLevel1);
                EndStory();
            }


        }
        else if (p_id == "O-8")
        {
            if (!level2tree)
            {
                UIManager.instance.characterDialogueUI.SetEndTransitionEnabledEvent(false);
               // UIManager.instance.characterDialogueUI.SetIsCloseOnEndEvent(true);
                UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(false);
                level2tree = true;
                UIManager.instance.upgradeToolsUI.SetSpecificToAllOther(false);
                panday.isQuestMode = true;
                Debug.Log("8 HAPPENEEEEEEEEEEEEEEEEEEEEEED");

                UIManager.instance.characterDialogueUI.firstTimeTutorial = true;
                ResourceNode newResourceNode = TreeVariantTwoNodePool.pool.Get();
                newResourceNode.transform.position = spawnPoint2.position + new Vector3(0f, -2.35f, 0f);
                newResourceNode.InitializeValues();
                PlayerManager.instance.playerToolCaster.SetRequireCorrectToolEvent(true);
                PlayerManager.instance.playerToolCaster.SetIsPrecise(true);
                
                
                StorylineManager.onWorldEventEndedEvent.AddListener(RequirePandayQuestComplete);


                EndStory();
            }
            

        }
        else if (p_id == "O-9")
        {
            Debug.Log("HELP");
        }
        else if (p_id == "Q-P")
        {
            if (p_intto == 0)
            {
                if (panday.complete == false)
                {
                    if (firstTime)
                    {
                        firstTime = false;
                        EndStory();
                    }
    
                }
                else
                {
                    
                    Debug.Log("QUEST COMPLETED");
                    Debug.Log("END");
                    Unsetup();
                    
                }
                
            }
            else if (p_intto == 1)
            {
                Debug.Log("QUEST COMPLETED 1 ");
                Debug.Log("END 111111");
                Unsetup();
            }
        }

    }

    void EndStory()
    {
     
        currentDialogueIndex++;
        tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;
        if (currentIndex == 0)
        {
            UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(false);
            //SPECIFIC
            ToolsUI.onToolQuestSwitchEvent.Invoke(3);
  
            PlayerManager.instance.playerToolCaster.SetIsPrecise(false);
            PlayerManager.instance.playerToolCaster.SetRequireCorrectTool(ToolManager.instance.tools[3]);

            //SPECFICI
            ToolCaster.onToolUsedEvent.AddListener(TeachUseTool);
            ToolCaster.onToolSpecialUsedEvent.AddListener(TeachSpecialUseTool);
        }
       


    }
    void EndLecture()
    {
        if (proceed)
        {
            proceed = false;
            Debug.Log("END LECTURE ID: O-" + currentIndex + " CURRENT INDEX: " + currentIndex + " CURRENT DIALOGUE: " + currentDialogueIndex);
            currentIndex++;
            currentDialogueIndex++;
            if (currentIndex == 10)
            {
                Debug.Log("END");
                Unsetup();
            }
            else
            {
                StartLecture();
            }
        }
     


    }


    void GameplayModeChangedEvent(bool p_set)
    {
        tutorialUI.overheadUI.SetActive(!p_set);
       if (currentIndex == 6 || currentIndex == 7 || currentIndex == 8)
       {
            if(upgradeIsOpen.activeSelf)
            {
                tutorialUI.overheadUI.SetActive(false);
            }
            else
            {
                tutorialUI.overheadUI.SetActive(true);
            }
     
       }

    }

    public void TeachSpecialUseTool() //You�re getting the hang of it, son. But what if you overdo it? Try swinging your axe until you wipe out.
    {

        ToolCaster.onToolUsedEvent.RemoveListener(TeachUseTool);
        ToolCaster.onToolSpecialUsedEvent.RemoveListener(TeachSpecialUseTool);
        //SPECIFIC RESET
        ToolsUI.onToolQuestSwitchEvent.Invoke(-1);
        PlayerManager.instance.playerToolCaster.SetIsPrecise(false);
        PlayerManager.instance.playerToolCaster.SetRequireCorrectTool(null);

   
        //SPECFICI RESET
        EndLecture();
    }
    public void TeachUseTool(float p_useless) //You�re getting the hang of it, son. But what if you overdo it? Try swinging your axe until you wipe out.
    {

        ToolCaster.onToolUsedEvent.RemoveListener(TeachUseTool);
        ToolCaster.onToolSpecialUsedEvent.RemoveListener(TeachSpecialUseTool);
        //SPECIFIC RESET
        ToolsUI.onToolQuestSwitchEvent.Invoke(-1);
    
        PlayerManager.instance.playerToolCaster.SetIsPrecise(false);
        PlayerManager.instance.playerToolCaster.SetRequireCorrectTool(null);

        //SPECFICI RESET
        EndLecture();
    }
    public void TeachAppropriateToolForNode() 
    {
        resourceNode.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachAppropriateToolForNode);
        resourceNode = null;

        //SPECIFIC
        ToolsUI.onToolQuestSwitchEvent.Invoke(-1);
      
        PlayerManager.instance.playerToolCaster.SetIsPrecise(false);
        PlayerManager.instance.playerToolCaster.SetRequireCorrectTool(null);

        //SPECFICI

        //Debug.Log("AAAAAAAAAAAAAAAAAAA: " + currentIndex);
        EndLecture();
    }
  

    public void TeachSix()
    {
        infrastructure.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachSix);

        //SPECIFIC
        ToolsUI.onToolQuestSwitchEvent.Invoke(-1);


        PlayerManager.instance.playerToolCaster.SetIsPrecise(false);
        PlayerManager.instance.playerToolCaster.SetRequireCorrectTool(null);

        //SPECFICI

        EndLecture();

    }

    public void RequireMacheteProfLevel1(int p_level)
    {
        bool passed = true;
        for (int i = 0; i < ToolManager.instance.tools.Count;)
        {
            Tool selected_Tool = ToolManager.instance.tools[i];
            if (ToolManager.instance.tools[i].proficiencyLevel < 1)
            {
                passed = false;
                break;
            }
            i++;

        }
        if (passed)
        {
            //SPECIFIC
            ToolManager.onProficiencyLevelModifiedEvent.RemoveListener(RequireMacheteProfLevel1);
            PlayerManager.instance.playerToolCaster.SetIsPrecise(false);
     

            //SPECIFIC
            EndLecture();
        }



    }
  
    public void RequireMacheteCraftLevel1(int p_i)
    {
        bool passed = true;
        Debug.Log("STARTED " + ToolManager.instance.tools[p_i].craftLevel);
        if (p_i == 2)
        {
            Tool selected_Tool = ToolManager.instance.tools[p_i];
            if (ToolManager.instance.tools[p_i].craftLevel < 1)
            {
                Debug.Log("FAILED");
                passed = false;

            }
        }
        
          
        if (passed)
        {
            ToolManager.onToolCraftLevelUpgradedEvent.RemoveListener(RequireMacheteCraftLevel1);
            EndLecture();
        }



    }

    public void RequireAllToolsCraftLevel1(int p_i)
    {
        bool passed = true;
        for (int i = 0; i < ToolManager.instance.tools.Count;)
        {
            Tool selected_Tool = ToolManager.instance.tools[i];
            if (ToolManager.instance.tools[i].craftLevel < 1)
            {
                passed = false;
                break;
            }
            i++;

        }
        if (passed)
        {
            
            ToolManager.onToolCraftLevelUpgradedEvent.RemoveListener(RequireAllToolsCraftLevel1);
            EndLecture();
        }

    }

    public void RequirePandayQuestComplete(string p_id, int p_test, int p_testt)
    {
        Debug.Log(p_id + " - " + p_test + " - " + p_testt);
        if (p_id == "Q-P")
        {
            if (p_test == 0)
            {
                //if (p_testt == 1)
                //{
                if (characterQuest)
                {
                    characterQuest = false;
                    StorylineManager.onWorldEventEndedEvent.RemoveListener(RequirePandayQuestComplete);
                    Debug.Log("QUEST COMPLETED");
                    EndLecture();
                }
              
               // }
            }

        }


    }

 

   
}
