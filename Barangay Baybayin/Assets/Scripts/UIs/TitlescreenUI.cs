using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitlescreenUI : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("TEST");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
