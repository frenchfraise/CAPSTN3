using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialFloater : MonoBehaviour
{
    public SpriteRenderer image;
    public TextMeshPro textMeshPro;
    public float projectileDecayTime = 1.5f;
    public void InitializeValues(SO_Item p_SOItem, string p_text, Vector3 p_playerPosition)
    {
        image.sprite = p_SOItem.icon;
        textMeshPro.color = p_SOItem.color;
        textMeshPro.text = "+"+ p_text;
        transform.position = p_playerPosition;
    }
    private void OnEnable()
    {
        StartCoroutine(DecayTimer(projectileDecayTime));
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
   
    public void Update()
    {
        transform.Translate(new Vector2(0, 1) * 1f * Time.deltaTime);
    }
    public IEnumerator DecayTimer(float decayTime)
    {
        float count = decayTime;
        while (count > 0)
        {
            yield return new WaitForSeconds(1);
            count--;
        }


        // ProjectilePool.instance.ReturnToPool(this);
        //CoinPool.instance.ReturnToPool(this);
        Destroy(gameObject);
    }
}
