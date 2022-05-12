using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;
    public List<Infrastructure> buildings = new List<Infrastructure>();
    private void Awake()
    {
        instance = this;

    }

    
}
