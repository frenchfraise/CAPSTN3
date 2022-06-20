using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SpecialPointsModifiedEvent : UnityEvent<float, float> { }

public class SpecialPointsFilledEvent : UnityEvent { }

public class ProficiencyAmountModifiedEvent : UnityEvent<float,float> { }
public class ProficiencyLevelModifiedEvent : UnityEvent<int> { }

public class ToolUpgradedEvent : UnityEvent { }

public class ToolChangedEvent : UnityEvent <Tool>{ }
public class ToolManager : MonoBehaviour
{
    private static ToolManager _instance;
    public static ToolManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ToolManager>();
            }

            return _instance;
        }
    }
    [NonReorderable] public List<Tool> tools = new List<Tool>();
    //Proficiency
    public static ProficiencyAmountModifiedEvent onProficiencyAmountModifiedEvent = new ProficiencyAmountModifiedEvent();
    public static ProficiencyLevelModifiedEvent onProficiencyLevelModifiedEvent = new ProficiencyLevelModifiedEvent();

    //Special Points
    public static SpecialPointsModifiedEvent onSpecialPointsModifiedEvent = new SpecialPointsModifiedEvent();
    public static SpecialPointsFilledEvent onSpecialPointsFilledEvent = new SpecialPointsFilledEvent();


    public static ToolChangedEvent onToolChangedEvent = new ToolChangedEvent();
    public static ToolUpgradedEvent onToolUpgradedEvent  = new ToolUpgradedEvent();

    private void Awake()
    {
        //if (_instance != null)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
            _instance = this;
            DontDestroyOnLoad(gameObject);
        //}
    }

    public static void ResetAllSpecials()
    {
        foreach (Tool to in ToolManager.instance.tools)
        {
            to.ModifySpecialAmount(-to.so_Tool.maxSpecialPoints[to.craftLevel-1]);
        }
    }
}
