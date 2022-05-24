using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class DayInfoUI : MonoBehaviour
{
    public TMP_Text dayCountTitle;
    public TMP_Text dayCountText;
    public bool fainted;
  
    public void test(int p_dayCount)
    {
        
        gameObject.SetActive(true);
        if (!fainted)
        {
            StartCoroutine(Co_test(p_dayCount));

        }
        else
        {
            StartCoroutine(Co_teste(p_dayCount));
            fainted = false;
        }
        
    }

    IEnumerator Co_test(int p_dayCount)
    {
        Debug.Log("DAY END");
        dayCountTitle.text = "DAY";
        dayCountText.text = (p_dayCount).ToString() + " ENDED";
        
        UIManager.TransitionFade(1);
        yield return new WaitForSeconds(0.5f);
        Sequence tr = DOTween.Sequence();
        tr.Join(dayCountTitle.DOFade(1f, 0.75f));
        tr.Join(dayCountText.DOFade(1f, 0.75f));
        yield return tr.WaitForCompletion();
        yield return new WaitForSeconds(1f);


        PlayerManager.instance.stamina.transform.position = PlayerManager.instance.bed.transform.position;
        CameraManager.instance.onCameraMoved.Invoke(new Vector3(0, 0, -10));
        Sequence trt = DOTween.Sequence();
        trt.Join(dayCountTitle.DOFade(0f, 0.75f));
        trt.Join(dayCountText.DOFade(0f, 0.75f));
        yield return trt.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        dayCountTitle.text = "DAY";
        dayCountText.text = (p_dayCount + 1).ToString().ToString();
        Sequence t = DOTween.Sequence();
        t.Join(dayCountTitle.DOFade(1f, 0.75f));
        t.Join(dayCountText.DOFade(1f, 0.75f));
        
       
        yield return t.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        Sequence te = DOTween.Sequence();
        te.Join(dayCountTitle.DOFade(0f, 0.5f));
        te.Join(dayCountText.DOFade(0f, 0.5f));
        yield return te.WaitForCompletion();
        UIManager.TransitionFade(0, false) ;
        TimeManager.instance.NewDay();
        gameObject.SetActive(false);
    }

    public void Faint(int p_dayCount)
    {
        gameObject.SetActive(true);
        StartCoroutine(Co_teste(p_dayCount));
        fainted = true;
    }

    IEnumerator Co_teste(int p_dayCount)
    {
        Debug.Log("FAINTED");
        dayCountTitle.text = "YOU";
        dayCountText.text = "FAINTED";

        UIManager.TransitionFade(1);
        yield return new WaitForSeconds(0.5f);
        Sequence tr = DOTween.Sequence();
        tr.Join(dayCountTitle.DOFade(1f, 0.75f));
        tr.Join(dayCountText.DOFade(1f, 0.75f));
        yield return tr.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        PlayerManager.instance.stamina.transform.position = PlayerManager.instance.bed.transform.position;
        CameraManager.instance.onCameraMoved.Invoke(new Vector3(0,0,-10));
        Sequence trt = DOTween.Sequence();
        trt.Join(dayCountTitle.DOFade(0f, 0.75f));
        trt.Join(dayCountText.DOFade(0f, 0.75f));
        yield return trt.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        dayCountTitle.text = "DAY";
        dayCountText.text = (p_dayCount + 1).ToString().ToString();
        Sequence t = DOTween.Sequence();
        t.Join(dayCountTitle.DOFade(1f, 0.75f));
        t.Join(dayCountText.DOFade(1f, 0.75f));


        yield return t.WaitForCompletion();
        yield return new WaitForSeconds(1f);

        Sequence te = DOTween.Sequence();
        te.Join(dayCountTitle.DOFade(0f, 0.5f));
        te.Join(dayCountText.DOFade(0f, 0.5f));
        yield return te.WaitForCompletion();
        UIManager.TransitionFade(0, false);
        TimeManager.instance.NewDay();
        gameObject.SetActive(false);
    }
}
