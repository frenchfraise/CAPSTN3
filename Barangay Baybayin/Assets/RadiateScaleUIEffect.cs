using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using DG.Tweening;
public class RadiateScaleUIEffect : MonoBehaviour
{
    [SerializeField] private RectTransform affectedTransform;


    [SerializeField] private RectTransform originalTransform;
    [SerializeField] private RectTransform targetTransform;
    [SerializeField] private float scaleUpTime;
    [SerializeField] private float delayTime;
    [SerializeField] private float scaleDownTime;
    public IEnumerator runningCoroutine;
    private void Awake()
    {

        runningCoroutine = Co_Scale();
        StartCoroutine(runningCoroutine);
    }
    public IEnumerator Co_Scale()
    {
        Debug.Log("Scale ");

        var sequence = DOTween.Sequence()
        .Append(affectedTransform.DOSizeDelta(originalTransform.sizeDelta, scaleDownTime));
        sequence.Play();
        yield return sequence.WaitForCompletion();
        yield return new WaitForSeconds(delayTime);
        var sequenceTwo = DOTween.Sequence()
        .Append(affectedTransform.DOSizeDelta(targetTransform.sizeDelta, scaleUpTime));
        sequence.Play();
        yield return sequenceTwo.WaitForCompletion();
        Debug.Log("Scale down ");
        runningCoroutine = Co_Scale();
        StartCoroutine(runningCoroutine);
    }
}
