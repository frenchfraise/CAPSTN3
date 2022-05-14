using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfrastructureManager : MonoBehaviour
{
    public static InfrastructureManager instance;
    public List<Infrastructure> buildings = new List<Infrastructure>();
    private void Awake()
    {
        instance = this;

    }
}
