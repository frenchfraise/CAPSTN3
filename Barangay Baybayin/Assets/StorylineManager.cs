using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class WorldEvent : UnityEvent<string, int, int> { };

[System.Serializable]
public class QuestCompletedEvent : UnityEvent<int,int,int> { };

[System.Serializable]
public class StorylineData
{
    [SerializeField] public SO_StoryLine so_StoryLine;
    public int currentQuestChainIndex;
    public int currentQuestLineIndex;


}
public class StorylineManager : MonoBehaviour
{
    public static WorldEvent onWorldEvent = new WorldEvent();
    public static QuestCompletedEvent onQuestCompletedEvent = new QuestCompletedEvent();
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
    [NonReorderable][SerializeField] public List<StorylineData> storyLines;
    private void Awake()
    {

       
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static int GetStorylineIndexFromID(string p_ID)
    {
        if (p_ID == "Q-LP")
        {
            return 0;
        }
        else  if (p_ID == "Q-KS") //temp
        {
            return 1;
        }
        else if (p_ID  == "Q-TA")
        {
            return 2;
        }
        else if (p_ID == "Q-BC") //temp
        {
            return 3;
        }
        return 0;
    }

    public static StorylineData GetStorylineDataFromID(string p_ID)
    {
        int index = StorylineManager.GetStorylineIndexFromID(p_ID);
        return StorylineManager.instance.storyLines[index];

    }
}
