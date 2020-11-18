using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gem : MonoBehaviour
{
    public bool blueGemIsActive;
    public bool redGemIsActive;
    public bool yellowGemIsActive;

    public Transform blueGem;
    public Transform redGem;
    public Transform yellowGem;

    public Image blueGemImage;
    public Image redGemImage;
    public Image yellowGemImage;

    public Image blueGemLetter;
    public Image redGemLetter;
    public Image yellowGemLetter;

    Vector3 temp1;
    Vector3 temp2;
    Vector3 temp3;

    public GameObject gems;

    // Start is called before the first frame update
    void Start()
    {
        blueGemIsActive = true;
        redGemIsActive = false;
        yellowGemIsActive = false;

        blueGemImage.fillAmount = 0;
        blueGemLetter.fillAmount = 0;

        temp1 = blueGem.localScale;
        temp1.x = 2;
        temp1.y = 2;
        blueGem.localScale = temp1;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("a"))
        {
            BlueGemEquip();
        }
        if (Input.GetKeyDown("s"))
        {
            RedGemEquip();
        }
        if (Input.GetKeyDown("d"))
        {
            YellowGemEquip();
        }

    }

    public void BlueGemEquip()
    {
        blueGemIsActive = true;
        blueGemImage.fillAmount = 0;
        blueGemLetter.fillAmount = 0;
        temp1 = blueGem.localScale;
        temp1.x = 2;
        temp1.y = 2;

        blueGem.localScale = temp1;
        RedGemUnequip();
        YellowGemUnequip();
    }

    public void BlueGemUnequip()
    {
        blueGemIsActive = false;
        blueGemImage.fillAmount = 1;
        blueGemLetter.fillAmount = 1;
        temp1 = blueGem.localScale;
        temp1.x = 1;
        temp1.y = 1;

        blueGem.localScale = temp1;
    }

    public void RedGemEquip()
    {
        redGemIsActive = true;
        redGemImage.fillAmount = 0;
        redGemLetter.fillAmount = 0;
        temp2 = redGem.localScale;
        temp2.x = 2;
        temp2.y = 2;

        redGem.localScale = temp2;
        BlueGemUnequip();
        YellowGemUnequip();
    }

    public void RedGemUnequip()
    {
        redGemIsActive = false;
        redGemImage.fillAmount = 1;
        redGemLetter.fillAmount = 1;
        temp2 = redGem.localScale;
        temp2.x = 1;
        temp2.y = 1;

        redGem.localScale = temp2;
    }

    public void YellowGemEquip()
    {
        yellowGemIsActive = true;
        yellowGemImage.fillAmount = 0;
        yellowGemLetter.fillAmount = 0;
        temp3 = yellowGem.localScale;
        temp3.x = 2;
        temp3.y = 2;

        yellowGem.localScale = temp3;
        BlueGemUnequip();
        RedGemUnequip();
    }

    public void YellowGemUnequip()
    {
        yellowGemIsActive = false;
        yellowGemImage.fillAmount = 1;
        yellowGemLetter.fillAmount = 1;
        temp3 = yellowGem.localScale;
        temp3.x = 1;
        temp3.y = 1;

        yellowGem.localScale = temp3;
    }
}
