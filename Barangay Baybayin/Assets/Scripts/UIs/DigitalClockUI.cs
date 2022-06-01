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

        TimeManager.onHourChangedEvent.AddListener(UpdateTime);
        TimeManager.onMinuteChangedEvent.AddListener(UpdateTime);
    }

    private void OnDisable()
    {
        TimeManager.onHourChangedEvent.RemoveListener(UpdateTime);
        TimeManager.onMinuteChangedEvent.RemoveListener(UpdateTime);
    }

    // Update is called once per frame
    private void UpdateTime()
    {
        textDisplay.text = $"{TimeManager.hour:00}:{TimeManager.minute:00}";
    }
}
