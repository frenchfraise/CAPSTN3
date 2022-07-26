using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class StorylinesUIClose : UnityEvent<bool> { }
public class StorylinesUI : MonoBehaviour
{
    [SerializeField] private GameObject frame;
    [NonReorderable]
    [SerializeField]
    private List<Sprite> frameLevels;
    public ItemUI prefab;
    public ItemUI innerprefab;
    public List<StorylineUI> storylines = new List<StorylineUI>();

    public GameObject selectionPanel;
    public GameObject selectedPanel;

    public Sprite unseenFrame;
    public Sprite seenFrame;

    public TMP_Text characterNameText;
    public Image icon;
    public TMP_Text counterText;
    public TMP_Text descriptionText;

    public RectTransform requirementsContainer;
    public RectTransform rewardsContainer;

    public List<ItemUI> requirementsUIs;
    public List<ItemUI> rewardsUIs;

    public Sprite hammer;

    public int currentIndex = 0;

    public TMP_Text amountQuestCompleted;

    public Image questFrame;
    private void Awake()
    {
        StorylineManager.onFirstTimeStorylineEndedEvent.AddListener(FirstTimeStorylineEndedEvent);
        StorylineManager.onLastTimeStoryline.AddListener(LastTimeStorylineEvent);
    }
    void LastTimeStorylineEvent(int p_int)
    {
        storylines[p_int].isFinished = true;
        UpdateStoryLineUI(p_int);
    }
    void FirstTimeStorylineEndedEvent(int p_int)
    {
        storylines[p_int].isSeen = true;
        UpdateStoryLineUI(p_int);
    }
    private void Start()
    {
        for (int i = 0; i < storylines.Count; i++)
        {

            UpdateStoryLineUI(i);
        }
    }
    public void UpdateSelectedStoryLineUI(int index)
    {
        if (storylines[index].isFinished)
        {
            storylines[index].completed.SetActive(true);
            storylines[index].seenFrame.SetActive(true);
        }
        else
        {
            currentIndex = index;
            for (int i = 0; i < requirementsUIs.Count; i++)
            {
                requirementsUIs[i].DeinitializeValues();
                Destroy(requirementsUIs[i].gameObject);

            }
            for (int i = 0; i < rewardsUIs.Count; i++)
            {
                rewardsUIs[i].DeinitializeValues();
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



                        if (requirements[0].so_requirement is SO_ItemRequirement)
                        {

                            SO_ItemRequirement so_ItemRequirement = requirements[0].so_requirement as SO_ItemRequirement;
                            for (int ii = 0; ii < so_ItemRequirement.so_Item.Count; ii++)
                            {
                                ItemUI newObject = Instantiate(innerprefab, requirementsContainer);
                                requirementsUIs.Add(newObject);
                                Debug.Log(InventoryManager.GetItem(so_ItemRequirement.so_Item[ii].name.ToString()).amount.ToString());
                                string combi = InventoryManager.GetItem(so_ItemRequirement.so_Item[ii].name.ToString()).amount.ToString() + " / " + so_ItemRequirement.requiredAmount[ii].ToString();
                                newObject.InitializeValues("", combi, so_ItemRequirement.so_Item[ii].icon);
                            }


                        }


                        else
                        {
                            SO_InfrastructureRequirement infra = so_QuestLine.questlineData[1].quest.requirements[0].so_requirement as SO_InfrastructureRequirement;
                            ItemUI newObject = Instantiate(prefab, storylines[index].reqcontainer);
                            storylines[index].requiredItemUIs.Add(newObject);

                            //newObject.InitializeValues("", rewards[i].amount.ToString(), rewards[i].so_Item.icon);
                            newObject.itemIconImage.sprite = infra.so_infrastructure.sprites[0];
                            newObject.frameRectTransform.gameObject.SetActive(false);
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
            
       
       
    }

    public void UpdateStoryLineUI(int index)
    {
        amountQuestCompleted.text = "Quests Completed: " + StorylineManager.instance.amountQuestComplete.ToString();
        if (storylines[index].isFinished)
        {
            storylines[index].completed.SetActive(true);
            storylines[index].seenFrame.SetActive(true);
        }
        else
        {
            
            for (int i = 0; i < storylines[index].itemUIs.Count; i++)
            {
                storylines[index].itemUIs[i].DeinitializeValues();
                Destroy(storylines[index].itemUIs[i].gameObject);
            }

            for (int i = 0; i < storylines[index].requiredItemUIs.Count; i++)
            {
                storylines[index].requiredItemUIs[i].DeinitializeValues();
                Destroy(storylines[index].requiredItemUIs[i].gameObject);
            }
            if (storylines[index].isSeen)
            {
                storylines[index].seenFrame.SetActive(false);
                storylines[index].thisFrame.sprite = seenFrame;
            }
            else
            {
                storylines[index].seenFrame.SetActive(true);
                storylines[index].thisFrame.sprite = unseenFrame;
            }
            storylines[index].itemUIs.Clear();
            storylines[index].requiredItemUIs.Clear();
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

            List<ItemReward> rewards = so_QuestLine.questlineData[so_QuestLine.questlineData.Count - 1].quest.rewards;
            for (int i = 0; i < rewards.Count;)
            {
                ItemUI newObject = Instantiate(prefab, storylines[index].container);
                storylines[index].itemUIs.Add(newObject);

                //newObject.InitializeValues("", rewards[i].amount.ToString(), rewards[i].so_Item.icon);
                newObject.itemAmountText.text = rewards[i].amount.ToString();
                newObject.itemIconImage.sprite = rewards[i].so_Item.icon;
                //Debug.Log(index + " - INDEEEX " + i + " () "+rewards[i].amount.ToString());
                i++;

            }
            SO_ItemRequirement ir = so_QuestLine.questlineData[0].quest.requirements[0].so_requirement as SO_ItemRequirement;
            List<SO_Item> requirements = ir.so_Item;
            if (ir != null)
            {
                for (int i = 0; i < requirements.Count;)
                {
                    ItemUI newObject = Instantiate(prefab, storylines[index].reqcontainer);
                    storylines[index].requiredItemUIs.Add(newObject);

                    //newObject.InitializeValues("", rewards[i].amount.ToString(), rewards[i].so_Item.icon);
                    newObject.itemAmountText.text = ir.requiredAmount[i].ToString();
                    newObject.itemIconImage.sprite = requirements[i].icon;
                    //Debug.Log(index + " - INDEEEX " + i + " () "+rewards[i].amount.ToString());
                    i++;

                }
            }
            else
            {
                SO_InfrastructureRequirement infra = so_QuestLine.questlineData[1].quest.requirements[0].so_requirement as SO_InfrastructureRequirement;
                ItemUI newObject = Instantiate(prefab, storylines[index].reqcontainer);
                storylines[index].requiredItemUIs.Add(newObject);

                //newObject.InitializeValues("", rewards[i].amount.ToString(), rewards[i].so_Item.icon);
                newObject.itemIconImage.sprite = infra.so_infrastructure.sprites[0];
                newObject.frameRectTransform.gameObject.SetActive(false);
            }
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
        frame.SetActive(false);
        //gameObject.SetActive(false);
        UIManager.onGameplayModeChangedEvent.Invoke(false);
       // TimeManager.onPauseGameTime.Invoke(true);
    }

    public void OpenButtonUIClicked()
    {
        frame.SetActive(true);
        //TimeManager.onPauseGameTime.Invoke(false);
        for (int i = 0; i < storylines.Count; i++)
        {

            UpdateStoryLineUI(i);
        }
        selectionPanel.SetActive(true);
        selectedPanel.SetActive(false);
        //gameObject.SetActive(true);
        UIManager.onGameplayModeChangedEvent.Invoke(true);
    }
}
