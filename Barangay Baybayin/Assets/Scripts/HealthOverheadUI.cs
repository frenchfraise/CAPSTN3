using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HealthOverheadUI : PoolableObject
{
    private bool isRevealed;
    [SerializeField] private float unrevealTimeOut;
    Coroutine currentTimeOut;
    private Camera cam;
    [SerializeField] private Vector2 positionCorrection = new Vector2(0, 40);
    private RectTransform targetCanvas;
    private RectTransform healthBarTransform;
    [SerializeField] private Image healthFrame;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image delayedBar;
    private Transform objectToFollow;

    float fill;

    public void OnHealthDied()
    {
        if (currentTimeOut != null)
        {
            StopCoroutine(currentTimeOut);
        }
        
        isRevealed = false;
        healthFrame.gameObject.SetActive(false);
        genericObjectPool.pool.Release(this);
        
       
    }

    public IEnumerator Co_RevealTimeOut()
    {
        yield return new WaitForSeconds(unrevealTimeOut);
        healthFrame.gameObject.SetActive(false);
        isRevealed = false;
    }

    public void SetHealthBarData(Transform p_targetTransform, RectTransform p_healthBarPanel)
    {
        this.targetCanvas = p_healthBarPanel;
        healthBarTransform = GetComponent<RectTransform>();
        objectToFollow = p_targetTransform;
        healthFrame.gameObject.SetActive(false);
        transform.SetParent(p_healthBarPanel, false);

    }
    public void OnHealthChanged(Health p_healthFill)
    {
        if (p_healthFill.isAlive)
        {

            if (!isRevealed)
            {

                RepositionHealthBar();
                healthFrame.gameObject.SetActive(true);
                isRevealed = true;

            }

            fill = p_healthFill.currentHealth / p_healthFill.maxHealth;
            
          
            StartCoroutine(Co_Test());
            



            if (currentTimeOut != null)
            {
                StopCoroutine(currentTimeOut);
            }
            currentTimeOut = StartCoroutine(Co_RevealTimeOut()); 
        }
       

    }
  

    IEnumerator Co_Test()
    {
        //white
        healthBar.color = new Color(255, 255, 255);

        //black
        Tween WhiteToBlack = healthBar.DOColor(new Color(0, 0, 0), 0.05f);
        yield return WhiteToBlack.WaitForCompletion();

        Tween BlackToRed = healthBar.DOColor(new Color(255, 0, 0), 0.05f);
        yield return BlackToRed.WaitForCompletion();

        healthBar.color = new Color(0, 248, 0);//reset
        healthBar.fillAmount = fill;
        yield return new WaitForSeconds(1f);

        Sequence s = DOTween.Sequence();
        s.Join(delayedBar.DOFade(0f, 0.35f));
        s.Join(delayedBar.DOFillAmount(fill, 0.35f));
        s.Play();
        yield return s.WaitForCompletion();
        delayedBar.color = new Color32(250, 255, 255, 255);//reset
        //delayedBar.DOFade(1f, 0.01f); //reset
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

        healthBarTransform.anchoredPosition = WorldObject_ScreenPosition;

    }

}
