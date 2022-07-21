using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyInteract : Interactables
{
    public Enemy enemy;
    public int indexNo;

    public Action<int> OnEnemyDeath;

    void Start()
    {
        OnEnemyDeath += OnDeath;
    }

    private void OnDisable()
    {
        OnEnemyDeath -= OnDeath;
    }

    void Update()
    {
        
    }

    void OnDeath(int index)
    {
        interactedWith = true;
        PuzzleManager.instance.OnItemInteracted(indexNo);
    }
}
