using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public int currentIndex = 0;
    public int currentDialogueIndex = 0;
    public List<SO_Dialogues> dialogues;

    public TutorialUI tutorialUI;
    [SerializeField] private Stamina stamina;

    public Transform spawnPoint;
    public ResourceNode resourceNode;
    public Infrastructure infrastructure;
    

    [SerializeField] private int toolEquipped;

 
    private void Awake()
    {
        instance = this;
    }

    //ToolManager.onToolChangedEvent.Invoke(selected_Tool);
   
    public void Next()
    {
        
        StorylineManager.onWorldEventEndedEvent.Invoke("O-" + currentIndex, 0,0);
        Debug.Log(currentIndex + " NEXT " + (currentIndex + 1).ToString());
    }
    private void Start()
    {
       
        StartTeachingZero();
        
        //CharacterDialogueUI.onSetIsAdvancedonWorldEventEndedEvent.Invoke(true);
        //CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(false);
        CharacterDialogueUI.onSetEndTransitionEnabledEvent.Invoke(false);
        CharacterDialogueUI.onSetIsCloseOnEndEvent.Invoke(false);
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-"+currentIndex,dialogues[currentDialogueIndex]);
    }
    #region 0

    public void StartTeachingZero() //Storm has us stuck for a while, eh? Time to train then, Sakoro.
    {
        //Debug.Log("start teaching");
        StorylineManager.onWorldEventEndedEvent.AddListener(StoryZero);

    }
    public void StoryZero(string p_id,int p_test,int p_teste)
    {
        if (p_id == "O-0")
        {
            //Debug.Log("START STORY");
            EndTeachingZero();
        }
       

    }

    public void TeachZero()
    {
        


    }
    public void EndTeachingZero() //Mind your Stamina. Using your held tool decreases it. Go on, use your tool.
    {
        currentIndex++;
        StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryZero);
        tutorialUI.frame.SetActive(true);
        CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(false);

        //CharacterDialogueUI.onSetIsAdvancedonWorldEventEndedEvent.Invoke(false);
        //CharacterDialogueUI.onSetButtonEnabledEvent.Invoke(false);
        StartTeachingOne();
    }
    #endregion
    #region 1

    public void StartTeachingOne()
    {
        Debug.Log("TWO");
        
        tutorialUI.frame.SetActive(false);
        tutorialUI.overheadUI.SetActive(true);

        StoryOne("0",0,0);
    }

    
    public void StoryOne(string p_id, int p_test, int p_teste)
    {

        currentDialogueIndex++;
        tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;


        EndTeachingOne();

    }

    //public void TeachOne()
    //{
    //}
    public void EndTeachingOne() //Mind your Stamina. Using your held tool decreases it. Go on, use your tool.
    {
        Debug.Log(currentIndex + " ended " + (currentIndex).ToString());
        currentIndex++;
        
        StartTeachingTwo();
    }
    #endregion

    #region 2
    public void StartTeachingTwo()
    {
        Stamina.onManualSetStaminaEvent.Invoke(20);
        ToolCaster.onToolUsedEvent.AddListener(TeachTwo);

    }
    public void StoryTwo(string p_id, int p_test, int p_teste)
    {
        Debug.Log("oooooooooooooooooooooooTRY - ID " + p_id + " O-" + currentIndex);
        if (p_id == "O-1")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            tutorialUI.overheadUI.SetActive(false);
            currentDialogueIndex++;
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);
            
            EndTeachingTwo();

        }
       


    }

    public void TeachTwo(float p_useless) //You’re getting the hang of it, son. But what if you overdo it? Try swinging your axe until you wipe out.
    {
        ToolCaster.onToolUsedEvent.RemoveListener(TeachTwo);
        Debug.Log("ttttttttttttttttttool USED" + "- ID " + "O-" + currentIndex);
        StoryTwo("O-1", 0, 0);
        //EndTeachingTwo();


    }



    public void EndTeachingTwo()
    {
        Debug.Log("- ID " + "O-" + currentIndex + " ENDED");

        currentIndex++;
        //CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(false);
        //CharacterDialogueUI.onSetIsAdvancedonWorldEventEndedEvent.Invoke(false);
        Debug.Log(currentIndex + " ended " + (currentIndex).ToString());
        
        StorylineManager.onWorldEventEndedEvent.AddListener(StoryTwoB);


        //currentIndex++;

        //Stamina.onStaminaDepletedEvent.RemoveListener(TeachStamina);


    }
    #endregion

    #region 2b
    public void StartTeachingTwoB()
    {
        //Debug.Log("1b1b1b1b1b1b1b1b1b1b1b - ID " + currentIndex);
        //StoryTwoB("O-1b", 0, 0);

    }

    public void StoryTwoB(string p_id, int p_test, int p_teste) //Use the Axe using the Tool Action button on the lower right, while minding the Stamina Bar on the upper left.
    {
        Debug.Log("qqqqqqqqqqqqqqqqqqqqqqqqqqqq - ID " + p_id + " O-" + currentIndex);
        if (p_id == "O-2")
        {
            Debug.Log("TTTTTTTTTTTTT - ID " + p_id + " O-" + currentIndex);
            tutorialUI.overheadUI.SetActive(true);
            currentDialogueIndex++;
            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;
            EndTeachingTwoB();
            //ENDDDDDDDDDDDDDDDDDDDDDDD

        }
    }

    //public void TeachTwoB(float p_useless)
    //{

      

    //}



    public void EndTeachingTwoB()
    {
        Debug.Log("- ID " + "O-" + currentIndex + " ENDED");
        StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryTwoB);

        currentIndex++;
        StartTeachingThree();


    }
    #endregion

    #region 3

    public void StartTeachingThree()
    {
        Debug.Log("THREE" + " START START");
        
        StoryThree("O-3",0,0);


    }
    public void StoryThree(string p_id, int p_test, int p_teste)
    {
        Debug.Log("TRY - ID " + p_id + " O-" + currentIndex);
        if (p_id == "O-3")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            Stamina.onStaminaDepletedEvent.AddListener(TeachThree);

        }


    }
    public void TeachThree()
    {
        Stamina.onStaminaDepletedEvent.RemoveListener(TeachThree);
        Debug.Log("FAINTED");
        EndTeachingThree();
        tutorialUI.overheadUI.SetActive(false);

    }
    public void EndTeachingThree()
    {
        Debug.Log(currentIndex + " ended " + (currentIndex).ToString());
        StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryThree);
       
        StartTeachingFour();




    }
    #endregion

    #region 4

    public void StartTeachingFour()
    {
        currentIndex++;
        StorylineManager.onWorldEventEndedEvent.AddListener(StoryFour);
        currentDialogueIndex++;
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
            tutorialUI.overheadUI.SetActive(true);
            currentDialogueIndex++;
            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;

            //3
        }


    }
    public void TeachFour()
    {
        StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryFour);
        resourceNode.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachFour);
        EndTeachingFour();

    }
    public void EndTeachingFour()
    {
        Debug.Log("FOUR ENDED");
        currentIndex++;
        //CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(false);
        //CharacterDialogueUI.onSetIsAdvancedonWorldEventEndedEvent.Invoke(false);
        Debug.Log(currentIndex + " ended " + (currentIndex).ToString());

        StartTeachingFive();

    }
    #endregion


    #region 5

    public void StartTeachingFive()
    {
        tutorialUI.overheadUI.SetActive(false);
        StorylineManager.onWorldEventEndedEvent.AddListener(StoryFive);
        currentDialogueIndex++;
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


            //3
        }


    }
    public void TeachFive()
    {
        resourceNode.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachFive);
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
        
            infrastructure.transform.position = spawnPoint.position + new Vector3(0f, -2.35f, 0f);
            infrastructure.InitializeValues();
            infrastructure.GetComponent<Health>().OnDeathEvent.AddListener(TeachSix);
            infrastructure.gameObject.SetActive(true);
            //oreVariantOneNode = resourceNode;
            tutorialUI.overheadUI.SetActive(true);
            currentDialogueIndex++;
            tutorialUI.overheadText.text = dialogues[currentDialogueIndex].dialogues[0].words;


            //3
        }


    }
    public void TeachSix()
    {
        resourceNode.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachSix);
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


            //3
        }


    }
    public void TeachSeven()
    {
        resourceNode.GetComponent<Health>().OnDeathEvent.RemoveListener(TeachSeven);
        EndTeachingSeven();

    }
    public void EndTeachingSeven()
    {
        Debug.Log("FIVE ENDED");
        tutorialUI.overheadUI.SetActive(false); //for now
        Final();
    }
    #endregion


    public void Final()
    {
        CharacterDialogueUI.onSetEndTransitionEnabledEvent.Invoke(true);
        CharacterDialogueUI.onSetIsCloseOnEndEvent.Invoke(true);
        CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(true);


    }


























    //#region 2

    //public void StartTeachingTwo()
    //{
    //    Debug.Log("THREE");
    //    StorylineManager.onWorldEventEndedEvent.AddListener(StoryStaminaA);
    //    CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentIndex]);
    //    Stamina.onStaminaDepletedEvent.AddListener(TeachTwo);


    //}
    //public void StoryTwo(string p_id, int p_test, int p_teste)
    //{
    //    if (p_id == "O-1")
    //    {
    //        //Debug.Log("START");
    //        Stamina.onStaminaDepletedEvent.AddListener(TeachTwo);
    //    }


    //}
    //public void TeachTwo()
    //{

    //    EndTeachingTwo();

    //}
    //public void EndTeachingTwo()
    //{
    //    Debug.Log(currentIndex + " ended " + (currentIndex + 1).ToString());
    //    currentIndex++;
    //    Stamina.onStaminaDepletedEvent.RemoveListener(TeachTwo);


    //}
    //#endregion


}
