using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HoverEffect : MonoBehaviour
{
    [SerializeField] private float hoverDownPeakOffset;
    [SerializeField] private float hoverUpPeakOffset;
    [SerializeField] private float hoverUpTime;
    //[SerializeField] private float hoverUpSpeed;
    //[SerializeField] private float hoverUpRate;
    [SerializeField] private float delayTime;
    [SerializeField] private float hoverDownTime;
    //[SerializeField] private float hoverDownSpeed;
    //[SerializeField] private float hoverDownRate;
    public float startYPosition;
    public IEnumerator runningCoroutine;

    [HideInInspector] public SpriteRenderer sr;
     public SpriteRenderer srIcon;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        //runningCoroutine = Co_Hover();
        //StartCoroutine(runningCoroutine);
    }

    private void OnDisable()
    {
        //if (runningCoroutine != null)
        //{
        //    StopCoroutine(runningCoroutine);
        //}
  
    }


    public IEnumerator Co_Hover()
    {
        

        var sequence = DOTween.Sequence()
        .Append(transform.DOMoveY(startYPosition+hoverUpPeakOffset, hoverUpTime));
        sequence.Play();
        yield return sequence.WaitForCompletion();
        yield return new WaitForSeconds(delayTime);
        var sequenceTwo = DOTween.Sequence()
        .Append(transform.DOMoveY(startYPosition-hoverDownPeakOffset, hoverDownTime));
        sequenceTwo.Play();
        yield return sequenceTwo.WaitForCompletion();

  
        
        runningCoroutine = Co_Hover();
        StartCoroutine(runningCoroutine);
    }
}
