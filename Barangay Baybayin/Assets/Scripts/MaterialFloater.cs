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
        StartCoroutine(FadeTimer(projectileDecayTime));
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
   
    public void Update()
    {
        transform.Translate(new Vector2(0, 1) * 1f * Time.deltaTime);
    }

    public IEnumerator FadeTimer(float decayTime)
    {
        float count = decayTime;
        float rate = (decayTime * 10f) / decayTime * 0.01f; // (1.5 * 10) / 1.5 * 0.01
        count = 1f;

        for (float i = 1; i >= 0.1f; i -= 0.1f)
        {
        
            yield return new WaitForSeconds(0.1f);
            //Color newColor = image.material.color;
            //newColor.a = i;
            //Debug.Log("FADING: " + rate + " CURRENT " + i + " newColor " + newColor);
            ////image.sprite.color = newColor;
            //image.material.color = newColor;// = newColor;


            Color textMeshProNewColor = textMeshPro.color;
            textMeshProNewColor.a = i;
            textMeshPro.color = textMeshProNewColor;


        }
    }
    public IEnumerator DecayTimer(float decayTime)
    {
        float count = decayTime;
   
        while (count > 0)
        {
            yield return new WaitForSeconds(0.1f);
            count-=0.1f;
        }
    

        //for (float i = 1; i >= 0.1f; i -= 0.1f)
        //{
        //    Color newColor = this.gameObject.GetComponent<SpriteRenderer>().material.color;
        //    newColor.a = i;
        //    this.gameObject.GetComponent<SpriteRenderer>().material.color = newColor;
        //    yield return new WaitForSeconds(0.1f);
        //}
        // ProjectilePool.instance.ReturnToPool(this);
        //CoinPool.instance.ReturnToPool(this);
        Destroy(gameObject);
    }
}
