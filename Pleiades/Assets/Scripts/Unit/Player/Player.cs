using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : Unit
{
    public GameObject weapon;
    public HealthBar healthBar;
    public Transform startingPos;
    public GameObject moveTipPanel;

    private void Awake()
    {
        foreach (Slot currentSlot in gameObject.GetComponents<Slot>())
        {
            if (currentSlot.item.type == ItemType.Equipment)
            {
                Debug.Log("WA");
                currentSlot.OnEquip += EquipElement;
            }

        }
    }
    public void EquipElement(Slot p_slot)
    {
        if (p_slot.item.type == ItemType.Equipment)
        {
            GemSO gem = p_slot.item as GemSO;
            currentElementalType.elementalType = gem.elementalType;

        }
        
    }
    void Start()
    {

        //Init();
        //StartCoroutine(CheckIfMoved());
    }

    void Init()
    {
        this.maxHp = 100;
        this.curHp = 100;
        this.atkDmg = 50;
        healthBar.SetMaxHealth(maxHp);
    }

    IEnumerator CheckIfMoved()
    {
        Debug.Log("Started CIM");
        yield return new WaitForSeconds(15f);

        Debug.Log("Wait over");
        //if(transform.position == startingPos.transform.position)
        //{
        //if (!Gem.instance.blueGemGet) {
        //    moveTipPanel.SetActive(true);
        //    //d}
        //}

        yield return new WaitForSeconds(6f);

        //if (transform.position != startingPos.transform.position)
        //{
            moveTipPanel.SetActive(false);
        //}
    }

    public void TakeDamage(float damage)
    {
        AudioManager.Instance.playerHit.Play();
        curHp -= damage;

        healthBar.SetHealth(curHp);

        if(curHp <= 0)
        {
            OnDeath();
        }
    }

    public void AddHealth(int value)
    {
        curHp += value;
        healthBar.SetHealth(curHp);
        AudioManager.Instance.hpItemGet.Play();
        if (curHp >= 100)
        {
            curHp = 100;
        }

    }

    public void OnDeath()
    {
        RespawnWaypoint.Instance.Respawn(RespawnWaypoint.Instance.currentWaypoint);

        //show Retry, Exit panel
    }
}
