using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviour
{
    public Animator animator;
    public GameObject tablet;
    public GameObject panel;
    public Sprite lit;
    public int tabletNumber;

    bool isReading;

    void Update()
    {
        if(isReading == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = lit; 
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            animator.SetBool("ReadingDone", false);
            StartCoroutine("ReadingCo");
            isReading = true;
            panel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        StopCoroutine("DoneReadingCo");
        animator.SetBool("ReadingDone", true);
        isReading = false;
        panel.SetActive(false);
    }

    public IEnumerator ReadingCo()
    {
        animator.SetBool("Reading", true);
        yield return null;
        animator.SetBool("Reading", false);
    }

}
