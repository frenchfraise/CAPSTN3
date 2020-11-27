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

    public GameObject blueGemUI;
    public GameObject redGemUI;
    public GameObject yellowGemUI;

    public GameObject bluePointer;
    public GameObject redPointer;
    public GameObject yellowPointer;

    bool blueGemGet;
    bool redGemGet;
    bool yellowGemGet;

    int gem;

    Vector3 temp1;
    Vector3 temp2;
    Vector3 temp3;

    public GameObject gems;

    // Start is called before the first frame update
    void Start()
    {
        blueGemLetter.fillAmount = 0;
        redGemLetter.fillAmount = 0;
        yellowGemLetter.fillAmount = 0;

        blueGemIsActive = false;
        redGemIsActive = false;
        yellowGemIsActive = false;

        blueGemGet = false;
        redGemGet = false;
        yellowGemGet = false;
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

    public void GemGet(int gem)
    {
        if(gem == 1)
        {
            BlueGemGet();
            blueGemGet = true;
        }
        else if (gem == 2)
        {
            RedGemGet();
            redGemGet = true;
        }
        else if (gem == 3)
        {
            YellowGemGet();
            yellowGemGet = true;
        }
    }

    public void BlueGemGet()
    {
        blueGemUI.SetActive(true);
    }

    public void RedGemGet()
    {
        redGemUI.SetActive(true);
    }

    public void YellowGemGet()
    {
        yellowGemUI.SetActive(true);
    }

    public void BlueGemEquip()
    {
        bluePointer.SetActive(true);
        blueGemIsActive = true;
        blueGemImage.fillAmount = 0;
        //blueGemLetter.fillAmount = 0;
        //temp1 = blueGem.localScale;
        //temp1.x = 1.6f;
        //temp1.y = 1.6f;

        //blueGem.localScale = temp1;
        RedGemUnequip();
        YellowGemUnequip();
    }

    public void BlueGemUnequip()
    {
        bluePointer.SetActive(false);
        blueGemIsActive = false;
        blueGemImage.fillAmount = 1;
        //blueGemLetter.fillAmount = 1;
        temp1 = blueGem.localScale;
        temp1.x = .85f;
        temp1.y = .85f;

        blueGem.localScale = temp1;
    }

    public void RedGemEquip()
    {
        redPointer.SetActive(true);
        redGemIsActive = true;
        redGemImage.fillAmount = 0;
        //redGemLetter.fillAmount = 0;
        //temp2 = redGem.localScale;
        //temp2.x = 1.6f;
        //temp2.y = 1.6f;

        //redGem.localScale = temp2;
        BlueGemUnequip();
        YellowGemUnequip();
    }

    public void RedGemUnequip()
    {
        redPointer.SetActive(false);
        redGemIsActive = false;
        redGemImage.fillAmount = 1;
        //redGemLetter.fillAmount = 1;
        temp2 = redGem.localScale;
        temp2.x = .85f;
        temp2.y = .85f;

        redGem.localScale = temp2;
    }

    public void YellowGemEquip()
    {
        yellowPointer.SetActive(true);
        yellowGemIsActive = true;
        yellowGemImage.fillAmount = 0;
        //yellowGemLetter.fillAmount = 0;
        //temp3 = yellowGem.localScale;
        //temp3.x = 1.6f;
        //temp3.y = 1.6f;

        //yellowGem.localScale = temp3;
        BlueGemUnequip();
        RedGemUnequip();
    }

    public void YellowGemUnequip()
    {
        yellowPointer.SetActive(false);
        yellowGemIsActive = false;
        yellowGemImage.fillAmount = 1;
        //yellowGemLetter.fillAmount = 1;
        temp3 = yellowGem.localScale;
        temp3.x = .85f;
        temp3.y = .85f;

        yellowGem.localScale = temp3;
    }
}
