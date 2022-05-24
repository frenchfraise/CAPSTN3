using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class DayInfoUI : MonoBehaviour
{
    public TMP_Text dayCountText;
    public TMP_Text conditionsText;
    public bool fainted;
    public bool isFirstTime = true;
    public void DayEnd(int p_dayCount)
    {
        
        if (!isFirstTime)
        {
            gameObject.SetActive(true);
            if (!fainted)
            {
                StartCoroutine(Co_DayEndTransition(p_dayCount));

            }
            else
            {
                StartCoroutine(Co_DayFaintedTransition(p_dayCount));
                fainted = false;
            }
        }
        if (isFirstTime)
        {
            isFirstTime = false;
        }


    }

    IEnumerator Co_DayEndTransition(int p_dayCount)
    {
        Debug.Log("DAY END");
        conditionsText.text = "ENDED";
        dayCountText.text = "DAY " + (p_dayCount).ToString();
        
        UIManager.TransitionFade(1);
        yield return new WaitForSeconds(0.5f);
        Sequence tr = DOTween.Sequence();
        tr.Join(conditionsText.DOFade(1f, 0.75f));
        tr.Join(dayCountText.DOFade(1f, 0.75f));
        yield return tr.WaitForCompletion();
        yield return new WaitForSeconds(1f);


        PlayerManager.instance.stamina.transform.position = PlayerManager.instance.bed.transform.position;
        CameraManager.instance.onCameraMoved.Invoke(new Vector3(0, 0, -10));
        Sequence trt = DOTween.Sequence();
        trt.Join(conditionsText.DOFade(0f, 0.75f));
        trt.Join(dayCountText.DOFade(0f, 0.75f));
        yield return trt.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        conditionsText.text = "STARTS";
        dayCountText.text = "DAY " + (p_dayCount + 1).ToString();
        UIManager.instance.dayText.text = "DAY " + (p_dayCount + 1).ToString();
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
        gameObject.SetActive(false);
    }

    public void Faint(int p_dayCount)
    {
        gameObject.SetActive(true);
        StartCoroutine(Co_DayFaintedTransition(p_dayCount));
        fainted = true;
    }

    IEnumerator Co_DayFaintedTransition(int p_dayCount)
    {
        Debug.Log("FAINTED");
        conditionsText.text = "YOU FAINTED";
        dayCountText.text = "DAY " + (p_dayCount).ToString();
        UIManager.instance.dayText.text = "DAY " + (p_dayCount + 1).ToString();

        UIManager.TransitionFade(1);
        yield return new WaitForSeconds(0.5f);
        Sequence tr = DOTween.Sequence();
        tr.Join(conditionsText.DOFade(1f, 0.75f));
        tr.Join(dayCountText.DOFade(1f, 0.75f));
        yield return tr.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        PlayerManager.instance.stamina.transform.position = PlayerManager.instance.bed.transform.position;
        CameraManager.instance.onCameraMoved.Invoke(new Vector3(0,0,-10));
        Sequence trt = DOTween.Sequence();
        trt.Join(conditionsText.DOFade(0f, 0.75f));
        trt.Join(dayCountText.DOFade(0f, 0.75f));
        yield return trt.WaitForCompletion();
        yield return new WaitForSeconds(1f);

  
        Sequence t = DOTween.Sequence();
        t.Join(conditionsText.DOFade(1f, 0.75f));
        t.Join(dayCountText.DOFade(1f, 0.75f));


        yield return t.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        Sequence te = DOTween.Sequence();
        te.Join(conditionsText.DOFade(0f, 0.5f));
        te.Join(dayCountText.DOFade(0f, 0.5f));
        yield return te.WaitForCompletion();
        UIManager.TransitionFade(0, false);
        TimeManager.instance.NewDay();
        gameObject.SetActive(false);
    }
}
