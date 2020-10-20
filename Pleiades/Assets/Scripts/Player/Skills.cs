using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [Header("Skill 1")]
    public Image skillImage1;
    public Image letter1;
    public float cooldown1 = 5;
    bool isCooldown1 = false;
    public KeyCode skill1;

    [Header("Skill 2")]
    public Image skillImage2;
    public Image letter2;
    public float cooldown2 = 12;
    bool isCooldown2 = false;
    public KeyCode skill2;

    [Header("Skill 3")]
    public Image skillImage3;
    public Image letter3;
    public float cooldown3 = 20;
    bool isCooldown3 = false;
    public KeyCode skill3;

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
    }

    // Update is called once per frame
    void Update()
    {
        Skill1();
        Skill2();
        Skill3();
        if(Pause.GameIsPaused == true)
        {
            skills.SetActive(false);
        }
        if(Pause.GameIsPaused == false)
        {
            skills.SetActive(true);
        }
    }

    void Skill1()
    {
        if(Input.GetKey(skill1) && isCooldown1 == false)
        {
            isCooldown1 = true;
            skillImage1.fillAmount = 1;
            letter1.fillAmount = 1;
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
        if (Input.GetKey(skill2) && isCooldown2 == false)
        {
            isCooldown2 = true;
            skillImage2.fillAmount = 1;
            letter2.fillAmount = 1;
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
        if (Input.GetKey(skill3) && isCooldown3 == false)
        {
            isCooldown3 = true;
            skillImage3.fillAmount = 1;
            letter3.fillAmount = 1;
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
