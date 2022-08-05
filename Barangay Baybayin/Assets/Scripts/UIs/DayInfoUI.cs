using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class DayInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject frame;

    public TMP_Text dayText; //make this action

    public TMP_Text dayCountText;
    public TMP_Text daysRemainingText;
    public TMP_Text conditionsText;
    private bool fainted;
    private bool isFirstTime = true;

    private void Awake()
    {

        DayEnd(false,0);
    }
    private void OnEnable()
    {
        TimeManager.onDayEndedEvent.AddListener(DayEnd);

     
    }
    private void OnDisable()
    {
        TimeManager.onDayEndedEvent.RemoveListener(DayEnd);
    }

    public void DayEnd(bool p_causedByFainting, int p_dayCount)
    {        
        TransitionUI.onFadeTransition.Invoke(0, false); //BAND-AID SOLUTION
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
        daysRemainingText.color = new Color32(0, 0, 0, 0);
        conditionsText.text = "";
        dayCountText.text = "MAY " + (1).ToString();

        conditionsText.text = "STARTS";
       
    
        TransitionUI.onFadeTransition.Invoke(1);
        yield return new WaitForSeconds(0.5f);
     
        Sequence tr = DOTween.Sequence();
        tr.Append(conditionsText.DOFade(1f, 0.75f));
        tr.Join(dayCountText.DOFade(1f, 0.75f));
        tr.Play();
        yield return tr.WaitForCompletion();

        yield return new WaitForSeconds(1f);

        Sequence trt = DOTween.Sequence();
        trt.Join(conditionsText.DOFade(0f, 0.75f));
        trt.Join(dayCountText.DOFade(0f, 0.75f));
        trt.Play();
        yield return trt.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        TransitionUI.onFadeTransition.Invoke(0, false);
        TimeManager.instance.NewDay();
        frame.SetActive(false);
        TimeManager.onPauseGameTime.Invoke(true);


    }
    IEnumerator Co_DayEndTransition(bool p_causedByFainting, int p_dayCount)
    {
        if (TimeManager.instance.daysRemaining > 0)
        {

     
            TimeManager.onPauseGameTime.Invoke(false);
            // Debug.Log("GOING TO TRY");
            if (!p_causedByFainting)
            {
                conditionsText.text = "ENDED";
                dayCountText.text = "MAY " + (p_dayCount).ToString();
                if (TimeManager.instance.daysRemaining <= 15)
                {
                    daysRemainingText.text = (TimeManager.instance.daysRemaining - 1).ToString() + " DAYS REMAINING";
                    daysRemainingText.color = new Color32(255, 255, 255, 0);
                }
                else
                {
                    daysRemainingText.color = new Color32(0, 0, 0, 0);
                }
            }
            else
            {
                conditionsText.text = "YOU FAINTED";
                dayCountText.text = "MAY " + (p_dayCount).ToString();
                dayText.text = "MAY " + (p_dayCount + 1).ToString();
                if (TimeManager.instance.daysRemaining <= 15)
                {
                    daysRemainingText.text = (TimeManager.instance.daysRemaining - 1).ToString() + " DAYS REMAINING";
                    daysRemainingText.color = new Color32(255, 255, 255, 0);

                }
                else
                {
                    daysRemainingText.color = new Color32(0, 0, 0, 0);
                }

             

            }

            TransitionUI.onFadeTransition.Invoke(1);
            yield return new WaitForSeconds(0.5f);
            //TimeManager.onDayChangingEvent.Invoke();
            Sequence tr = DOTween.Sequence();
            tr.Append(conditionsText.DOFade(1f, 0.75f));
            tr.Join(dayCountText.DOFade(1f, 0.75f));
            //if (TimeManager.instance.daysRemaining <= 12)
            //{
            //    tr.Join(daysRemainingText.DOFade(1f, 0.75f));
            //}
            tr.Play();
            yield return tr.WaitForCompletion();

            yield return new WaitForSeconds(1f);

            //Debug.Log("day changing call!");
            TimeManager.onDayChangingEvent.Invoke();

            //PlayerManager.instance.stamina.transform.position = PlayerManager.instance.bed.transform.position;
            // Uncomment this out when it is READY
            // UIManager.instance.recipeUpgrade.SetActive(false);
            // CameraManager.onCameraMovedEvent.Invoke(new Vector3(0, 0, -10), new Vector3(41.5f, 20f, 0f));
            Sequence trt = DOTween.Sequence();
            trt.Join(conditionsText.DOFade(0f, 0.75f));
            trt.Join(dayCountText.DOFade(0f, 0.75f));
            //if (TimeManager.instance.daysRemaining <= 12)
            //{
            //    trt.Join(daysRemainingText.DOFade(0f, 0.75f));
            //}
            trt.Play();
            yield return trt.WaitForCompletion();
            yield return new WaitForSeconds(1f);

            conditionsText.text = "STARTS";
            dayCountText.text = "MAY " + (p_dayCount + 1).ToString();
            dayText.text = "MAY " + (p_dayCount + 1).ToString();
            Sequence t = DOTween.Sequence();
            t.Join(conditionsText.DOFade(1f, 0.75f));
            t.Join(dayCountText.DOFade(1f, 0.75f));
            if (TimeManager.instance.daysRemaining <= 15)
            {
                t.Join(daysRemainingText.DOFade(1f, 0.75f));
            }
            t.Play();

            yield return t.WaitForCompletion();
            yield return new WaitForSeconds(1f);

       
            TimeManager.instance.NewDay();
        

            TownDialogueData tdd = StorylineManager.instance.townEventDialogues;
            //  Debug.Log("TRY");
            // Debug.Log("TRY " + tdd.td[tdd.currentQuestChainIndex].dayRequiredCount + " - - " + (p_dayCount + 1));
            if (tdd.currentQuestChainIndex < tdd.td.Count )
            {
               
                if (tdd.td[tdd.currentQuestChainIndex].dayRequiredCount == (p_dayCount + 1))
                {
            
               

                    CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("TE-"+ tdd.currentQuestChainIndex.ToString(), tdd.td[tdd.currentQuestChainIndex].so_Dialogue);
                    TimeManager.onPauseGameTime.Invoke(false);
                }
            }
            else
            {
                UIManager.instance.characterDialogueUI.SetEndTransitionEnabledEvent(true);
                UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(true);
                TimeManager.onPauseGameTime.Invoke(false);
            }
            Sequence te = DOTween.Sequence();
            te.Join(conditionsText.DOFade(0f, 0.5f));
            te.Join(dayCountText.DOFade(0f, 0.5f));
            if (TimeManager.instance.daysRemaining <= 15)
            {
                te.Join(daysRemainingText.DOFade(0f, 0.5f));
            }

            te.Play();
            yield return te.WaitForCompletion();
            TransitionUI.onFadeTransition.Invoke(0, false);
            yield return new WaitForSeconds(0.5f);
            frame.SetActive(false);
            
   
        }
        else
        {
            TimeManager.onPauseGameTime.Invoke(false);
            TransitionUI.onFadeTransition.Invoke(1);
            yield return new WaitForSeconds(0.5f);
            TransitionUI.onFadeTransition.Invoke(0, false);
            yield return new WaitForSeconds(0.5f);
     
            frame.SetActive(false);
            if (StorylineManager.instance.amountQuestComplete >= 8)
            {
                CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("GOODENDING", StorylineManager.instance.goodso_dialogue);

            }
            else
            {
                CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("BADENDING", StorylineManager.instance.badso_dialogue);
            }
        }

    }


}
