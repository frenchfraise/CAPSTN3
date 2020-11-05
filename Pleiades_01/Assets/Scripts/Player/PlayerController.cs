using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    public float MOVEMENT_BASE_SPEED = 1.0f;
    public float FIRE_BASE_SPEED = 1.0f;
    public float CROSSHAIR_DISTANCE = 1.0f;
    public float LIGHTNING_DISTANCE = 5f;
    public Vector2 movementDirection;
    public float movementSpeed;
    public bool isMoving;

    public Rigidbody2D rb;

    public Animator animator;
    public GameObject crosshair;
    public GameObject lightningAim;

    public GameObject firePrefab;
    public GameObject[] lightningPrefabs = new GameObject[4];
    public Transform lightning;

    void Update()
    {
        ProcessInputs();
        Move();
        Animate();
        Aim();
        LightningAim();
    }

    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
    }

    void Move()
    {
        rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
    }

    void Animate()
    {
        if(movementDirection != Vector2.zero)
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

    public void Attack()
    {
        StartCoroutine(AttackCo());
        Debug.Log("Coroutine Attack start");
    }

    public IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.25f);
    }

    public void Slash()
    {
        Debug.Log("player slash!");
    }

    public void Shoot()
    {
        Debug.Log("player fire!");
        Vector2 shootingDirection = crosshair.transform.localPosition;
        shootingDirection.Normalize();

        GameObject arrow = Instantiate(firePrefab, transform.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * FIRE_BASE_SPEED;
        arrow.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
        Destroy(arrow, 2.0f);
    }

    public void Lightning()
    {
        Debug.Log("player lightning!");
        Vector2 shootingDirection = lightningAim.transform.localPosition;
        shootingDirection.Normalize();

        GameObject thunder = Instantiate(lightningPrefabs[Random.Range(0, lightningPrefabs.Length)],
                                         lightning);
        thunder.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
        Destroy(thunder, .35f);
    }
}
