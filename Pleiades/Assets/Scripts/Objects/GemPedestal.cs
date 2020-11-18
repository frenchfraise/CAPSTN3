using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPedestal : MonoBehaviour
{
    public Sprite noGemVar;
    private SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rend.sprite = noGemVar;

            //unlock appropriate skill
            Debug.Log("Skill unlock!");
        }
    }
}
