using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class MaterialFloater : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private float decayTime = 1.5f;
    [SerializeField] private float delayTime = 1.5f;
    //[SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Vector3 targetMovePosition;
    public void InitializeValues(SO_Item p_SOItem, string p_text, Vector3 p_playerPosition)
    {
        image.sprite = p_SOItem.icon;
        textMeshPro.color = p_SOItem.color;
        textMeshPro.text = "+"+ p_text;
        transform.position = p_playerPosition;// +offsetPosition;
    }
    private void OnEnable()
    {
        StartCoroutine(DecayTimer(delayTime,decayTime));

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
   
    //public void Update()
    //{
    //    transform.Translate(new Vector2(0, 1) * 1f * Time.deltaTime);
    //}

    public IEnumerator DecayTimer(float p_delayTime, float p_decayTime)
    {

        transform.DOMoveY(targetMovePosition.y, p_decayTime + p_delayTime);
        yield return new WaitForSeconds(p_delayTime);
        var sequence = DOTween.Sequence()
        .Append(image.DOFade(0, p_decayTime));
        sequence.Append(textMeshPro.DOFade(0, p_decayTime));
        sequence.Play();
        yield return sequence.WaitForCompletion();



        Destroy(gameObject);
    }
}
