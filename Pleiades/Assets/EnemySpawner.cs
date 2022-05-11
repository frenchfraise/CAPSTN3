using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemySpawner : MonoBehaviour
{
    public float delayTime;
    public float spawnRate;
    public int currentAmount; 
    [SerializeField] int amount; // IF YO USET THIS ABOVE 100, IT'LL BE INFINITELY SPAWNING
    public GameObject enemyPrefab;

    public event Action OnFinishedSpawning;
    private void OnEnable()
    {
        currentAmount = amount;
        RequireFinishEvent requireEvent = GetComponent<RequireFinishEvent>();
        requireEvent.OnStartRequirement += StartSpawning;
       // OnFinishedSpawning += requireEvent.RequirementCleared;

    }

    private void OnDisable()
    {
        currentAmount = amount;
        GetComponent<RequireFinishEvent>().OnStartRequirement -= StartSpawning;
        StopAllCoroutines();
    }

    IEnumerator Co_SpawnerStartUp()
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(Co_SpawnObject());
    }

    IEnumerator Co_SpawnObject()
    {
        currentAmount--;
        GameObject newEnemy = Instantiate(enemyPrefab,transform.position,Quaternion.identity);
        newEnemy.GetComponent<ObjectRequirement>().Spawned(GetComponent<ObjectRequirement>().requirer);
        Debug.Log("SPAWNED REQUIERER SET");
        yield return new WaitForSeconds(spawnRate);
        if (amount > 100 ||
            currentAmount > 0)
        {
            StartCoroutine(Co_SpawnObject());
        }
        else
        {

            OnFinishedSpawning.Invoke();
            FinishedSpawning();
        }
    }
    public void StartSpawning()
    {
        Debug.Log("RARA");
        StartCoroutine(Co_SpawnerStartUp());
    }
    public void FinishedSpawning()
    {
       // GetComponent<RequireFinishEvent>().RequirementCleared();
    }
}
