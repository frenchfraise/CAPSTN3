using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlinkEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI affectedText;
    [SerializeField] private float blinkRate;


    private void Awake()
    {
        StartCoroutine(Co_Blink());
    }
    IEnumerator Co_Blink()
    {
        affectedText.enabled = false;
        yield return new WaitForSeconds(blinkRate);
        affectedText.enabled = true;
        yield return new WaitForSeconds(blinkRate);
        StartCoroutine(Co_Blink());
    }
}
