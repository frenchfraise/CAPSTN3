using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPedestal : MonoBehaviour
{
    public Sprite noGemVar;
    private SpriteRenderer rend; 
    public Animator animator;
    public int number;
    public bool gemGotten;

    void Start()
    {
        gemGotten = false;
        rend = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine("Gem");
            //rend.sprite = noGemVar;
            //GameManager.Instance.gem.GemGet(number);
            //GameManager.Instance.skills.SkillEnable(number);

            if(!gemGotten)
            { 
                AudioManager.Instance.gemGet.Play();
                gemGotten = true;
            }
            //unlock appropriate skill
            Debug.Log("Skill unlock!");
        }
    }

    private void OnTriggetExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            animator.SetBool("GemGet", false);
        }
    }

    public IEnumerator Gem()
    {
        animator.SetBool("GemGet", true);
        yield return null;
        animator.SetBool("GemGet", false);
    }
}
