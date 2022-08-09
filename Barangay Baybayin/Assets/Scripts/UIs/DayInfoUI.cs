using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class DayInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject frame;

    public TMP_Text dayText; 

    public TMP_Text dayCountText;
    public TMP_Text daysRemainingText;
    public TMP_Text conditionsText;
    public Image trans;
    public Image fader;
    public Sprite defaultSR;
    public Animator anim;
    private bool fainted;
    private bool isFirstTime = true;

    private void Awake()
    {
        TimeManager.onDayEndedEvent.AddListener(DayEnd);
        DayEnd(false,0);
    }
    private void OnDestroy()
    {
        TimeManager.onDayEndedEvent.RemoveListener(DayEnd);
    }
 

    public void DayEnd(bool p_causedByFainting, int p_dayCount)
    {
        //PlayerManager.instance.canPressPanel = false;
        UIManager.instance.transitionUI.transitionUI.color = new Color32(0, 0, 0, 0);
        trans.sprite = defaultSR;
        conditionsText.color = new Color32(255, 255, 255, 0);
        dayCountText.color = new Color32(255, 255, 255, 0);
        daysRemainingText.color = new Color32(255, 255, 255, 0);
        if (!isFirstTime )
        {
            frame.SetActive(true);
            StartCoroutine(Co_DayEndTransition(p_causedByFainting, p_dayCount));
            UIManager.instance.characterDialogueUI.SetEndTransitionEnabledEvent(false);
            UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(false);
        }
        else if (isFirstTime)
        {
            isFirstTime = false;

            StartCoroutine(Co_fIRSTDayEndTransition());
            frame.SetActive(true);
        }

    }
    IEnumerator Co_fIRSTDayEndTransition()
    {
        TimeManager.onPauseGameTime.Invoke(false);
        dayText.text = "MAY 1";

        dayCountText.text = "MAY 1";
        conditionsText.text = "STARTS";
        daysRemainingText.text = "";
        anim.SetTrigger("trans");

       

        yield return new WaitForSeconds(3.5f);

        dayCountText.text = "";
        conditionsText.text = "";
        yield return new WaitForSeconds(3.15f);

        TimeManager.instance.NewDay();
        frame.SetActive(false);
        TimeManager.onPauseGameTime.Invoke(true);
     
        

    }
    IEnumerator Co_DayEndTransition(bool p_causedByFainting, int p_dayCount)
    {
        if (TimeManager.instance.daysRemaining > 0)
        {
            TimeManager.onPauseGameTime.Invoke(false);
            dayText.text = "MAY " + (p_dayCount + 1).ToString();
            dayCountText.text = "MAY " + (p_dayCount).ToString();
            if (!p_causedByFainting)
            {
                conditionsText.text = "ENDED";
            }
            else
            {
                conditionsText.text = "YOU FAINTED";
            }

            anim.SetTrigger("trans");

           

           

            yield return new WaitForSeconds(3.5f);

            TimeManager.onDayChangingEvent.Invoke();
            dayCountText.text = "MAY " + (p_dayCount + 1).ToString();
            conditionsText.text = "STARTS";
            if (TimeManager.instance.daysRemaining <= 15)
            {
                daysRemainingText.text = (TimeManager.instance.daysRemaining - 1).ToString() + " DAYS REMAINING";

            }
            else
            {
                daysRemainingText.text = "";
            }

           



            yield return new WaitForSeconds(3.15f);

            TimeManager.instance.NewDay();
        
            TownDialogueData tdd = StorylineManager.instance.townEventDialogues;
            
            if (tdd.currentQuestChainIndex < tdd.td.Count )
            {
               
                if (tdd.td[tdd.currentQuestChainIndex].dayRequiredCount == (p_dayCount + 1))
                {

                    CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("TE-"+ tdd.currentQuestChainIndex.ToString(), tdd.td[tdd.currentQuestChainIndex].so_Dialogue);
                    Debug.Log("The quest get song");
                    AudioManager.instance.PlayOnRoomEnterString("Quest Get");

                    TimeManager.onPauseGameTime.Invoke(false);
                }
            }
            else
            {
                UIManager.instance.characterDialogueUI.SetEndTransitionEnabledEvent(true);
                UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(true);
                TimeManager.onPauseGameTime.Invoke(false);
            }
         

            frame.SetActive(false);
         
        }
        else
        {
            Debug.Log("ENDING");
            TimeManager.onPauseGameTime.Invoke(false);
            TransitionUI.onFadeTransition(1f);
            fader.color = new Color32(0, 0, 0,0);
            frame.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            TransitionUI.onFadeTransition(0f);



            if (StorylineManager.instance.amountQuestComplete >= 8)
            {
                CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("GOODENDING", StorylineManager.instance.goodso_dialogue);
                AudioManager.instance.PlayOnRoomEnterString("QuestComplete");
            }
            else
            {
                CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("BADENDING", StorylineManager.instance.badso_dialogue);
                AudioManager.instance.PlayOnRoomEnterString("Quest Get");
            }
        }

    }


}
