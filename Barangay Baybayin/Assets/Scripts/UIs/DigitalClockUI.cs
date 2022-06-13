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

    // Update is called once per frame
    private void UpdateTime(int hour, int minute, int minuteByTens)
    {
        textDisplay.text = $"{hour:00}:{minuteByTens:00}";
    }
}
