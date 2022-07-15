using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TutorialEventEndedEvent : UnityEvent<int> { };
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public int currentIndex = 0;
    public int currentDialogueIndex = 0;
    public List<SO_Dialogues> dialogues;

    public TutorialUI tutorialUI;
    [SerializeField] private Stamina stamina;

    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;


    public ResourceNode resourceNode;

    public Infrastructure infrastructure;


    public SO_Dialogues equippingWrongTool;
    public SO_Dialogues swingingWrongTool;
    public SO_Dialogues swingingInAir;
    public SO_Dialogues upgradingWrong;
    public SO_Dialogues needToUpgradeAllTools;
    public SO_Dialogues cantGoThere;

    public EdgeCollider2D edgeCollider;
    public Passageway MidToPandayRoomPassageway;
    public Passageway MidToForkRoomPassageway;
    public Passageway PandayToMidPassageway;
    public Passageway ForkToMidPassageway;


    public static TutorialEventEndedEvent onTutorialEventEndedEvent = new TutorialEventEndedEvent();

    public Panday panday;
    public bool firstTime = true;
    private void Awake()
    {
        instance = this;
        //onTutorialEventEndedEvent.AddListener(TutorialEventEndedEvent);
    }

    public void Next()
    {

        StorylineManager.onWorldEventEndedEvent.Invoke("O-" + currentIndex, 0, 0);
        Debug.Log(currentIndex + " NEXT " + (currentIndex + 1).ToString());
    }

    //void TutorialEventEndedEvent(int i)
    //{

    //}
    private void Start()
    {
        Setup();

    }

    void Setup()
    {
        StorylineManager.onWorldEventEndedEvent.AddListener(TellStory);
        UIManager.onGameplayModeChangedEvent.AddListener(GameplayModeChangedEvent);
        infrastructure.InitializeValues();
        infrastructure.gameObject.SetActive(true);
        CharacterDialogueUI.onSetEndTransitionEnabledEvent.Invoke(false);
        CharacterDialogueUI.onSetIsCloseOnEndEvent.Invoke(false);
        TimeManager.instance.tutorialOn = true;
        TimeManager.onPauseGameTime.Invoke(true);
        Stamina.onManualSetStaminaEvent.Invoke(200);

        //
        StartLecture();
    }
    void Unsetup()
    {
        Debug.Log("UNSETTING UP");
        StorylineManager.onWorldEventEndedEvent.RemoveListener(TellStory);
        UIManager.onGameplayModeChangedEvent.RemoveListener(GameplayModeChangedEvent);
        CharacterDialogueUI.onSetEndTransitionEnabledEvent.Invoke(true);
        CharacterDialogueUI.onSetIsCloseOnEndEvent.Invoke(true);
        CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(true);
        TimeManager.instance.tutorialOn = false;
        TimeManager.onPauseGameTime.Invoke(true);
        tutorialUI.overheadUI.SetActive(false);
        panday.isQuestMode = false;


    }

    void StartLecture()
    {
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
        else if (p_id == "RETURNTOCURRENTTUTORIAL")
        {
            tutorialUI.overheadUI.SetActive(true);
        }
        else if (p_id == "O-0")
        {
            Debug.Log("im intisde 0");
            EndStory();


        }
        else if (p_id == "O-1")
        {
            Debug.Log("im intisde 1");
            //SPAWN
            resourceNode = TreeVariantOneNodePool.pool.Get();
            resourceNode.transform.position = spawnPoint1.position + new Vector3(0f, -2.35f, 0f);
            resourceNode.InitializeValues();
            resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachAppropriateToolForNode);

            //SPECIFIC
            ToolsUI.onToolQuestSwitchEvent.Invoke(3);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[3]);
            //SPECFICI
            EndStory();


        }
        else if (p_id == "O-2")
        {
            Debug.Log("im intisde 2");
            //SPAWN
            resourceNode = OreVariantOneNodePool.pool.Get();
            resourceNode.transform.position = spawnPoint1.position + new Vector3(0f, -2.35f, 0f);
            resourceNode.InitializeValues();
            resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachAppropriateToolForNode);
        
            //SPECIFIC
            ToolsUI.onToolQuestSwitchEvent.Invoke(1);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[1]);
            //SPECFICI
            EndStory();

        }
        else if (p_id == "O-3") // change hint icon
        {
            //SPAWN
            infrastructure.GetComponent<Health>().OnDeathEvent.AddListener(TeachSix);

            //SPECIFIC
            ToolsUI.onToolQuestSwitchEvent.Invoke(0);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[0]);
            //SPECFICI
            EndStory();
        }
        else if (p_id == "O-4")
        {
            //SPAWN
            resourceNode = HerbVariantOneNodePool.pool.Get();
            resourceNode.transform.position = spawnPoint1.position + new Vector3(0f, -2.35f, 0f);
            resourceNode.InitializeValues();
            resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachAppropriateToolForNode);

            //SPECIFIC
            ToolsUI.onToolQuestSwitchEvent.Invoke(2);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[2]);
            //SPECFICI
            EndStory();
        }
        else if (p_id == "O-5")
        {

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


            //SPECIFIC
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            //SPECFICI

            ToolManager.onProficiencyLevelModifiedEvent.AddListener(RequireMacheteProfLevel1);
            EndStory();
        }
        else if (p_id == "O-6")
        {
            InventoryManager.AddItem("Recipe 1", 1);
            InventoryManager.AddItem("Wood 1", 10);

            ToolManager.onToolUpgradedEvent.AddListener(RequireMacheteCraftLevel1);
            EndStory();



        }
        else if (p_id == "O-7")
        {
            InventoryManager.AddItem("Recipe 1", 3); //it happens 3 times
            InventoryManager.AddItem("Wood 1", 30);

            ToolManager.onToolUpgradedEvent.AddListener(RequireAllToolsCraftLevel1);
            EndStory();


        }
        else if (p_id == "O-8")
        {
            Debug.Log("IM INSIDE");
            panday.isQuestMode = true;

            //ResourceNode newResourceNode = TreeVariantOneNodePool.pool.Get();
            //newResourceNode.transform.position = spawnPoint1.position + new Vector3(0f, -2.35f, 0f);
            //newResourceNode.InitializeValues();

            ResourceNode newResourceNode = TreeVariantOneNodePool.pool.Get();
            newResourceNode.transform.position = spawnPoint2.position + new Vector3(0f, -2.35f, 0f);
            newResourceNode.InitializeValues();

            //newResourceNode = TreeVariantOneNodePool.pool.Get();
            //newResourceNode.transform.position = spawnPoint3.position + new Vector3(0f, -2.35f, 0f);
            //newResourceNode.InitializeValues();

            //newResourceNode = NipaLeavesVariantOneNodePool.pool.Get();
            //newResourceNode.transform.position = spawnPoint7.position + new Vector3(0f, -2.35f, 0f);
            //newResourceNode.InitializeValues();

            //newResourceNode = NipaLeavesVariantOneNodePool.pool.Get();
            //newResourceNode.transform.position = spawnPoint8.position + new Vector3(0f, -2.35f, 0f);
            //newResourceNode.InitializeValues();

            //newResourceNode = NipaLeavesVariantOneNodePool.pool.Get();
            //newResourceNode.transform.position = spawnPoint9.position + new Vector3(0f, -2.35f, 0f);
            //newResourceNode.InitializeValues();
            StorylineManager.onWorldEventEndedEvent.AddListener(RequirePandayQuestComplete);

           
            EndStory();

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
                    if (currentIndex == 9)
                    {
                        Debug.Log("QUEST COMPLETED");
                        Debug.Log("END");
                        Unsetup();
                    }
                    //if(currentIndex == 9)
                    //{
                    //    Debug.Log("END");
                    //    Unsetup();
                    //}

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
        Debug.Log("END STORY ID: O-" + currentIndex + " CURRENT INDEX: " + currentIndex + " CURRENT DIALOGUE: " + currentDialogueIndex);
        currentDialogueIndex++;
        tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;
        if (currentIndex == 0)
        {
            CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(false);
            //SPECIFIC
            ToolsUI.onToolQuestSwitchEvent.Invoke(3);
            ToolCaster.onSetIsPreciseEvent.Invoke(false);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[3]);
            //SPECFICI
            ToolCaster.onToolUsedEvent.AddListener(TeachUseTool);
        }
        //else if (currentIndex == 7)
        //{
        //    tutorialUI.overheadUI.SetActive(false);
        //}


    }
    void EndLecture()
    {
       
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


    void GameplayModeChangedEvent(bool p_set)
    {
        tutorialUI.overheadUI.SetActive(!p_set);
       if (currentIndex == 7 || currentIndex == 8)
       {
           tutorialUI.overheadUI.SetActive(false);
       }

    }

 
    public void TeachUseTool(float p_useless) //You�re getting the hang of it, son. But what if you overdo it? Try swinging your axe until you wipe out.
    {

        ToolCaster.onToolUsedEvent.RemoveListener(TeachUseTool);
        //SPECIFIC RESET
        ToolsUI.onToolQuestSwitchEvent.Invoke(-1);
        ToolCaster.onSetIsPreciseEvent.Invoke(false);
        ToolCaster.onSetRequireCorrectToolEvent.Invoke(null);
        //SPECFICI RESET
        EndLecture();
    }
    public void TeachAppropriateToolForNode() 
    {
        resourceNode.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachAppropriateToolForNode);
        resourceNode = null;

        //SPECIFIC
        ToolsUI.onToolQuestSwitchEvent.Invoke(-1);
        ToolCaster.onSetIsPreciseEvent.Invoke(false);
        ToolCaster.onSetRequireCorrectToolEvent.Invoke(null);
        //SPECFICI

        Debug.Log("AAAAAAAAAAAAAAAAAAA: " + currentIndex);
        EndLecture();
    }
  

    public void TeachSix()
    {
        infrastructure.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachSix);

        //SPECIFIC
        ToolsUI.onToolQuestSwitchEvent.Invoke(-1);
        ToolCaster.onSetIsPreciseEvent.Invoke(false);
        ToolCaster.onSetRequireCorrectToolEvent.Invoke(null);
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
            ToolCaster.onSetIsPreciseEvent.Invoke(false);
            //SPECIFIC
            EndLecture();
        }



    }
  
    public void RequireMacheteCraftLevel1()
    {
        bool passed = true;
        Debug.Log("STARTED " + ToolManager.instance.tools[2].craftLevel);
        for (int i = 0; i < ToolManager.instance.tools.Count;)
        {
            Tool selected_Tool = ToolManager.instance.tools[2];
            if (ToolManager.instance.tools[2].craftLevel < 1)
            {
                Debug.Log("FAILED");
                passed = false;
                break;
            }
            i++;

        }
        if (passed)
        {
            ToolManager.onToolUpgradedEvent.RemoveListener(RequireMacheteCraftLevel1);
            EndLecture();
        }



    }

    public void RequireAllToolsCraftLevel1()
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
            
            ToolManager.onToolUpgradedEvent.RemoveListener(RequireAllToolsCraftLevel1);
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
                    Debug.Log("QUEST COMPLETED");
                    EndLecture();
               // }
            }

        }


    }

 

   
}
