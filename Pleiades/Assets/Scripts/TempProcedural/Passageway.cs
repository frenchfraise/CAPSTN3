using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Passageway : MonoBehaviour
{
    public Passageway connectedTo;
    public bool isConnectedToAnotherPassageway = false;
    [SerializeField] public CardinalDirection cardinalDirection;
    private bool isHorizontalPassageway = false;
    private bool explored = false;

    private Vector2 originalDirection;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    public Action OnFirstTimeEntered;


    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (cardinalDirection == CardinalDirection.North || 
            cardinalDirection == CardinalDirection.South || 
            cardinalDirection == CardinalDirection.Center || 
            cardinalDirection == CardinalDirection.None)
        {
            isHorizontalPassageway = true;
        }
        else
        {
            isHorizontalPassageway = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            originalDirection = (transform.position - collision.transform.position); //Destination - Origin
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Vector2 latestDirection = (collision.transform.position - transform.position);

            float originalChosenAxis = 0;
            float latestChosenAxis = 0;
          
            if (isHorizontalPassageway)//Decide if vertical/horizontal axis direction will it compare
            {
                latestChosenAxis = latestDirection.x;
                originalChosenAxis = originalDirection.x;
            }
            else
            {
                latestChosenAxis = latestDirection.y;
                originalChosenAxis = originalDirection.y;
            }

            //Compare if player proceeded with the same direction as he started with
            if (Mathf.Sign(latestChosenAxis) == Mathf.Sign(originalChosenAxis)) //If he is on the same direction as he started, it means he entered
            {
                explored = true;
                OnFirstTimeEntered.Invoke();
            }

        }
    }

    public void Open()
    {
        spriteRenderer.enabled = false;
        boxCollider.isTrigger = true;
    }

    public void Close()
    {
        spriteRenderer.enabled = true;
        boxCollider.isTrigger = false;
    }
}
