using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class DayInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject frame;
    [SerializeField] private RadiateScaleUIEffect faintRadiateScaleUI;
    public TMP_Text dayText; //make this action

    public TMP_Text dayCountText;
    public TMP_Text conditionsText;
    private bool fainted;
    private bool isFirstTime = true;
    private void Awake()
    {
        faintRadiateScaleUI.gameObject.SetActive(false);
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

        if (!isFirstTime)
        {
            frame.SetActive(true);
            StartCoroutine(Co_DayEndTransition(p_causedByFainting, p_dayCount));
        }
        else if (isFirstTime)
        {
            Debug.Log("FIRST TIME");
            isFirstTime = false;
            StartCoroutine(Co_fIRSTDayEndTransition());
            //TimeManager.onDayChangingEvent.Invoke();
        }

    }
    IEnumerator Co_fIRSTDayEndTransition()
    {
        TimeManager.onPauseGameTime.Invoke(false);
        Debug.Log("FIRST TIME");
        

        TransitionUI.onFadeTransition.Invoke(1);
        yield return new WaitForSeconds(0.5f);
        //TimeManager.onDayChangingEvent.Invoke();
        Sequence tr = DOTween.Sequence();
        tr.Append(conditionsText.DOFade(1f, 0.75f));
        tr.Join(dayCountText.DOFade(1f, 0.75f));
        tr.Play();
        yield return tr.WaitForCompletion();

        yield return new WaitForSeconds(1f);

        //TimeManager.onDayChangingEvent.Invoke();

        //PlayerManager.instance.stamina.transform.position = PlayerManager.instance.bed.transform.position;
        // Uncomment this out when it is READY
        // UIManager.instance.recipeUpgrade.SetActive(false);
        // CameraManager.onCameraMovedEvent.Invoke(new Vector3(0, 0, -10), new Vector3(41.5f, 20f, 0f));
        Sequence trt = DOTween.Sequence();
        trt.Join(conditionsText.DOFade(0f, 0.75f));
        trt.Join(dayCountText.DOFade(0f, 0.75f));
        trt.Play();
        yield return trt.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        conditionsText.text = "STARTS";
        dayCountText.text = "DAY " + (1).ToString();
        dayText.text = "DAY " + (1).ToString();
        Sequence t = DOTween.Sequence();
        t.Join(conditionsText.DOFade(1f, 0.75f));
        t.Join(dayCountText.DOFade(1f, 0.75f));
        t.Play();

        yield return t.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        Sequence te = DOTween.Sequence();
        te.Join(conditionsText.DOFade(0f, 0.5f));
        te.Join(dayCountText.DOFade(0f, 0.5f));
        te.Play();
        yield return te.WaitForCompletion();
        TransitionUI.onFadeTransition.Invoke(0, false);
        TimeManager.instance.NewDay();
        frame.SetActive(false);
        TimeManager.onPauseGameTime.Invoke(true);

        TownDialogueData tdd = StorylineManager.instance.townEventDialogues;
        Debug.Log("TRY");
        Debug.Log("TRY " + tdd.td[tdd.currentQuestChainIndex].dayRequiredCount + " - - " + ( 1));
        if (tdd.td[tdd.currentQuestChainIndex].dayRequiredCount == (1))
        {
            Debug.Log("TEEEEEEEEEEEE");
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("TE", tdd.td[tdd.currentQuestChainIndex].so_Dialogue);
        }

    }
    IEnumerator Co_DayEndTransition(bool p_causedByFainting, int p_dayCount)
    {
        TimeManager.onPauseGameTime.Invoke(false);
        Debug.Log("GOING TO TRY");
        if (!p_causedByFainting)
        {
            conditionsText.text = "ENDED";
            dayCountText.text = "DAY " + (p_dayCount).ToString();

        }
        else
        {
            conditionsText.text = "YOU FAINTED";
            dayCountText.text = "DAY " + (p_dayCount).ToString();
            dayText.text = "DAY " + (p_dayCount + 1).ToString();
            faintRadiateScaleUI.gameObject.SetActive(true);
            if (faintRadiateScaleUI.runningCoroutine != null) 
            {
                StopCoroutine(faintRadiateScaleUI.runningCoroutine);
                faintRadiateScaleUI.runningCoroutine = null;

            }
            faintRadiateScaleUI.runningCoroutine = faintRadiateScaleUI.Co_Scale();
            StartCoroutine(faintRadiateScaleUI.runningCoroutine);
            yield return new WaitForSeconds(2f);

            if (faintRadiateScaleUI.runningCoroutine != null)
            {
                StopCoroutine(faintRadiateScaleUI.runningCoroutine);
                faintRadiateScaleUI.runningCoroutine = null;

            }
            faintRadiateScaleUI.gameObject.SetActive(false);

        }

        TransitionUI.onFadeTransition.Invoke(1);
        yield return new WaitForSeconds(0.5f);
        //TimeManager.onDayChangingEvent.Invoke();
        Sequence tr = DOTween.Sequence();
        tr.Append(conditionsText.DOFade(1f, 0.75f));
        tr.Join(dayCountText.DOFade(1f, 0.75f));
        tr.Play();
        yield return tr.WaitForCompletion();

        yield return new WaitForSeconds(1f);

        TimeManager.onDayChangingEvent.Invoke();

        //PlayerManager.instance.stamina.transform.position = PlayerManager.instance.bed.transform.position;
        // Uncomment this out when it is READY
        // UIManager.instance.recipeUpgrade.SetActive(false);
        // CameraManager.onCameraMovedEvent.Invoke(new Vector3(0, 0, -10), new Vector3(41.5f, 20f, 0f));
        Sequence trt = DOTween.Sequence();
        trt.Join(conditionsText.DOFade(0f, 0.75f));
        trt.Join(dayCountText.DOFade(0f, 0.75f));
        trt.Play();
        yield return trt.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        conditionsText.text = "STARTS";
        dayCountText.text = "DAY " + (p_dayCount + 1).ToString();
        dayText.text = "DAY " + (p_dayCount + 1).ToString();
        Sequence t = DOTween.Sequence();
        t.Join(conditionsText.DOFade(1f, 0.75f));
        t.Join(dayCountText.DOFade(1f, 0.75f));
        t.Play();

        yield return t.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        Sequence te = DOTween.Sequence();
        te.Join(conditionsText.DOFade(0f, 0.5f));
        te.Join(dayCountText.DOFade(0f, 0.5f));
        te.Play();
        yield return te.WaitForCompletion();
        TransitionUI.onFadeTransition.Invoke(0, false);
        TimeManager.instance.NewDay();
        frame.SetActive(false);
        TimeManager.onPauseGameTime.Invoke(true);

        TownDialogueData tdd = StorylineManager.instance.townEventDialogues;
        Debug.Log("TRY");
        Debug.Log("TRY " + tdd.td[tdd.currentQuestChainIndex].dayRequiredCount + " - - " + (p_dayCount + 1));
        if (tdd.td[tdd.currentQuestChainIndex].dayRequiredCount == (p_dayCount + 1))
        {
            Debug.Log("TEEEEEEEEEEEE");
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("TE", tdd.td[tdd.currentQuestChainIndex].so_Dialogue);
        }

    }


}
