using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeToolUI : MonoBehaviour
{
    public int toolIndex;
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField] public Image isUpgradableImage;

    [SerializeField] public Image hasRecipeImage;
    [SerializeField] public TMP_Text proficiencyText;

    private void Start()
    {
        nameText.text = ToolManager.instance.tools[toolIndex].so_Tool.name;
        iconImage.sprite = ToolManager.instance.tools[toolIndex].so_Tool.unlockedIcon;
        UpdateUI();
    }

    public void UpdateUI()
    {

        bool isUpgradableReqOne = false;
        bool isUpgradableReqTwo = false;
        Tool selectedTool = ToolManager.instance.tools[toolIndex];
        CraftUpgradeItemRequirementsData craftUpgradeItemRequirementsData = selectedTool.so_Tool.craftUpgradeItemRequirementsDatas[selectedTool.craftLevel-1];

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
                hasRecipeImage.sprite = UIManager.instance.upgradeUI.sufficient;
                isUpgradableReqOne = true;
            }
            else if (InventoryManager.GetItem(itemUpgradeRequirement.so_Item).amount < itemUpgradeRequirement.requiredAmount)
            {
                //hasRecipeImage.color = new Color32(255, 0, 0, 225);
                hasRecipeImage.sprite = UIManager.instance.upgradeUI.insufficient;
                
            }
        }
        else
        {
            isUpgradableReqOne = true;
           // hasRecipeImage.color = new Color32(0, 255, 0, 225);
            hasRecipeImage.sprite = UIManager.instance.upgradeUI.sufficient;
           // isUpgradableImage.color = new Color32(0, 255, 0, 225);
            isUpgradableImage.sprite = UIManager.instance.upgradeUI.sufficient;
        }
            
        
        

      
       
        

        if (isUpgradableReqOne && isUpgradableReqTwo)
        {
            if (craftUpgradeItemRequirementsData.itemRequirements.Count > 1)
            {
                ItemUpgradeRequirement materialTwo = craftUpgradeItemRequirementsData.itemRequirements[1];

                if (InventoryManager.GetItem(materialTwo.so_Item).amount >= materialTwo.requiredAmount)
                {
                    isUpgradableImage.sprite = UIManager.instance.upgradeUI.sufficient;
                   // isUpgradableImage.color = new Color32(0, 255, 0, 225);
                }
                else if (InventoryManager.GetItem(materialTwo.so_Item).amount < materialTwo.requiredAmount)
                {
                    isUpgradableImage.sprite = UIManager.instance.upgradeUI.insufficient;
                   // isUpgradableImage.color = new Color32(255, 0, 0, 225);
                }

            }
        }
        else
        {
            isUpgradableImage.sprite = UIManager.instance.upgradeUI.insufficient;
        }
        
    }
    
}
