using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [Header("Skill 1")]
    public Image skillImage1;
    public Image letter1;
    public float cooldown1 = 2;
    public bool isCooldown1 = false;

    [Header("Skill 2")]
    public Image skillImage2;
    public Image letter2;
    public float cooldown2 = 3;
    public bool isCooldown2 = false;

    [Header("Skill 3")]
    public Image skillImage3;
    public Image letter3;
    public float cooldown3 = 5;
    public bool isCooldown3 = false;

    public GameObject skills;

    // Start is called before the first frame update
    void Start()
    {
        skillImage1.fillAmount = 0;
        letter1.fillAmount = 0;
        skillImage2.fillAmount = 0;
        letter2.fillAmount = 0;
        skillImage3.fillAmount = 0;
        letter3.fillAmount = 0;
        cooldown1 = 2;
        cooldown2 = 3;
        cooldown3 = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Skill1();
        Skill2();
        Skill3();
    }

    void Skill1()
    {
        if(Input.GetKeyDown(KeyCode.Z) && isCooldown1 == false)
        {
            isCooldown1 = true;
            skillImage1.fillAmount = 1;
            letter1.fillAmount = 1;
            GameManager.Instance.playerController.Slash();
            GameManager.Instance.playerController.Attack();
        }

        if(isCooldown1)
        {
            skillImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            letter1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (skillImage1.fillAmount <= 0)
            {
                skillImage1.fillAmount = 0;
                letter1.fillAmount = 0;
                isCooldown1 = false;
            }

        }
    }

    void Skill2()
    {
        if (Input.GetKeyDown(KeyCode.X) && isCooldown2 == false)
        {
            isCooldown2 = true;
            skillImage2.fillAmount = 1;
            letter2.fillAmount = 1;
            GameManager.Instance.playerController.Shoot();
        }

        if (isCooldown2)
        {
            skillImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            letter2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            if (skillImage2.fillAmount <= 0)
            {
                skillImage2.fillAmount = 0;
                letter2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }

    void Skill3()
    {
        if (Input.GetKeyDown(KeyCode.C) && isCooldown3 == false)
        {
            isCooldown3 = true;
            skillImage3.fillAmount = 1;
            letter3.fillAmount = 1;
            GameManager.Instance.playerController.Lightning();
        }

        if (isCooldown3)
        {
            skillImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;
            letter3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

            if (skillImage3.fillAmount <= 0)
            {
                skillImage3.fillAmount = 0;
                letter3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }
}
