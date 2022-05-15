using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthOverheadUI : PoolableObject
{
    private bool isRevealed;
    [SerializeField] private float unrevealTimeOut;
    Coroutine currentTimeOut;
    private Camera cam;
    [SerializeField] private Vector2 positionCorrection = new Vector2(0, 40);
    private RectTransform targetCanvas;
    private RectTransform healthBar;
    private Transform objectToFollow;

    public void OnHealthDied()
    {
        if (currentTimeOut != null)
        {
            StopCoroutine(currentTimeOut);
        }
        
        isRevealed = false;
        healthBar.gameObject.SetActive(false);
        genericObjectPool.pool.Release(this);
        
       
    }

    public IEnumerator Co_RevealTimeOut()
    {
        yield return new WaitForSeconds(unrevealTimeOut);
        healthBar.gameObject.SetActive(false);
        isRevealed = false;
    }

    public void SetHealthBarData(Transform p_targetTransform, RectTransform p_healthBarPanel)
    {
        this.targetCanvas = p_healthBarPanel;
        healthBar = transform.GetChild(0).GetComponent<RectTransform>();
        objectToFollow = p_targetTransform;
        healthBar.gameObject.SetActive(false);
        transform.SetParent(p_healthBarPanel, false);

    }
    public void OnHealthChanged(Health p_healthFill)
    {
        if (p_healthFill.isAlive)
        {
            if (!isRevealed)
            {

                RepositionHealthBar();
                healthBar.gameObject.SetActive(true);
                isRevealed = true;

            }
            healthBar.GetChild(0).GetComponent<Image>().fillAmount = p_healthFill.currentHealth / p_healthFill.maxHealth;
            if (currentTimeOut != null)
            {
                StopCoroutine(currentTimeOut);
            }
            currentTimeOut = StartCoroutine(Co_RevealTimeOut()); 
        }
       

    }

    private void Start()
    {
        cam = cam ? cam : Camera.main;
        
    }

   
    private void RepositionHealthBar()
    {
        Vector2 ViewportPosition = cam.WorldToViewportPoint(objectToFollow.position);

        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * targetCanvas.sizeDelta.x)- (targetCanvas.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f)));

        WorldObject_ScreenPosition += new Vector2(positionCorrection.x, positionCorrection.y);

        healthBar.anchoredPosition = WorldObject_ScreenPosition;

    }

}
