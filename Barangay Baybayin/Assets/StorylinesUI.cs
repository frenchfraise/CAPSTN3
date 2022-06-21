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

    public StorylinesUIClose onStorylinesUIClose = new StorylinesUIClose();

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
            int currentStorylineIndex = storylineData.currentStorylineIndex;
            int currentQuestlinePartIndex = storylineData.currentQuestlinePartIndex;
            if (currentStorylineIndex < so_StoryLine.questLines.Count)
            {
                SO_Questline so_Questline = so_StoryLine.questLines[currentStorylineIndex];
                if (currentQuestlinePartIndex < so_Questline.questlineData.Count)
                {
                    QuestlineData questlineData = so_Questline.questlineData[currentQuestlinePartIndex];

                    characterNameText.text = so_StoryLine.name;//.text = so_StoryLine.questLines[currentCharacterDataIndex].quest.title;
                    icon.sprite = questlineData.quest.questImage;
                    counterText.text = "QUEST " + (index + 1).ToString();
                    descriptionText.text = questlineData.quest.description;


                    List<QuestRequirement> requirements = questlineData.quest.requirements;

                    for (int i = 0; i < requirements.Count;)
                    {
                        ItemUI newObject = Instantiate(prefab, requirementsContainer);
                        requirementsUIs.Add(newObject);
                        if (requirements[i].so_requirement is SO_ItemRequirement)
                        {

                            SO_ItemRequirement so_ItemRequirement = requirements[i].so_requirement as SO_ItemRequirement;
                            for (int ii = 0; ii < so_ItemRequirement.so_Item.Count; i++)
                            {
                                newObject.InitializeValues("", so_ItemRequirement.requiredAmount.ToString(), so_ItemRequirement.so_Item[ii].icon);
                            }
                           

                        }
                        else if (requirements[i].so_requirement is SO_InfrastructureRequirement)
                        {
                            SO_InfrastructureRequirement so_ItemRequirement = requirements[i].so_requirement as SO_InfrastructureRequirement;
                            newObject.InitializeValues("", so_ItemRequirement.requiredLevel.ToString(), so_ItemRequirement.so_infrastructure.sprites[so_ItemRequirement.requiredLevel]);

                        }
                        i++;

                    }
                    List<ItemReward> rewards = so_StoryLine.questLines[currentStorylineIndex].questlineData[currentQuestlinePartIndex].quest.rewards;
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
        int currentStorylineIndex = storylineData.currentStorylineIndex;
        int currentQuestlinePartIndex = storylineData.currentQuestlinePartIndex;
        storylines[index].questFrame.sprite = frameLevels[currentStorylineIndex];
        storylines[index].titleText.text = so_StoryLine.name; //so_StoryLine.questLines[currentCharacterDataIndex].quest.title;
        storylines[index].questCountText.text = "QUEST " + currentStorylineIndex;//so_StoryLine.questLines[currentCharacterDataIndex].quest.description;
        storylines[index].icon.sprite = so_StoryLine.questLines[currentStorylineIndex].questlineData[currentQuestlinePartIndex].quest.questImage;
        List<ItemReward> rewards = so_StoryLine.questLines[currentStorylineIndex].questlineData[currentQuestlinePartIndex].quest.rewards;
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
        onStorylinesUIClose.Invoke(true);
    }

    public void OpenButtonUIClicked()
    {
        selectionPanel.SetActive(true);
        selectedPanel.SetActive(false);
        gameObject.SetActive(true);
        onStorylinesUIClose.Invoke(false);
    }
}
