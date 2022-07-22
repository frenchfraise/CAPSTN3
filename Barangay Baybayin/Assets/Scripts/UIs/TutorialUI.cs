using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class RemindTutorialEvent : UnityEvent<int> { }
[System.Serializable]
public class PopUpTutorialPanel
{
    [NonReorderable] public List<Sprite> panels;
}
public class TutorialUI : MonoBehaviour
{
    public GameObject frame;
    public Image reminderFramesContainer;
    public Button nextButton;
    public Button reminderFrameButton;
    [NonReorderable] public List<PopUpTutorialPanel> popUps;
    public int currentTutorialIndex;
    public int currentIndex;
  

    public bool isTimeDay;

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

    private void OnDestroy()
    {
        onRemindTutorialEvent.RemoveListener(RemindTutorialEvent);
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
        //reminderFrameButton.enabled = false;
       
        UIManager.onGameplayModeChangedEvent.Invoke(false);
        //TimeManager.onPauseGameTime.Invoke(true);

        isTimeDay = false;
    
        isInventory = false;
        isQuests = false;
        isMap = false;
        isCritHits = false;
        isFood = false;
        reminderFramesContainer.gameObject.SetActive(false);
    }
    public void Check()
    {
        if (currentIndex < popUps[currentTutorialIndex].panels.Count - 1)
        {

            reminderFramesContainer.sprite = popUps[currentTutorialIndex].panels[currentIndex];

        }
        else if (currentIndex >= popUps[currentTutorialIndex].panels.Count - 1)
        {
            if (currentIndex == popUps[currentTutorialIndex].panels.Count - 1)
            {
                reminderFramesContainer.sprite = popUps[currentTutorialIndex].panels[currentIndex];
            }
            else if (currentIndex > popUps[currentTutorialIndex].panels.Count - 1)
            {

            }

            nextButton.gameObject.SetActive(false);
            //reminderFrameButton.enabled = true;
        }
    }
    public void OnNextReminderButtonUI()
    {
        currentIndex++;
        Check();

        //else 
        //{

        //    OnCloseReminderButtonUI();
        //}
    }

    public void RemindTutorialEvent(int p_tutorialReminder)
    {
        UIManager.onGameplayModeChangedEvent.Invoke(true);
        //TimeManager.onPauseGameTime.Invoke(false);
        //reminderFrameButton.enabled = false;
        currentIndex = 0;
       // Debug.Log("TUTORIAL UI: " +p_tutorialReminder);
     
        if (p_tutorialReminder < popUps.Count)
        {
            currentTutorialIndex = p_tutorialReminder;
            reminderFramesContainer.sprite = popUps[currentTutorialIndex].panels[currentIndex];
          
            reminderFramesContainer.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
            Check();
        }
        //else if (p_tutorialReminder == "quests")
        //{
        //    currentTutorialIndex = 1;
        //    reminderFramesContainer.sprite = popUps[currentTutorialIndex].panels[currentIndex];

        //    reminderFramesContainer.gameObject.SetActive(true);

        //}
     
        //else if (p_tutorialReminder == "map")
        //{
        //    currentTutorialIndex = 2;
        //    reminderFramesContainer.sprite = popUps[currentTutorialIndex].panels[currentIndex];

        //    reminderFramesContainer.gameObject.SetActive(true);

        //}
        //else if (p_tutorialReminder == "critHits")
        //{
        //    currentTutorialIndex = 3;
        //    reminderFramesContainer.sprite = popUps[currentTutorialIndex].panels[currentIndex];

        //    reminderFramesContainer.gameObject.SetActive(true);

        //}
        //else if (p_tutorialReminder == "food")
        //{
        //    currentTutorialIndex = 4;
        //    reminderFramesContainer.sprite = popUps[currentTutorialIndex].panels[currentIndex];

        //    reminderFramesContainer.gameObject.SetActive(true);
        //}
        else
        {
            reminderFramesContainer.gameObject.SetActive(false);
        }
    }
}