using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


public class UpgradeToolsUI : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanelUI;

    [NonReorderable] [SerializeField] public List<Sprite> plates = new List<Sprite>(); // test

    [SerializeField] private GameObject confirmPanelUI;

    [SerializeField] private UpgradePreviewToolUI currentUpgradePreviewToolUI;
    [SerializeField] private UpgradePreviewToolUI nextUpgradePreviewToolUI;

    [SerializeField] public Sprite sufficientIcon;
    [SerializeField] public Sprite insufficientIcon;

    [SerializeField] public Sprite sufficientFrame;
    [SerializeField] public Sprite insufficientFrame;

    [SerializeField] private List<UpgradeMaterialUI> upgradeMaterialUIs;

    private int currentIndex;

    private bool threeRequirement = false;

    [SerializeField]
    private List<UpgradeToolUI> upgradeToolUIs = new List<UpgradeToolUI>();


    private void Awake()
    {
        foreach (UpgradeToolUI upgradeToolUI in upgradeToolUIs)
        {
            upgradeToolUI.upgradeUI = this;
        }
    }

    private void OnEnable()
    {
        Panday.onPandaySpokenToEvent.AddListener(OpenButtonUIClicked);
    }

    private void OnDisable()
    {
        Panday.onPandaySpokenToEvent.RemoveListener(OpenButtonUIClicked);
    }
    public void ToolButtonUIClicked(int p_toolIndex)
    {
        upgradeMaterialUIs[0].requirementFulfilled = false;
        upgradeMaterialUIs[1].requirementFulfilled = false;
        threeRequirement = false;
        currentIndex = p_toolIndex;
        confirmPanelUI.SetActive(true);
        selectionPanelUI.SetActive(false);
        UpdateCurrentToolUI(ToolManager.instance.tools[currentIndex]);
    }

    void UpdateCurrentToolUI(Tool p_currentTool)
    {
        SO_Tool currentSOTool = p_currentTool.so_Tool;
        int currentToolLevel = p_currentTool.craftLevel;
        int upgradeToolLevel = currentToolLevel+1;
        Debug.Log(currentToolLevel + " + " + upgradeToolLevel);
        currentUpgradePreviewToolUI.nameText.text = currentSOTool.name[p_currentTool.craftLevel];
        currentUpgradePreviewToolUI.iconImage.sprite = currentSOTool.equippedIcon[p_currentTool.craftLevel];
        currentUpgradePreviewToolUI.frame.sprite = plates[p_currentTool.craftLevel];

        nextUpgradePreviewToolUI.nameText.text = currentSOTool.name[upgradeToolLevel];
        nextUpgradePreviewToolUI.iconImage.sprite = currentSOTool.equippedIcon[upgradeToolLevel];
        nextUpgradePreviewToolUI.frame.sprite = plates[upgradeToolLevel];

        CraftUpgradeItemRequirementsData craftUpgradeItemRequirementsData = currentSOTool.craftUpgradeItemRequirementsDatas[currentToolLevel];
        if (p_currentTool.proficiencyLevel >= craftUpgradeItemRequirementsData.requiredProficiencyLevel)
        {
            threeRequirement = true;
        }
  
        for (int i = 0; i < upgradeMaterialUIs.Count; i++)
        {
            upgradeMaterialUIs[i].isInUse = false;
        }
        if (craftUpgradeItemRequirementsData.itemRequirements.Count > 0)
        {
            for (int i = 0; i < craftUpgradeItemRequirementsData.itemRequirements.Count; i++)
            {
                
                ItemUpgradeRequirement materialRequirement = craftUpgradeItemRequirementsData.itemRequirements[i];
                int amt = InventoryManager.GetItem(materialRequirement.so_Item).amount;
                upgradeMaterialUIs[i].iconImage.sprite = materialRequirement.so_Item.icon;
                upgradeMaterialUIs[i].gameObject.SetActive(true);
                upgradeMaterialUIs[i].so_item = materialRequirement.so_Item;
                upgradeMaterialUIs[i].isInUse = true;
                upgradeMaterialUIs[i].amountRequired = craftUpgradeItemRequirementsData.itemRequirements[i].requiredAmount;
                upgradeMaterialUIs[i].currentAmountText.text = amt.ToString();
                upgradeMaterialUIs[i].maxAmountText.text = upgradeMaterialUIs[i].amountRequired.ToString();

                upgradeMaterialUIs[i].iconImage.gameObject.SetActive(true);
                if (amt >= upgradeMaterialUIs[i].amountRequired)
                {
                    upgradeMaterialUIs[i].requirementFulfilled = true;
                }
            }
        }
        for (int i = 0; i < upgradeMaterialUIs.Count; i++)
        {
            if (upgradeMaterialUIs[i].isInUse == false)
            {
                upgradeMaterialUIs[i].iconImage.gameObject.SetActive(false);
                upgradeMaterialUIs[i].requirementFulfilled = true;
            }
        }
   
    }


    public void ToolUpgradeButtonUIClicked()
    {
     
        if (ToolManager.instance.tools[currentIndex].craftLevel < ToolManager.instance.tools[currentIndex].so_Tool.maxCraftLevel)
        {
            Debug.Log("TEST " + upgradeMaterialUIs[0].requirementFulfilled + " - " + upgradeMaterialUIs[1].requirementFulfilled + " - "+ threeRequirement + " - " + ToolManager.instance.tools[currentIndex].craftLevel);
         
            if (upgradeMaterialUIs[0].requirementFulfilled && upgradeMaterialUIs[1].requirementFulfilled && threeRequirement)
            {
                for (int i = 0; i < upgradeMaterialUIs.Count; i++)
                {
                    if (upgradeMaterialUIs[i].requirementFulfilled)
                    {
                        InventoryManager.ReduceItem(upgradeMaterialUIs[i].so_item, upgradeMaterialUIs[i].amountRequired);
                    }
                }
                
                ToolManager.instance.tools[currentIndex].craftLevel++;
                UpgradeToolUI currentlyUpgradedToolUI = upgradeToolUIs[currentIndex];
                currentlyUpgradedToolUI.UpdateUI();
                ToolManager.onToolUpgradedEvent.Invoke();
                selectionPanelUI.SetActive(true);
                confirmPanelUI.SetActive(false);
            }
            
            
        }
     

    }

    public void BackButtonUIClicked()
    {
        
        foreach(UpgradeToolUI upgradeToolUI in upgradeToolUIs)
        {
            upgradeToolUI.UpdateUI();
        }    
        selectionPanelUI.SetActive(true);
        confirmPanelUI.SetActive(false);

    }

    public void QuitButtonUIClicked()
    {
        confirmPanelUI.SetActive(false);
        selectionPanelUI.SetActive(false);
        UIManager.onGameplayModeChangedEvent.Invoke(false);
        TimeManager.onPauseGameTime.Invoke(true);
    }

    public void OpenButtonUIClicked()
    {
        TimeManager.onPauseGameTime.Invoke(false);
        foreach (UpgradeToolUI upgradeToolUI in upgradeToolUIs)
        {
            upgradeToolUI.UpdateUI();
        }
        selectionPanelUI.SetActive(true);
        confirmPanelUI.SetActive(false);
        
        UIManager.onGameplayModeChangedEvent.Invoke(true);
    }
}
