using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infrastructure : InteractibleObject
{
    [SerializeField] public SO_Infrastructure so_Infrastructure;
    private int currentHealth;
    private SpriteRenderer sr;
    [SerializeField] public int currentLevel = 1;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        currentHealth = 0;
    }

    protected override void OnInteract()
    {
        if (canInteract)
        {
            
            currentHealth++;
            
            if (currentHealth >= so_Infrastructure.maxHealth)
            {
                if (currentLevel >= so_Infrastructure.sprites.Count)
                {
                    Debug.Log("MAX LEVEL REACHED");
                }
                else
                {
                    currentLevel++;
                    currentHealth = 0;
                    sr.sprite = so_Infrastructure.sprites[currentLevel-1];
                }
                

            }
           
        
        }

    }
}
