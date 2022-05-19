using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ExpIncrease : UnityEvent<float,float> { }
public class ExpLevelIncrease : UnityEvent<int> { }
public class ExpLevelExpIncrease : UnityEvent<float, float> { }
public class ToolChanged : UnityEvent <Tool>{ }
public class ToolManager : MonoBehaviour
{
    public static ToolManager instance;
    public List<Tool> tools = new List<Tool>();
    public static ExpLevelExpIncrease OnExpLevelExpIncrease = new ExpLevelExpIncrease();
    public static ExpIncrease OnExpIncrease = new ExpIncrease();
    public static ExpLevelIncrease OnExpLevelIncrease = new ExpLevelIncrease();
    public static ToolChanged OnToolChanged = new ToolChanged();
    private void Awake()
    {
        instance = this;
    }

 
}
