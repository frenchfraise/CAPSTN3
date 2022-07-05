using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class StorylinesUIClose : UnityEvent<bool> { }
public class StorylinesUI : MonoBehaviour
{
    [NonReorderable]
    [SerializeField]
    private List<Sprite> frameLevels;
    public ItemUI prefab;
    public List<StorylineUI> storylines = new List<StorylineUI>();

    public GameObject selectionPanel;
    public GameObject selectedPanel;

    public TMP_Text characterNameText;
    public Image icon;
    public TMP_Text counterText;
    public TMP_Text descriptionText;

    public RectTransform requirementsContainer;
    public RectTransform rewardsContainer;

    public List<ItemUI> requirementsUIs;
    public List<ItemUI> rewardsUIs;

    public int currentIndex = 0;

    public Image questFrame;
    private void Start()
    {
        for (int i = 0; i < storylines.Count; i++)
        {

            UpdateStoryLineUI(i);
        }
    }
    public void UpdateSelectedStoryLineUI(int index)
    {
        currentIndex = index;
        for (int i = 0; i < requirementsUIs.Count; i++)
        {
            Destroy(requirementsUIs[i].gameObject);
            
        }
        for (int i = 0; i < rewardsUIs.Count; i++)
        {
            Destroy(rewardsUIs[i].gameObject);
        }
        requirementsUIs.Clear();
        rewardsUIs.Clear();

        if (index < StorylineManager.instance.storyLines.Count)
        {
            StorylineData storylineData = StorylineManager.instance.storyLines[index];
            SO_StoryLine so_StoryLine = storylineData.so_StoryLine;
            int currentStorylineIndex = storylineData.currentQuestChainIndex;
            int currentQuestlinePartIndex = storylineData.currentQuestLineIndex;
            SO_Questline so_QuestLine = so_StoryLine.questLines[currentStorylineIndex];
            if (currentStorylineIndex < so_StoryLine.questLines.Count)
            {
              
                if (currentQuestlinePartIndex < so_QuestLine.questlineData.Count)
                {
                    QuestlineData questlineData = so_QuestLine.questlineData[currentQuestlinePartIndex];

                    characterNameText.text = so_StoryLine.character.name.ToString();//.text = so_StoryLine.questLines[currentCharacterDataIndex].quest.title;
                    icon.sprite = questlineData.quest.questImage;
                    counterText.text = "QUEST " + (currentStorylineIndex + 1).ToString() + " : " + questlineData.quest.title.ToString();
                    questFrame.sprite = frameLevels[currentStorylineIndex];
                    descriptionText.text = questlineData.quest.description;

                    List<QuestRequirement> requirements = so_QuestLine.questlineData[0].quest.requirements;
                    for (int i = 0; i < requirements.Count;)
                    {

                        if (requirements[i].so_requirement is SO_ItemRequirement)
                        {

                            SO_ItemRequirement so_ItemRequirement = requirements[i].so_requirement as SO_ItemRequirement;
                            for (int ii = 0; ii < so_ItemRequirement.so_Item.Count; ii++)
                            {
                                ItemUI newObject = Instantiate(prefab, requirementsContainer);
                                requirementsUIs.Add(newObject);
                                newObject.InitializeValues("", so_ItemRequirement.requiredAmount[ii].ToString(), so_ItemRequirement.so_Item[ii].icon);
                            }


                        }
                      
                        i++;

                    }

                    List<ItemReward> rewards = so_QuestLine.questlineData[so_QuestLine.questlineData.Count - 1].quest.rewards;
                    for (int i = 0; i < rewards.Count;)
                    {
         
                        ItemUI newObject = Instantiate(prefab, rewardsContainer);
                        rewardsUIs.Add(newObject);
                        newObject.InitializeValues("", rewards[i].amount.ToString(), rewards[i].so_Item.icon);
                        i++;

                    }
                    selectionPanel.SetActive(false);
                    selectedPanel.SetActive(true);
                }
                
            }
        }
       
       
    }

    public void UpdateStoryLineUI(int index)
    {
        for (int i = 0; i < storylines[index].itemUIs.Count; i++)
        {
            Destroy(storylines[index].itemUIs[i].gameObject);
        }
        storylines[index].itemUIs.Clear();
        StorylineData storylineData = StorylineManager.instance.storyLines[index];
        SO_StoryLine so_StoryLine = storylineData.so_StoryLine;
        
        int currentStorylineIndex = storylineData.currentQuestChainIndex;
        int currentQuestlinePartIndex = storylineData.currentQuestLineIndex;
        SO_Questline so_QuestLine = so_StoryLine.questLines[currentStorylineIndex];
        QuestlineData questLineData = so_QuestLine.questlineData[currentQuestlinePartIndex];
        storylines[index].questFrame.sprite = frameLevels[currentStorylineIndex];
        storylines[index].titleText.text = so_StoryLine.character.name.ToString();// so_StoryLine.name; //so_StoryLine.questLines[currentCharacterDataIndex].quest.title;
        storylines[index].questCountText.text = "QUEST " + (currentStorylineIndex + 1).ToString() + " : " + questLineData.quest.title.ToString();//so_StoryLine.questLines[currentCharacterDataIndex].quest.description;
        storylines[index].icon.sprite = so_QuestLine.questlineData[currentQuestlinePartIndex].quest.questImage;
        List<ItemReward> rewards = so_QuestLine.questlineData[so_QuestLine.questlineData.Count -1].quest.rewards;
        for (int i = 0; i < rewards.Count;)
        {
            ItemUI newObject = Instantiate(prefab,storylines[index].container);
            storylines[index].itemUIs.Add(newObject);
            newObject.InitializeValues("", rewards[i].amount.ToString(), rewards[i].so_Item.icon);
            i++;
            
        }

    }

    public void LeftButtonUIClicked()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = storylines.Count -1;
        }
     
        UpdateSelectedStoryLineUI(currentIndex);

    }

    public void RightButtonUIClicked()
    {
        currentIndex++;
        if (currentIndex > storylines.Count - 1)
        {
            currentIndex = 0;
        }
      
        UpdateSelectedStoryLineUI(currentIndex);
    }

    public void BackButtonUIClicked()
    {
        
        selectionPanel.SetActive(true);
        selectedPanel.SetActive(false);

    }

    public void QuitButtonUIClicked()
    {
        gameObject.SetActive(false);
        UIManager.onGameplayModeChangedEvent.Invoke(false);
       // TimeManager.onPauseGameTime.Invoke(true);
    }

    public void OpenButtonUIClicked()
    {
        //TimeManager.onPauseGameTime.Invoke(false);
        for (int i = 0; i < storylines.Count; i++)
        {

            UpdateStoryLineUI(i);
        }
        selectionPanel.SetActive(true);
        selectedPanel.SetActive(false);
        gameObject.SetActive(true);
        UIManager.onGameplayModeChangedEvent.Invoke(true);
    }
}
