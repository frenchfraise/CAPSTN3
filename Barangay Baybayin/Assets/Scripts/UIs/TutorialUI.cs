using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class RemindTutorialEvent : UnityEvent<string> { }
public class TutorialUI : MonoBehaviour
{
    public GameObject frame;
    public Image reminderFramesContainer;
    public Sprite timeDay;
    public Sprite weatherRadio;
    public Sprite bed;
    public Sprite inventory;
    public Sprite quests;
    public Sprite map;
    public Sprite critHits;
    public Sprite food;

    public bool isTimeDay;
    public bool isWeatherRadio;
    public bool isBed;
    public bool isInventory;
    public bool isQuests;
    public bool isMap;
    public bool isCritHits;
    public bool isFood;

    public Button button;
    public GameObject overheadUI;
    public TMP_Text overheadText;

    public static RemindTutorialEvent onRemindTutorialEvent = new RemindTutorialEvent();
    private void Awake()
    {
        onRemindTutorialEvent.AddListener(RemindTutorialEvent);
    }
    public void SetVisibility(bool p_bool)
    {
        frame.SetActive(p_bool);
    }
    public void SetButton(bool p_bool)
    {
        button.enabled = (p_bool);
    }
    public void SetOverheadText(string p_string)
    {
        overheadText.text = p_string;
    }

    public void OnCloseReminderButtonUI()
    {
        if (isTimeDay)
        {

        }
        else if (isWeatherRadio)
        {
            Radio.onRadioFirstTimeEvent.Invoke();
        }
        else if (isBed)
        {
            Bed.onBedFirstTimeEvent.Invoke();
        }
        else if (isInventory)
        {

        }
        else if (isQuests)
        {
            CharacterDialogueUI.onQuestAcceptFirstTimeEvent.Invoke();
        }
        else if (isMap)
        {
            RoomInfoUI.onLeaveFirstTimeEvent.Invoke();
        }
        else if (isCritHits)
        {
            ToolCaster.onCriticalFirstTimeEvent.Invoke();
        }
        else if (isFood)
        {
            Food.onFoodFirstTimeUseEvent.Invoke();
        }
        isTimeDay = false;
        isWeatherRadio = false;
        isBed = false;
        isInventory = false;
        isQuests = false;
        isMap = false;
        isCritHits = false;
        isFood = false;
        reminderFramesContainer.gameObject.SetActive(false);
    }

    public void RemindTutorialEvent(string p_tutorialReminder)
    {


        if (p_tutorialReminder == "timeDay")
        {
            reminderFramesContainer.sprite = timeDay;
            isTimeDay = true;
            reminderFramesContainer.gameObject.SetActive(true);

        }
        else if (p_tutorialReminder == "weatherRadio")
        {
            reminderFramesContainer.sprite = weatherRadio;
            isWeatherRadio = true;
            reminderFramesContainer.gameObject.SetActive(true);

        }
        else if (p_tutorialReminder == "bed")
        {
            reminderFramesContainer.sprite = bed;
            isBed = true;
            reminderFramesContainer.gameObject.SetActive(true);

        }
        else if (p_tutorialReminder == "inventory")
        {
            reminderFramesContainer.sprite = inventory;
            reminderFramesContainer.gameObject.SetActive(true);
            isInventory = true;
        }
        else if (p_tutorialReminder == "quests")
        {
            reminderFramesContainer.sprite = quests;
            reminderFramesContainer.gameObject.SetActive(true);

            isQuests = true;
        }
        else if (p_tutorialReminder == "map")
        {
            reminderFramesContainer.sprite = map;
            isMap = true;
            reminderFramesContainer.gameObject.SetActive(true);

        }
        else if (p_tutorialReminder == "critHits")
        {
            reminderFramesContainer.sprite = critHits;
            isCritHits = true;
            reminderFramesContainer.gameObject.SetActive(true);

        }
        else if (p_tutorialReminder == "food")
        {
            reminderFramesContainer.sprite = food;
            reminderFramesContainer.gameObject.SetActive(true);
            isFood = true;
        }
        else
        {
            reminderFramesContainer.gameObject.SetActive(false);
        }
    }
}