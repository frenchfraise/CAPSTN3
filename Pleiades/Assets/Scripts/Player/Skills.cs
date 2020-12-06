﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [Header("Skill 1")]
    public Image skillImage1;
    public float cooldown1;
    public bool isCooldown1 = false;
    public bool hasPlayedAudio = false;
    public GameObject swordSkill;
    bool swordSkillGet;

    [Header("Skill 2")]
    public Image skillImage2;
    public float cooldown2;
    public bool isCooldown2 = false;
    public GameObject fireSkill;
    bool fireSkillGet;

    [Header("Skill 3")]
    public Image skillImage3;
    public float cooldown3;
    public bool isCooldown3 = false;
    public GameObject sparkSkill;
    bool sparkSkillGet;

    public GameObject skills;
    public GameObject panel;
    int skillEnable;

    // Start is called before the first frame update
    void Start()
    {
        skillImage1.fillAmount = 0;
        skillImage2.fillAmount = 0;
        skillImage3.fillAmount = 0;
        cooldown1 = 1.5f;
        cooldown2 = 2;
        cooldown3 = 2.5f;
        swordSkillGet = false;
        fireSkillGet = false;
        sparkSkillGet = false;
    }

    // Update is called once per frame
    void Update()
    {
        Skill1();
        Skill2();
        Skill3();
    }

    public void SkillEnable(int skillEnable)
    {
        if(skillEnable == 1)
        {
            swordSkillGet = true;
            panel.SetActive(true);
            swordSkill.SetActive(true);
        }
        else if (skillEnable == 2)
        {
            fireSkillGet = true;
            fireSkill.SetActive(true);
        }
        else if (skillEnable == 3)
        {
            sparkSkillGet = true;
            sparkSkill.SetActive(true);
        }
    }

    void Skill1()
    {
        if (Input.GetKeyDown(KeyCode.J) && isCooldown1 == false && swordSkillGet == true)
        {
            isCooldown1 = true;
            skillImage1.fillAmount = 1;
            GameManager.Instance.playerController.Attack();

            if (!hasPlayedAudio)
            { 
                AudioManager.Instance.slash.Play();
                hasPlayedAudio = true;
            }
        }

        if(isCooldown1)
        {
            skillImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (skillImage1.fillAmount <= 0)
            {
                skillImage1.fillAmount = 0;
                isCooldown1 = false;
                hasPlayedAudio = false;
            }

        }
    }

    void Skill2()
    {
        if (Input.GetKeyDown(KeyCode.K) && isCooldown2 == false && fireSkillGet == true)
        {
            isCooldown2 = true;
            skillImage2.fillAmount = 1;
            GameManager.Instance.playerController.Shoot();
            AudioManager.Instance.fireBall.Play();
        }

        if (isCooldown2)
        {
            skillImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            if (skillImage2.fillAmount <= 0)
            {
                skillImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }

    void Skill3()
    {
        if (Input.GetKeyDown(KeyCode.L) && isCooldown3 == false && sparkSkillGet == true)
        {
            isCooldown3 = true;
            skillImage3.fillAmount = 1;
            GameManager.Instance.playerController.Lightning();
            AudioManager.Instance.lightning.Play();
        }

        if (isCooldown3)
        {
            skillImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

            if (skillImage3.fillAmount <= 0)
            {
                skillImage3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }
}
