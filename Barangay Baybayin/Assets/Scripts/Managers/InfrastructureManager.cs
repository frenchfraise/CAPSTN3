using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfrastructureManager : MonoBehaviour
{
    private static InfrastructureManager _instance;
    public static InfrastructureManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InfrastructureManager>();
            }

            return _instance;

        }
    }
    public List<Infrastructure> infrastructures = new List<Infrastructure>();

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    public static Infrastructure GetInfrastructure(SO_Infrastructure p_infrastructure)
    {


        for (int i = 0; i < InfrastructureManager.instance.infrastructures.Count;)
        {
            if (InfrastructureManager.instance.infrastructures[i].so_Infrastructure == p_infrastructure)
            {
                return InfrastructureManager.instance.infrastructures[i];
            }
            i++;
            if (i >= InfrastructureManager.instance.infrastructures.Count)
            {
                //Loop finished but didnt find any matching item
                Debug.Log("FAILED TO FIND INFRASTRUCTURE " + p_infrastructure.name + " BECAUSE COULD NOT FIND SO_Infrastructure IN Infrastructure Manager WITH MATCHING NAME");
            }
        }
                
            
        
        return null;

    }
}
