using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class WorldEvent : UnityEvent { };

[System.Serializable]
public class StorylineData
{
    [SerializeField] public SO_StoryLine so_StoryLine;
    public int currentCharacterDataIndex;
}
public class StorylineManager : MonoBehaviour
{
    public static WorldEvent worldEvent = new WorldEvent();
    private static StorylineManager _instance;
    public static StorylineManager instance

        
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<StorylineManager>();
            }

            return _instance;
        }
    }
    [SerializeField] public List<StorylineData> storyLines;
    private void Awake()
    {

       
        _instance = this;
        DontDestroyOnLoad(gameObject);

        
    }
}
