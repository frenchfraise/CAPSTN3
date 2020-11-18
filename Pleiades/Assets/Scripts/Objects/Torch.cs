using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Interactables
{
    public Sprite firstSpr;
    public Sprite secondSpr;
    public Sprite unlit;
    private SpriteRenderer rend;

    void Start()
    {
        rend = this.GetComponent<SpriteRenderer>();
        //interactedWith = true;

        StartCoroutine(Alternate());
    }


    void Update()
    {
        
    }

    public void LightThis()
    {
        interactedWith = true;

        ChangeSprite();
    }

    void ChangeSprite()
    {
        if (rend.sprite != firstSpr)
        {
            rend.sprite = firstSpr;
        }
        else
        {
            rend.sprite = secondSpr;
        }
    }

    IEnumerator Alternate()
    {
       while (interactedWith == true)
        {
            ChangeSprite();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
