using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : ElementalMonument
{
    public Animator anim;
   
    private SpriteRenderer spriteRenderer;
    public int indexNo;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision) //delete
    {
        OnMonumentActivated.Invoke();
        //anim.SetTrigger("");
    }
}
