using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SpecialPointsModified : UnityEvent<float, float> { }

public class SpecialPointsFilled : UnityEvent { }

public class ProficiencyAmountModified : UnityEvent<float,float> { }
public class ProficiencyLevelModified : UnityEvent<int> { }

public class ToolUpgraded : UnityEvent { }

public class ToolChanged : UnityEvent <Tool>{ }
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
    public List<Tool> tools = new List<Tool>();
    //Proficiency
    public static ProficiencyAmountModified onProficiencyAmountModified = new ProficiencyAmountModified();
    public static ProficiencyLevelModified onProficiencyLevelModified = new ProficiencyLevelModified();

    //Special Points
    public static SpecialPointsModified onSpecialPointsModified = new SpecialPointsModified();
    public static SpecialPointsFilled onSpecialPointsFilled = new SpecialPointsFilled();


    public static ToolChanged onToolChanged = new ToolChanged();
    public static ToolUpgraded onToolUpgraded  = new ToolUpgraded();

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void ResetAllSpeicals()
    {
        foreach (Tool to in ToolManager.instance.tools)
        {
            to.ModifySpecialAmount(-to.so_Tool.maxSpecialPoints[to.craftLevel]);
        }
    }
}
