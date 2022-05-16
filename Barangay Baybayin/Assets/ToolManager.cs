using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager instance;
    public List<Tool> tools = new List<Tool>();

    private void Awake()
    {
        instance = this;
    }

}
