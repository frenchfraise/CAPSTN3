using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    [SerializeField] private float hoverUpPeakOffset;
    [SerializeField] private float hoverUpSpeed;
    [SerializeField] private float hoverUpRate;
    [SerializeField] private float delayTime;
    [SerializeField] private float hoverDownSpeed;
    [SerializeField] private float hoverDownRate;
    public float startYPosition;
    public IEnumerator runningCoroutine;

    private void OnEnable()
    {
        //startYPosition = transform.position.y;
       // runningCoroutine = StartCoroutine(Co_Hover());
        
    }

    private void OnDisable()
    {
        //if (runningCoroutine != null)
        //{
        //     StopCoroutine(runningCoroutine);
        //}
    }


    public IEnumerator Co_Hover()
    {
        

        while (transform.position.y < startYPosition + hoverUpPeakOffset)
        {
            transform.position += new Vector3(0, hoverUpSpeed);
            yield return new WaitForSeconds(hoverUpRate);
        }


        yield return new WaitForSeconds(delayTime);

        while (transform.position.y > startYPosition)
        {
            transform.position -= new Vector3(0, hoverDownSpeed);
            yield return new WaitForSeconds(hoverDownRate);
        }
        runningCoroutine = Co_Hover();
        StartCoroutine(runningCoroutine);
    }
}
