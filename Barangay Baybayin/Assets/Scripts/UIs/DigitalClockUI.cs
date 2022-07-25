using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DigitalClockUI : MonoBehaviour
{
    private TMP_Text textDisplay;
    private void Awake()
    {
        textDisplay = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {        
        TimeManager.onTimeChangedEvent.AddListener(UpdateTime);
    }

    private void OnDisable()
    {
        TimeManager.onTimeChangedEvent.RemoveListener(UpdateTime);
    }

    private void UpdateTime(int hour24, int hour12, int minute, int minuteByTens)
    {
        //Debug.Log(TimeManager.);
        textDisplay.text = $"{hour12:00}:{minuteByTens:00} {TimeManager.instance.abbreviation}";
        //textDisplay.text = hour12.ToString() + ":" + minuteByTens.ToString() + " " + TimeManager.abbreviation;
    }
}
