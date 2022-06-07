using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystick : MonoBehaviour
{
    [SerializeField] private float speed;
    public Joystick joystick;
    private Rigidbody2D rb;
    private Vector2 movement;

    public Animator animator;

    [SerializeField] public Transform aim;
    [SerializeField] private float aimOffset;
    private void Start()
    {
        rb = rb ? rb : GetComponent<Rigidbody2D>();
        aim.position = (Vector2)transform.position + (aimOffset * movement);        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        if (movement != Vector2.zero)
        {
            aim.position = (Vector2)transform.position + (aimOffset * movement);
        }
        
    }
}
