using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public int currentIndex = 0;
    public int currentDialogueIndex = 0;
    public List<SO_Dialogues> dialogues;

    public TutorialUI tutorialUI;
    [SerializeField] private Stamina stamina;

    public Transform spawnPoint;

    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform spawnPoint5;
    public Transform spawnPoint6;
    public Transform spawnPoint7;
    public Transform spawnPoint8;
    public Transform spawnPoint9;
    public ResourceNode resourceNode;

    public Infrastructure infrastructure;


    public SO_Dialogues equippingWrongTool;
    public SO_Dialogues swingingWrongTool;
    public SO_Dialogues swingingInAir;

    public Panday panday;
    private void Awake()
    {
        instance = this;
    }

    public void Next()
    {

        StorylineManager.onWorldEventEndedEvent.Invoke("O-" + currentIndex, 0, 0);
        Debug.Log(currentIndex + " NEXT " + (currentIndex + 1).ToString());
    }
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
       
        //Stamina.onManualSetStaminaEvent.Invoke(20);

        //
        StartLecture();
    }
    void Unsetup()
    {
        StorylineManager.onWorldEventEndedEvent.RemoveListener(TellStory);
        UIManager.onGameplayModeChangedEvent.RemoveListener(GameplayModeChangedEvent);
        CharacterDialogueUI.onSetEndTransitionEnabledEvent.Invoke(true);
        CharacterDialogueUI.onSetIsCloseOnEndEvent.Invoke(true);
        CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(true);
        panday.isQuestMode = false;


    }

    void StartLecture()
    {
        Debug.Log("START LECTURE ID: O-" + currentIndex + " CURRENT INDEX: " + currentIndex + " CURRENT DIALOGUE: " + currentDialogueIndex);
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);
    }
    void TellStory(string p_id, int p_intone, int p_intto)
    {
        tutorialUI.overheadUI.SetActive(false);
        Debug.Log("TELL STORY ID: " + p_id + " CURRENT INDEX: " + currentIndex + " CURRENT DIALOGUE: " + currentDialogueIndex);
        if (p_id == "O-2")
        {
            Debug.Log("I FOUND IT FINALLY HUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
        }
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
            resourceNode.transform.position = spawnPoint.position + new Vector3(0f, -2.35f, 0f);
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
            resourceNode.transform.position = spawnPoint.position + new Vector3(0f, -2.35f, 0f);
            resourceNode.InitializeValues();
            resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachAppropriateToolForNode);
        
            //SPECIFIC
            ToolsUI.onToolQuestSwitchEvent.Invoke(1);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[1]);
            //SPECFICI
            EndStory();

        }
        else if (p_id == "O-3")
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
            resourceNode.transform.position = spawnPoint.position + new Vector3(0f, -2.35f, 0f);
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

            newResourceNode = TreeVariantOneNodePool.pool.Get();
            newResourceNode.transform.position = spawnPoint2.position + new Vector3(0f, -2.35f, 0f);
            newResourceNode.InitializeValues();

            newResourceNode = TreeVariantOneNodePool.pool.Get();
            newResourceNode.transform.position = spawnPoint3.position + new Vector3(0f, -2.35f, 0f);
            newResourceNode.InitializeValues();

            newResourceNode = OreVariantOneNodePool.pool.Get();
            newResourceNode.transform.position = spawnPoint4.position + new Vector3(0f, -2.35f, 0f);
            newResourceNode.InitializeValues();

            newResourceNode = OreVariantOneNodePool.pool.Get();
            newResourceNode.transform.position = spawnPoint5.position + new Vector3(0f, -2.35f, 0f);
            newResourceNode.InitializeValues();

            newResourceNode = OreVariantOneNodePool.pool.Get();
            newResourceNode.transform.position = spawnPoint6.position + new Vector3(0f, -2.35f, 0f);
            newResourceNode.InitializeValues();

            newResourceNode = HerbVariantOneNodePool.pool.Get();
            newResourceNode.transform.position = spawnPoint7.position + new Vector3(0f, -2.35f, 0f);
            newResourceNode.InitializeValues();

            newResourceNode = HerbVariantOneNodePool.pool.Get();
            newResourceNode.transform.position = spawnPoint8.position + new Vector3(0f, -2.35f, 0f);
            newResourceNode.InitializeValues();

            newResourceNode = HerbVariantOneNodePool.pool.Get();
            newResourceNode.transform.position = spawnPoint9.position + new Vector3(0f, -2.35f, 0f);
            newResourceNode.InitializeValues();

            //SPECIFIC
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            //SPECFICI

            ToolManager.onProficiencyLevelModifiedEvent.AddListener(RequireLevel1Prof);

        }
        else if (p_id == "O-6")
        {
            InventoryManager.AddItem("Recipe 1", 1);
            InventoryManager.AddItem("Wood 1", 10);

            TeachNine();
            EndStory();

        }
        else if (p_id == "O-9")
        {

            //SPAWN




        }
        else if (p_id == "O-10")
        {

            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;
            TeachTen();
            EndStory();
        }
        else if (p_id == "O-11")
        {


            TeachEleven();

            //oreVariantOneNode = resourceNode;
    

            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;
            EndStory();

            //3
        }
       
        //else if (p_id == "O-4")
        //{
        //    //Stamina.onStaminaDepletedEvent.AddListener(TeachThree);

        //}
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
        else if (currentIndex == 1)
        {
            
        }


    }
    void EndLecture()
    {
        Debug.Log("END LECTURE ID: O-" + currentIndex + " CURRENT INDEX: " + currentIndex + " CURRENT DIALOGUE: " + currentDialogueIndex);
        currentIndex++;
        currentDialogueIndex++;
        StartLecture();

    }


    void GameplayModeChangedEvent(bool p_set)
    {
        tutorialUI.overheadUI.SetActive(!p_set);
    }





    #region 2
 
    public void TeachUseTool(float p_useless) //You’re getting the hang of it, son. But what if you overdo it? Try swinging your axe until you wipe out.
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
   

    #endregion

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

    public void RequireLevel1Prof(int p_level)
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

  

    #region 9

  
   
    public void TeachNine()
    {


        ToolManager.onToolUpgradedEvent.AddListener(RequireMacheteLevelCraft);
        // EndTeachingNine();

    }
    public void RequireMacheteLevelCraft()
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
            ToolManager.onToolUpgradedEvent.RemoveListener(RequireMacheteLevelCraft);
            EndLecture();
        }



    }

    #endregion
    #region 10

    public void StartTeachingTen()
    {
        tutorialUI.overheadUI.SetActive(false);
        Debug.Log("TEEEEEEEEEEEEEEST");
        InventoryManager.AddItem("Recipe 1", 3);
        InventoryManager.AddItem("Wood 1", 30);
        StorylineManager.onWorldEventEndedEvent.AddListener(StoryTen);
        currentDialogueIndex++;
        currentIndex++;
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);


    }
    public void StoryTen(string p_id, int p_test, int p_teste)
    {
        Debug.Log("TRY - ID " + p_id + " O-  uni uni" + currentIndex);
        if (p_id == "O-9")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN
            StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryTen);


            currentDialogueIndex++;
            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;
            TeachTen();
        }


    }
    public void TeachTen()
    {


        ToolManager.onToolUpgradedEvent.AddListener(RequireLevelCraft);


    }
    public void RequireLevelCraft()
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
            EndTeachingTen();
            ToolManager.onToolUpgradedEvent.RemoveListener(RequireLevelCraft);
        }



    }

    public void EndTeachingTen()
    {
        Debug.Log("TEN ENDED");

        StartTeachingEleven();
    }
    #endregion
    #region 11

    public void StartTeachingEleven()
    {
        tutorialUI.overheadUI.SetActive(false);
        StorylineManager.onWorldEventEndedEvent.AddListener(StoryEleven);
        currentDialogueIndex++;
        currentIndex++;
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);


    }
    public void StoryEleven(string p_id, int p_test, int p_teste)
    {
        Debug.Log("TRY - ID " + p_id + " O-  uni uni" + currentIndex);
        if (p_id == "O-10")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN
            StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryEleven);
            TeachEleven();

            currentDialogueIndex++;
            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;


            //3
        }


    }
    public void TeachEleven()
    {
        panday.isQuestMode = true;

        ResourceNode newResourceNode = TreeVariantOneNodePool.pool.Get();
        newResourceNode.transform.position = spawnPoint1.position + new Vector3(0f, -2.35f, 0f);
        newResourceNode.InitializeValues();

        newResourceNode = TreeVariantOneNodePool.pool.Get();
        newResourceNode.transform.position = spawnPoint2.position + new Vector3(0f, -2.35f, 0f);
        newResourceNode.InitializeValues();

        newResourceNode = TreeVariantOneNodePool.pool.Get();
        newResourceNode.transform.position = spawnPoint3.position + new Vector3(0f, -2.35f, 0f);
        newResourceNode.InitializeValues();

        newResourceNode = OreVariantOneNodePool.pool.Get();
        newResourceNode.transform.position = spawnPoint4.position + new Vector3(0f, -2.35f, 0f);
        newResourceNode.InitializeValues();

        newResourceNode = OreVariantOneNodePool.pool.Get();
        newResourceNode.transform.position = spawnPoint5.position + new Vector3(0f, -2.35f, 0f);
        newResourceNode.InitializeValues();

        newResourceNode = OreVariantOneNodePool.pool.Get();
        newResourceNode.transform.position = spawnPoint6.position + new Vector3(0f, -2.35f, 0f);
        newResourceNode.InitializeValues();

        newResourceNode = HerbVariantOneNodePool.pool.Get();
        newResourceNode.transform.position = spawnPoint7.position + new Vector3(0f, -2.35f, 0f);
        newResourceNode.InitializeValues();

        newResourceNode = HerbVariantOneNodePool.pool.Get();
        newResourceNode.transform.position = spawnPoint8.position + new Vector3(0f, -2.35f, 0f);
        newResourceNode.InitializeValues();

        newResourceNode = HerbVariantOneNodePool.pool.Get();
        newResourceNode.transform.position = spawnPoint9.position + new Vector3(0f, -2.35f, 0f);
        newResourceNode.InitializeValues();
        StorylineManager.onWorldEventEndedEvent.AddListener(PandayQuestComplete);


    }

    public void PandayQuestComplete(string p_id, int p_test, int p_testt)
    {
        Debug.Log(p_id + " - " + p_test + " - " + p_testt);
        if (p_id == "Q-P")
        {
            if (p_test == 0)
            {
                if (p_testt == 1)
                {
                    Debug.Log("QUEST COMPLETED");
                    EndTeachingEleven();
                }
            }

        }


    }
    public void EndTeachingEleven()
    {


        Unsetup();
    }
    #endregion

   
}
