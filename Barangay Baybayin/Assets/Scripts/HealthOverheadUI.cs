using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthOverheadUI : MonoBehaviour
{

    private Camera cam;

    [SerializeField] private Vector2 positionCorrection = new Vector2(0, 40);
    private RectTransform targetCanvas;
    private RectTransform healthBar;
    private Transform objectToFollow;

    public void OnHealthDied()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);

    }


    public void SetHealthBarData(Transform p_targetTransform, RectTransform p_healthBarPanel)
    {
        this.targetCanvas = p_healthBarPanel;
        healthBar = GetComponent<RectTransform>();
        objectToFollow = p_targetTransform;
        healthBar.gameObject.SetActive(true);
    }
    public void OnHealthChanged(Health p_healthFill)
    {
        if (!gameObject.activeSelf)
        {
            //gameObject.SetActive(true);
        }
        healthBar.GetChild(0).GetComponent<Image>().fillAmount = p_healthFill.currentHealth/p_healthFill.maxHealth;
    }

    private void Start()
    {
        cam = cam ? cam : Camera.main;
        RepositionHealthBar();
    }

    private void RepositionHealthBar()
    {
        Vector2 ViewportPosition = cam.WorldToViewportPoint(objectToFollow.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f)));

        WorldObject_ScreenPosition += new Vector2(positionCorrection.x, positionCorrection.y);

        healthBar.anchoredPosition = WorldObject_ScreenPosition;

    }

}
