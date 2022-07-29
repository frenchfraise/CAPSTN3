using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.Threading.Tasks;
public class TitlescreenUI : MonoBehaviour
{
    public GameObject creditsScreen;

    private void Start()
    {
        //TransitionUI.onFadeTransition.Invoke(0);
    }
    //public async void OnPlayButtonUIClicked()
    //{

    //    Task te = TransitionUI.onFadeTransition.Invoke(1);
    //    await te;

    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    //}

    public void OnPlayButtonUIClicked()
    {
        StartCoroutine(Co_OnPlayButtonUIClicked());
    }

    IEnumerator Co_OnPlayButtonUIClicked()
    {
        TransitionUI.onFadeTransition.Invoke(1);
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnCreditsButtonClicked(bool p_bool)
    {
        creditsScreen.SetActive(p_bool);
    }

    public void OnQuitButtonUIClicked()
    {
        Debug.Log("Game quit!");
        Application.Quit();
    }

}
