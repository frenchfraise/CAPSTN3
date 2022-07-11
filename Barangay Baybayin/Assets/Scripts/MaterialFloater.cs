using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialFloater : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private float decayTime = 1.5f;
    [SerializeField] private float delayTime = 1.5f;
    public void InitializeValues(SO_Item p_SOItem, string p_text, Vector3 p_playerPosition)
    {
        image.sprite = p_SOItem.icon;
        textMeshPro.color = p_SOItem.color;
        textMeshPro.text = "+"+ p_text;
        transform.position = p_playerPosition;
    }
    private void OnEnable()
    {
        StartCoroutine(DecayTimer(delayTime,decayTime));

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
   
    public void Update()
    {
        transform.Translate(new Vector2(0, 1) * 1f * Time.deltaTime);
    }

    public IEnumerator DecayTimer(float p_delayTime, float p_decayTime)
    {
        float rate = (p_decayTime * 10f) / p_decayTime * 0.01f; // (1.5 * 10) / 1.5 * 0.01
        yield return new WaitForSeconds(p_delayTime);

        for (float i = 1; i >= 0.1f; i -= 0.1f)
        {
            Color newColor = image.material.color;
            newColor.a = i;
            image.material.color = newColor;


            Color textMeshProNewColor = textMeshPro.color;
            textMeshProNewColor.a = i;
            textMeshPro.color = textMeshProNewColor;
            yield return new WaitForSeconds(0.1f);
       


        }

         
    
        Destroy(gameObject);
    }
}
