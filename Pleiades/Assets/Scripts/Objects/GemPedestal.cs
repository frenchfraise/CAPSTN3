using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPedestal : MonoBehaviour
{
    public Sprite noGemVar;
    private SpriteRenderer rend;
    public int number;

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
            GameManager.Instance.gem.GemGet(number);
            GameManager.Instance.skills.SkillEnable(number);
            //unlock appropriate skill
            Debug.Log("Skill unlock!");
        }
    }
}
