using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject pointer;

    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void Start()
    {
        pointer.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointer.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointer.SetActive(false);
    }
}
