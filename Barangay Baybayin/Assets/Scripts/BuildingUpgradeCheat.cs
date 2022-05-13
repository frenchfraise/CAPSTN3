using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpgradeCheat : MonoBehaviour
{
    public void Cheat()
    {
        if (BuildingManager.instance.buildings[0].currentLevel < BuildingManager.instance.buildings[0].sprites.Count)
        {
            BuildingManager.instance.buildings[0].currentLevel++;
            int toAdd = BuildingManager.instance.buildings[0].currentLevel - 1;
            Sprite ns = BuildingManager.instance.buildings[0].sprites[toAdd];
            BuildingManager.instance.buildings[0].GetComponent<SpriteRenderer>().sprite = ns;

        }


    }
}
