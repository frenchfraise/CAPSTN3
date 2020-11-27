using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    public float FIRE_BASE_SPEED = 1.0f;

    public Animator animator;
    public GameObject crosshair;
    public GameObject lightningAim;

    public GameObject firePrefab;
    public GameObject[] lightningPrefabs = new GameObject[4];
    public Transform lightning;

    public GameObject movement;

    void Awake()
    {
        movement = GameObject.FindWithTag("Player");
    }

    public void Attack()
    {
        StartCoroutine(StopMoving(.5f));
        StartCoroutine(AttackCo());
        Slash();
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

    IEnumerator StopMoving(float value)
    {
        movement.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(value);
        movement.GetComponent<PlayerMovement>().enabled = true;
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
        StartCoroutine(StopMoving(.3f));
        Debug.Log("player lightning!");
        Vector2 shootingDirection = lightningAim.transform.localPosition;
        shootingDirection.Normalize();

        GameObject thunder = Instantiate(lightningPrefabs[Random.Range(0, lightningPrefabs.Length)],
                                         lightning);
        thunder.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
        Destroy(thunder, .3f);
    }
}
