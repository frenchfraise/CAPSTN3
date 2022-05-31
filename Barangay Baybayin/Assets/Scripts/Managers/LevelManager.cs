using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class LevelManager : MonoBehaviour
{

    private static LevelManager _instance;
    public static LevelManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelManager>();
            }

            return _instance;

        }
    }

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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AsyncLoadScene("UI", OnUILoaded));
        StartCoroutine(AsyncLoadScene("World", OnWorldLoaded));
    }

    public void OnUILoaded()
    {

    }
    public void OnWorldLoaded()
    {





    }

    public void StartGame() //try again (I want to rename this)
    {

    }
    public void OnRestartGame()
    {



    }

    IEnumerator AsyncLoadScene(string name, UnityAction onCallBack = null)
    {
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        while (!asyncLoadScene.isDone)
        {
            // loading bar =  asyncLoadScene.progress

            yield return null;
        }
        if (onCallBack != null)
            onCallBack.Invoke();
    }

    IEnumerator UnLoadScene(string name, UnityAction onCallBack = null)
    {
        AsyncOperation unasych = SceneManager.UnloadSceneAsync(name);

        while (!unasych.isDone)
        {

            yield return null;
        }
        if (onCallBack != null)
            onCallBack.Invoke();
    }





}
