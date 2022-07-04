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

    
    [SerializeField] private TreeVariantOneNode treeVariantOneNode;

    [SerializeField] private int toolEquipped;

    [SerializeField] private OreVariantOneNode oreVariantOneNode;
    [SerializeField] private Infrastructure infrastructure;
    [SerializeField] private HerbVariantOneNode herbVariantOneNode;
    private void Awake()
    {
        instance = this;
    }

    public string recievedID;
    public void Next()
    {
        StorylineManager.onWorldEventEndedEvent.Invoke("O-" + currentIndex, 0,0);
        Debug.Log(currentIndex + " NEXT " + (currentIndex + 1).ToString());
    }
    private void Start()
    {
        StartTeachingZero();
        CharacterDialogueUI.onSetIsAdvancedonWorldEventEndedEvent.Invoke(true);
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
        CharacterDialogueUI.onSetIsAdvancedonWorldEventEndedEvent.Invoke(false);
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
        
        StorylineManager.onWorldEventEndedEvent.AddListener(StoryTwo);
    }
    public void StoryTwo(string p_id, int p_test, int p_teste)
    {
        Debug.Log("TRY - ID " + p_id + " O-" + currentIndex);
        if (p_id == "O-1")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryTwo);
            ToolCaster.onToolUsedEvent.AddListener(TeachTwo);

        }
       


    }

    public void TeachTwo(float p_useless) //You’re getting the hang of it, son. But what if you overdo it? Try swinging your axe until you wipe out.
    {

        Debug.Log("tool USED" + "- ID " + "O-" + currentIndex );
        currentDialogueIndex++;
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("O-" + currentIndex, dialogues[currentDialogueIndex]);
        //StorylineManager.onWorldEventEndedEvent.Invoke("O-" + currentIndex, 0, 0);
        EndTeachingTwo();

    }



    public void EndTeachingTwo()
    {
        Debug.Log("- ID " + "O-" + currentIndex + " ENDED");
        currentIndex++;
        ToolCaster.onToolUsedEvent.RemoveListener(TeachTwo);
        ToolCaster.onToolUsedEvent.RemoveListener(TeachTwo);
        //tutorialUI.frame.SetActive(true);
        CharacterDialogueUI.onSetStartTransitionEnabledEvent.Invoke(false);
        CharacterDialogueUI.onSetIsAdvancedonWorldEventEndedEvent.Invoke(false);
      
        StartTeachingThree();


        Debug.Log(currentIndex + " ended " + (currentIndex).ToString());
        currentIndex++;
        
        //Stamina.onStaminaDepletedEvent.RemoveListener(TeachStamina);


    }
    #endregion


    #region 3

    public void StartTeachingThree()
    {
        Debug.Log("THREE" + " END END");
        StorylineManager.onWorldEventEndedEvent.AddListener(StoryThree);
        


    }
    public void StoryThree(string p_id, int p_test, int p_teste)
    {
        Debug.Log("TRY - ID " + p_id + " O-" + currentIndex);
        if (p_id == "O-2")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
            Stamina.onStaminaDepletedEvent.AddListener(TeachThree);

        }


    }
    public void TeachThree()
    {
        Debug.Log("FAINTED");
        EndTeachingThree();

    }
    public void EndTeachingThree()
    {
        Debug.Log(currentIndex + " ended " + (currentIndex).ToString());
        StorylineManager.onWorldEventEndedEvent.RemoveListener(StoryThree);
        Stamina.onStaminaDepletedEvent.RemoveListener(TeachThree);
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
    public void StoryFour
        (string p_id, int p_test, int p_teste)
    {
        Debug.Log("TRY - ID " + p_id + " O-  uni uni" + currentIndex);
        if (p_id == "O-3")
        {
            Debug.Log("INSIDE - ID " + "O-" + currentIndex);
        }


    }
    public void TeachTwo()
    {

        EndTeachingTwo();

    }
    public void EndTeachingFour()
    {
     


    }
    #endregion
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
