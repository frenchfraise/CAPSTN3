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
    public TMP_Text conditionsText;
    private bool fainted;
    private bool isFirstTime = true;

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
            isFirstTime = false;
            TimeManager.onDayChangingEvent.Invoke();
        }

    }

    IEnumerator Co_DayEndTransition(bool p_causedByFainting, int p_dayCount)
    {
        TimeManager.onPauseGameTime.Invoke(false);
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

        }

        UIManager.TransitionFade(1);
        yield return new WaitForSeconds(0.5f);
        //TimeManager.onDayChangingEvent.Invoke();
        Sequence tr = DOTween.Sequence();
        tr.Join(conditionsText.DOFade(1f, 0.75f));
        tr.Join(dayCountText.DOFade(1f, 0.75f));
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
        yield return trt.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        conditionsText.text = "STARTS";
        dayCountText.text = "DAY " + (p_dayCount + 1).ToString();
        dayText.text = "DAY " + (p_dayCount + 1).ToString();
        Sequence t = DOTween.Sequence();
        t.Join(conditionsText.DOFade(1f, 0.75f));
        t.Join(dayCountText.DOFade(1f, 0.75f));
        
       
        yield return t.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        Sequence te = DOTween.Sequence();
        te.Join(conditionsText.DOFade(0f, 0.5f));
        te.Join(dayCountText.DOFade(0f, 0.5f));
        yield return te.WaitForCompletion();
        UIManager.TransitionFade(0, false) ;
        TimeManager.instance.NewDay();
        frame.SetActive(false);
        TimeManager.onPauseGameTime.Invoke(true);
    }


}
