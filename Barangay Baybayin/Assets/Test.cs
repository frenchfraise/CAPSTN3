using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Test : MonoBehaviour
{
    public static Test instance;
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
    //[SerializeField] private int toolEquipped;



    private void Awake()
    {
        instance = this;
    }

    //ToolManager.onToolChangedEvent.Invoke(selected_Tool);

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
        StorylineManager.onWorldEventEndedEvent.AddListener(WrongAction);
        StorylineManager.onWorldEventEndedEvent.AddListener(TellStory);
        UIManager.onGameplayModeChangedEvent.AddListener(GameplayModeChangedEvent);
        infrastructure.InitializeValues();
        infrastructure.gameObject.SetActive(true);
        CharacterDialogueUI.onSetEndTransitionEnabledEvent.Invoke(false);
        CharacterDialogueUI.onSetIsCloseOnEndEvent.Invoke(false);
        Stamina.onManualSetStaminaEvent.Invoke(20);

        //
        StartLecture();
    }
    void Unsetup()
    {
        StorylineManager.onWorldEventEndedEvent.RemoveListener(WrongAction);
        StorylineManager.onWorldEventEndedEvent.RemoveListener(TellStory);
        UIManager.onGameplayModeChangedEvent.RemoveListener(GameplayModeChangedEvent);
        CharacterDialogueUI.onSetEndTransitionEnabledEvent.Invoke(true);
        CharacterDialogueUI.onSetIsCloseOnEndEvent.Invoke(true);
        CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(true);
 


    }

    void StartLecture()
    {
        Debug.Log("START STORY ID: O-" + currentIndex + " CURRENT INDEX: " + currentIndex + " CURRENT DIALOGUE: " + currentDialogueIndex);
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);
    }
    void TellStory(string p_id, int p_intone, int p_intto)
    {
        tutorialUI.overheadUI.SetActive(false);
        Debug.Log("TELL STORY ID: " + p_id + " CURRENT INDEX: " + currentIndex + " CURRENT DIALOGUE: " + currentDialogueIndex);
        if (p_id == "O-0")
        {

          


        }
        else if (p_id == "O-1")
        {
            //SPECIFIC RESET
            StorylineManager.onWorldEventEndedEvent.RemoveListener(WrongAction);
            ToolsUI.onToolQuestSwitchEvent.Invoke(-1);
            ToolCaster.onSetIsPreciseEvent.Invoke(false);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(null);
            //SPECFICI RESET
            tutorialUI.overheadUI.SetActive(true);


          
            EndTeachingTwoB();

        }
        else if (p_id == "O-2")
        {

            //SPAWN
            resourceNode = TreeVariantOneNodePool.pool.Get();
            resourceNode.transform.position = spawnPoint.position + new Vector3(0f, -2.35f, 0f);
            resourceNode.InitializeValues();
            resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachFour);
            //treeVariantOneNode = resourceNode;
            tutorialUI.overheadUI.SetActive(true);

            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;


            //SPECIFIC
            StorylineManager.onWorldEventEndedEvent.AddListener(WrongAction);
            ToolsUI.onToolQuestSwitchEvent.Invoke(3);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[3]);
            //SPECFICI


        }
        else if (p_id == "O-5")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN
            resourceNode = OreVariantOneNodePool.pool.Get();
            resourceNode.transform.position = spawnPoint.position + new Vector3(0f, -2.35f, 0f);
            resourceNode.InitializeValues();
            resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachFive);
            //oreVariantOneNode = resourceNode;
            tutorialUI.overheadUI.SetActive(true);

            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;

            //SPECIFIC
            StorylineManager.onWorldEventEndedEvent.AddListener(WrongAction);
            ToolsUI.onToolQuestSwitchEvent.Invoke(1);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[1]);
            //SPECFICI
            //3
        }
        else if (p_id == "O-6")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN

            //infrastructure.transform.position = spawnPoint.position + new Vector3(0f, -2.35f, 0f);
            //infrastructure.InitializeValues();
            infrastructure.GetComponent<Health>().OnDeathEvent.AddListener(TeachSix);
            //infrastructure.gameObject.SetActive(true);

            tutorialUI.overheadUI.SetActive(true);

            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;

            //SPECIFIC
            StorylineManager.onWorldEventEndedEvent.AddListener(WrongAction);
            ToolsUI.onToolQuestSwitchEvent.Invoke(0);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[0]);
            //SPECFICI
            //3
        }
        else if (p_id == "O-7")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN
            resourceNode = HerbVariantOneNodePool.pool.Get();
            resourceNode.transform.position = spawnPoint.position + new Vector3(0f, -2.35f, 0f);
            resourceNode.InitializeValues();
            resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachSeven);
            //oreVariantOneNode = resourceNode;
            tutorialUI.overheadUI.SetActive(true);
            currentDialogueIndex++;
            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;

            //SPECIFIC
            StorylineManager.onWorldEventEndedEvent.AddListener(WrongAction);
            ToolsUI.onToolQuestSwitchEvent.Invoke(2);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[2]);
            //SPECFICI
            //3
        }
        else if (p_id == "O-8")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN
            //SPECIFIC
            StorylineManager.onWorldEventEndedEvent.AddListener(WrongAction);
            //ToolsUI.onToolQuestSwitchEvent.Invoke(1);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            //ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[1]);
            //SPECFICI


            tutorialUI.overheadUI.SetActive(true);

            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;
            TeachEight();
        }
        else if (p_id == "O-9")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN


            tutorialUI.overheadUI.SetActive(true);

            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;
            TeachNine();
        }
        else if (p_id == "O-10")
        {



            //tutorialUI.overheadUI.SetActive(true);

            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;
            TeachTen();
        }
        else if (p_id == "O-11")
        {


            TeachEleven();

            //oreVariantOneNode = resourceNode;
            tutorialUI.overheadUI.SetActive(true);

            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;


            //3
        }
        //else if (p_id == "O-4")
        //{
        //    //Stamina.onStaminaDepletedEvent.AddListener(TeachThree);

        //}
    }
    void WrongAction(string p_id, int p_intone, int p_intto)
    {
        Debug.Log("try ::: ID " + p_id + " ::: INDEX ::: " + currentIndex + " :::CURRENT DIALOGUE INDEX::: " + currentDialogueIndex);
        tutorialUI.overheadUI.SetActive(false);
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


        

    }

    void EndStory()
    {
        Debug.Log("END STORY ID: O-" + currentIndex + " CURRENT INDEX: " + currentIndex + " CURRENT DIALOGUE: " + currentDialogueIndex);
        tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;
        if (currentIndex == 0)
        {
            CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(false);
        }
        

    }
    void EndLecture()
    {
        Debug.Log("END LECTURE ID: O-" + currentIndex + " CURRENT INDEX: " + currentIndex + " CURRENT DIALOGUE: " + currentDialogueIndex);
        currentIndex++;
        currentDialogueIndex++;
        
    }


    void GameplayModeChangedEvent(bool p_set)
    {
        tutorialUI.overheadUI.SetActive(!p_set);
    }

 
 


    #region 2
    public void StartTeachingTwo()
    {


        //SPECIFIC
        ToolsUI.onToolQuestSwitchEvent.Invoke(3);
        ToolCaster.onSetIsPreciseEvent.Invoke(false);
        ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[3]);
        //SPECFICI
        ToolCaster.onToolUsedEvent.AddListener(TeachTwo);

    }


    public void TeachTwo(float p_useless) //You’re getting the hang of it, son. But what if you overdo it? Try swinging your axe until you wipe out.
    {

        ToolCaster.onToolUsedEvent.RemoveListener(TeachTwo);
        StorylineManager.onWorldEventEndedEvent.Invoke("O-" + currentIndex, 0, 0);
    }


    #endregion

    #region 2b
    public void StartTeachingTwoB()
    {


    }

    public void EndTeachingTwoB()
    {

        //StartTeachingThree();
        StartTeachingFour();

    }
    #endregion

    //#region 3

    //public void StartTeachingThree()
    //{
    //    Debug.Log("THREE" + " START START");

    //    StoryThree("O-3",0,0);


    //}
    //public void StoryThree(string p_id, int p_test, int p_teste)
    //{
    //    if (p_id == "O-3")
    //    {
    //        Stamina.onStaminaDepletedEvent.AddListener(TeachThree);

    //    }


    //}
    //public void TeachThree()
    //{
    //    Stamina.onStaminaDepletedEvent.RemoveListener(TeachThree);
    //    EndTeachingThree();
    //    tutorialUI.overheadUI.SetActive(false);

    //}
    //public void EndTeachingThree()
    //{
    //    StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryThree);

    //    StartTeachingFour();




    //}
    //#endregion

    #region 4

    public void StartTeachingFour()
    {

        StorylineManager.onWorldEventEndedEvent.AddListener(StoryFour);

        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);


    }
    public void StoryFour(string p_id, int p_test, int p_teste)
    {
        Debug.Log("TRY - ID " + p_id + " O-  uni uni" + currentIndex);
        if (p_id == "O-5")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN
            resourceNode = TreeVariantOneNodePool.pool.Get();
            resourceNode.transform.position = spawnPoint.position + new Vector3(0f, -2.35f, 0f);
            resourceNode.InitializeValues();
            resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachFour);
            //treeVariantOneNode = resourceNode;


            //SPECIFIC
            StorylineManager.onWorldEventEndedEvent.AddListener(WrongAction);
            ToolsUI.onToolQuestSwitchEvent.Invoke(3);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[3]);
            //SPECFICI

            //3
        }


    }
    public void TeachFour()
    {
        //SPECIFIC
        StorylineManager.onWorldEventEndedEvent.RemoveListener(WrongAction);
        ToolsUI.onToolQuestSwitchEvent.Invoke(-1);
        ToolCaster.onSetIsPreciseEvent.Invoke(false);
        ToolCaster.onSetRequireCorrectToolEvent.Invoke(null);
        //SPECFICI
        StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryFour);
        resourceNode.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachFour);
        EndTeachingFour();

    }
    public void EndTeachingFour()
    {
        Debug.Log("FOUR ENDED");

        Debug.Log(currentIndex + " ended " + (currentIndex).ToString());

        StartTeachingFive();

    }
    #endregion


    #region 5

    public void StartTeachingFive()
    {
        tutorialUI.overheadUI.SetActive(false);
        StorylineManager.onWorldEventEndedEvent.AddListener(StoryFive);
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);


    }
    public void StoryFive(string p_id, int p_test, int p_teste)
    {
        Debug.Log("TRY - ID " + p_id + " O-  uni uni" + currentIndex);
        if (p_id == "O-6")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN
            StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryFive);
            resourceNode = OreVariantOneNodePool.pool.Get();
            resourceNode.transform.position = spawnPoint.position + new Vector3(0f, -2.35f, 0f);
            resourceNode.InitializeValues();
            resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachFive);
            //oreVariantOneNode = resourceNode;
            tutorialUI.overheadUI.SetActive(true);
            currentDialogueIndex++;
            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;

            //SPECIFIC
            StorylineManager.onWorldEventEndedEvent.AddListener(WrongAction);
            ToolsUI.onToolQuestSwitchEvent.Invoke(1);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[1]);
            //SPECFICI
            //3
        }


    }
    public void TeachFive()
    {
        resourceNode.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachFive);
        //SPECIFIC
        StorylineManager.onWorldEventEndedEvent.RemoveListener(WrongAction);
        ToolsUI.onToolQuestSwitchEvent.Invoke(-1);
        ToolCaster.onSetIsPreciseEvent.Invoke(false);
        ToolCaster.onSetRequireCorrectToolEvent.Invoke(null);
        //SPECFICI
        EndTeachingFive();

    }
    public void EndTeachingFive()
    {
        Debug.Log("FIVE ENDED");
        tutorialUI.overheadUI.SetActive(false); //for now
        StartTeachingSix();
    }
    #endregion

    #region 6

    public void StartTeachingSix()
    {
        tutorialUI.overheadUI.SetActive(false);
        StorylineManager.onWorldEventEndedEvent.AddListener(StorySix);
        currentDialogueIndex++;
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);


    }
    public void StorySix(string p_id, int p_test, int p_teste)
    {
        Debug.Log("TRY - ID " + p_id + " O-  uni uni" + currentIndex);
        if (p_id == "O-6")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN
            StorylineManager.onWorldEventEndedEvent.RemoveListener(StorySix);

            //infrastructure.transform.position = spawnPoint.position + new Vector3(0f, -2.35f, 0f);
            //infrastructure.InitializeValues();
            infrastructure.GetComponent<Health>().OnDeathEvent.AddListener(TeachSix);
            //infrastructure.gameObject.SetActive(true);

            tutorialUI.overheadUI.SetActive(true);
            currentDialogueIndex++;
            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;

            //SPECIFIC
            StorylineManager.onWorldEventEndedEvent.AddListener(WrongAction);
            ToolsUI.onToolQuestSwitchEvent.Invoke(0);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[0]);
            //SPECFICI
            //3
        }


    }
    public void TeachSix()
    {
        infrastructure.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachSix);
        //resourceNode.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachSix);
        //SPECIFIC
        StorylineManager.onWorldEventEndedEvent.RemoveListener(WrongAction);
        ToolsUI.onToolQuestSwitchEvent.Invoke(-1);
        ToolCaster.onSetIsPreciseEvent.Invoke(false);
        ToolCaster.onSetRequireCorrectToolEvent.Invoke(null);
        //SPECFICI
        EndTeachingSix();

    }
    public void EndTeachingSix()
    {
        Debug.Log("FIVE ENDED");
        tutorialUI.overheadUI.SetActive(false); //for now
        StartTeachingSeven();
    }
    #endregion

    #region 7

    public void StartTeachingSeven()
    {
        tutorialUI.overheadUI.SetActive(false);
        StorylineManager.onWorldEventEndedEvent.AddListener(StorySeven);
        currentDialogueIndex++;
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);


    }
    public void StorySeven(string p_id, int p_test, int p_teste)
    {
        Debug.Log("TRY - ID " + p_id + " O-  uni uni" + currentIndex);
        if (p_id == "O-6")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN
            StorylineManager.onWorldEventEndedEvent.RemoveListener(StorySeven);
            resourceNode = HerbVariantOneNodePool.pool.Get();
            resourceNode.transform.position = spawnPoint.position + new Vector3(0f, -2.35f, 0f);
            resourceNode.InitializeValues();
            resourceNode.GetComponent<Health>().OnDeathEvent.AddListener(TeachSeven);
            //oreVariantOneNode = resourceNode;
            tutorialUI.overheadUI.SetActive(true);
            currentDialogueIndex++;
            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;

            //SPECIFIC
            StorylineManager.onWorldEventEndedEvent.AddListener(WrongAction);
            ToolsUI.onToolQuestSwitchEvent.Invoke(2);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[2]);
            //SPECFICI
            //3
        }


    }
    public void TeachSeven()
    {
        resourceNode.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachSeven);
        //SPECIFIC
        StorylineManager.onWorldEventEndedEvent.RemoveListener(WrongAction);
        ToolsUI.onToolQuestSwitchEvent.Invoke(-1);
        ToolCaster.onSetIsPreciseEvent.Invoke(false);
        ToolCaster.onSetRequireCorrectToolEvent.Invoke(null);
        //SPECFICI
        EndTeachingSeven();

    }
    public void EndTeachingSeven()
    {
        Debug.Log("FIVE ENDED");
        tutorialUI.overheadUI.SetActive(false); //for now
        StartTeachingEight();
    }
    #endregion

    #region 8

    public void StartTeachingEight()
    {
        tutorialUI.overheadUI.SetActive(false);
        StorylineManager.onWorldEventEndedEvent.AddListener(StoryEight);
        currentDialogueIndex++;
        currentIndex++;
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);


    }
    public void StoryEight(string p_id, int p_test, int p_teste)
    {
        Debug.Log("TRY - ID " + p_id + " O-  uni uni" + currentIndex);
        if (p_id == "O-7")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN
            StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryEight);
            //SPECIFIC
            StorylineManager.onWorldEventEndedEvent.AddListener(WrongAction);
            //ToolsUI.onToolQuestSwitchEvent.Invoke(1);
            ToolCaster.onSetIsPreciseEvent.Invoke(true);
            //ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[1]);
            //SPECFICI


            tutorialUI.overheadUI.SetActive(true);
            currentDialogueIndex++;
            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;
            TeachEight();
        }


    }
    public void TeachEight()
    {
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

        ToolManager.onProficiencyLevelModifiedEvent.AddListener(RequireLevel1Prof);
        Debug.Log("SPAWN ENDED");

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
            EndTeachingEight();
        }



    }
    public void EndTeachingEight()
    {
        //SPECIFIC
        StorylineManager.onWorldEventEndedEvent.AddListener(WrongAction);
        //ToolsUI.onToolQuestSwitchEvent.Invoke(1);
        ToolCaster.onSetIsPreciseEvent.Invoke(false);
        //ToolCaster.onSetRequireCorrectToolEvent.Invoke(ToolManager.instance.tools[1]);
        //SPECFICI
        StartTeachingNine();
    }
    #endregion

    #region 9

    public void StartTeachingNine()
    {
        tutorialUI.overheadUI.SetActive(false);
        InventoryManager.AddItem("Recipe 1", 1);
        InventoryManager.AddItem("Wood 1", 10);
        StorylineManager.onWorldEventEndedEvent.AddListener(StoryNine);
        currentIndex++;
        currentDialogueIndex++;
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);


    }
    public void StoryNine(string p_id, int p_test, int p_teste)
    {
        Debug.Log("TRY - ID " + p_id + " O-  uni uni" + currentIndex);
        if (p_id == "O-8")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            //SPAWN
            StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryNine);


            tutorialUI.overheadUI.SetActive(true);
            currentDialogueIndex++;
            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;
            TeachNine();
        }


    }
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
            EndTeachingNine();
        }



    }

    public void EndTeachingNine()
    {
        Debug.Log("FIVE ENDED");
        ToolManager.onToolUpgradedEvent.RemoveListener(RequireMacheteLevelCraft);
        StartTeachingTen();
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


            //tutorialUI.overheadUI.SetActive(true);
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

            //oreVariantOneNode = resourceNode;
            tutorialUI.overheadUI.SetActive(true);
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


        Final();
    }
    #endregion

    public void Final()
    {
        CharacterDialogueUI.onSetEndTransitionEnabledEvent.Invoke(true);
        CharacterDialogueUI.onSetIsCloseOnEndEvent.Invoke(true);
        CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(true);


    }
}
