using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RadiateScaleEffect : MonoBehaviour
{

    [SerializeField] private Transform affectedTransform;

    [SerializeField] private float scaleUpPeakOffset;
    [SerializeField] private float scaleUpTime;
    [SerializeField] private float delayTime;
    [SerializeField] private float scaleDownTime;
    public float scaleDownPeakOffset;
    public IEnumerator runningCoroutine;
    private void Awake()
    {

    }
    public IEnumerator Co_Scale()
    {
        Debug.Log("Scale ");
        var sequence = DOTween.Sequence()
        .Append(affectedTransform.DOScale(new Vector3(scaleDownPeakOffset, scaleDownPeakOffset), scaleDownTime));
        sequence.Play();
        yield return sequence.WaitForCompletion();
        yield return new WaitForSeconds(delayTime);
        var sequenceTwo = DOTween.Sequence()
        .Append(affectedTransform.DOScale(new Vector3(scaleUpPeakOffset, scaleUpPeakOffset), scaleUpTime));
        sequence.Play();
        yield return sequenceTwo.WaitForCompletion();
        Debug.Log("Scale down ");
        runningCoroutine = Co_Scale();
        StartCoroutine(runningCoroutine);
    }




}
