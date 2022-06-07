using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClockUI : MonoBehaviour
{
    [SerializeField] private RectTransform hand;

    const float hoursToDegrees = 180 / 24;

    // Start is called before the first frame update
    void Start()
    {
        hand.localRotation = Quaternion.Euler(0, 0, 90); 
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
        hand.localRotation = Quaternion.Euler(0, 0, 90 + 
            hoursToDegrees * ((TimeManager.hour + 
            TimeManager.hoursInDay - 
            TimeManager.sunriseHour) % 
            TimeManager.hoursInDay));
    }
}
