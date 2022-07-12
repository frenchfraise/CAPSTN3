using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeToolUI : MonoBehaviour
{

    public UpgradeToolsUI upgradeUI { private get; set; }
    public int toolIndex;
    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private Image nameFrameImage;
    [SerializeField] public Image isUpgradableImage;

    [SerializeField] public Image hasRecipeImage;
    [SerializeField] public TMP_Text proficiencyText;
    [SerializeField]
    private Image proficiencyFrameImage;

    private void Start()
    {
     
        UpdateUI();
    }

    public void UpdateUI()
    {
        Tool tool = ToolManager.instance.tools[toolIndex];
        nameText.text = tool.so_Tool.name[1];
        iconImage.sprite = tool.so_Tool.equippedIcon[tool.craftLevel];
        nameFrameImage.sprite = upgradeUI.plates[tool.craftLevel];
        proficiencyFrameImage.sprite = upgradeUI.plates[tool.craftLevel];
        bool isUpgradableReqOne = false;

        bool isUpgradableReqTwo = false;
        Tool selectedTool = ToolManager.instance.tools[toolIndex];
        CraftUpgradeItemRequirementsData craftUpgradeItemRequirementsData = selectedTool.so_Tool.craftUpgradeItemRequirementsDatas[selectedTool.craftLevel];

        proficiencyText.text = craftUpgradeItemRequirementsData.requiredProficiencyLevel.ToString();
        if (selectedTool.proficiencyLevel >= craftUpgradeItemRequirementsData.requiredProficiencyLevel)
        {
            isUpgradableReqTwo = true;
        }
        if (craftUpgradeItemRequirementsData.itemRequirements.Count > 0)
        {
            ItemUpgradeRequirement itemUpgradeRequirement = craftUpgradeItemRequirementsData.itemRequirements[0];
            if (InventoryManager.GetItem(itemUpgradeRequirement.so_Item).amount >= itemUpgradeRequirement.requiredAmount)
            {
               // hasRecipeImage.color = new Color32(0, 255, 0, 225);
                hasRecipeImage.sprite = upgradeUI.sufficientIcon;
                isUpgradableReqOne = true;
            }
            else if (InventoryManager.GetItem(itemUpgradeRequirement.so_Item).amount < itemUpgradeRequirement.requiredAmount)
            {
                //hasRecipeImage.color = new Color32(255, 0, 0, 225);
                hasRecipeImage.sprite = upgradeUI.insufficientIcon;
                
            }
        }
        else
        {
            isUpgradableReqOne = true;
           // hasRecipeImage.color = new Color32(0, 255, 0, 225);
            hasRecipeImage.sprite = upgradeUI.sufficientIcon;
           // isUpgradableImage.color = new Color32(0, 255, 0, 225);
            isUpgradableImage.sprite = upgradeUI.sufficientIcon;
        }

        if (isUpgradableReqOne && isUpgradableReqTwo)
        {
            if (craftUpgradeItemRequirementsData.itemRequirements.Count > 1)
            {
                ItemUpgradeRequirement materialTwo = craftUpgradeItemRequirementsData.itemRequirements[1];

                if (InventoryManager.GetItem(materialTwo.so_Item).amount >= materialTwo.requiredAmount)
                {
                    isUpgradableImage.sprite = upgradeUI.sufficientIcon;
                   // isUpgradableImage.color = new Color32(0, 255, 0, 225);
                }
                else if (InventoryManager.GetItem(materialTwo.so_Item).amount < materialTwo.requiredAmount)
                {
                    isUpgradableImage.sprite = upgradeUI.insufficientIcon;
                   // isUpgradableImage.color = new Color32(255, 0, 0, 225);
                }

            }
        }
        else
        {
            isUpgradableImage.sprite = upgradeUI.insufficientIcon;
        }
        
    }
    
}
