using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MOVEMENT_BASE_SPEED;
    public float CROSSHAIR_DISTANCE;
    public float LIGHTNING_DISTANCE;
    public Vector2 movementDirection;
    public float movementSpeed;
    public bool isMoving;
    bool lockHorizontal;
    bool lockVertical;

    public Rigidbody2D rb;

    public Animator animator;
    public GameObject crosshair;
    public GameObject lightningAim;

    void Awake()
    {
        lockHorizontal = false;
        lockVertical = false;
        MOVEMENT_BASE_SPEED = 8.0f;
        CROSSHAIR_DISTANCE = 1.0f;
        LIGHTNING_DISTANCE = 7.5f;
    }

    void Update()
    {
        ProcessInputs();
        Move();
        Animate();
        Aim();
        LightningAim();
    }

    void FixedUpdate()
    {

    }

    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(movementDirection.x > 0 || movementDirection.x < 0)
        {
            movementDirection.y = 0;
        }

        if(movementDirection.y > 0 || movementDirection.y < 0)
        {
            movementDirection.x = 0;
        }

        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
    }

    void Move()
    {
        //rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
        //rb.velocity = movementDirection * MOVEMENT_BASE_SPEED;
        rb.MovePosition(rb.position + movementDirection * MOVEMENT_BASE_SPEED * Time.fixedDeltaTime);
    }

    void Animate()
    {
        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }

        animator.SetFloat("Speed", movementSpeed);
    }

    void Aim()
    {
        if (movementDirection != Vector2.zero)
        {
            crosshair.transform.localPosition = movementDirection * CROSSHAIR_DISTANCE;
        }
    }

    void LightningAim()
    {
        if (movementDirection != Vector2.zero)
        {
            lightningAim.transform.localPosition = movementDirection * LIGHTNING_DISTANCE;
        }
    }
}
