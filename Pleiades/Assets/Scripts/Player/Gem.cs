using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gem : MonoBehaviour
{
    public bool blueGemisActive;
    public bool redGemisActive;
    public bool yellowGemisActive;

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
        blueGemisActive = true;
        redGemisActive = false;
        yellowGemisActive = false;

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

        if (Input.GetKeyDown("q"))
        {
            BlueGemEquip();
        }
        if (Input.GetKeyDown("w"))
        {
            RedGemEquip();
        }
        if (Input.GetKeyDown("e"))
        {
            YellowGemEquip();
        }

        if (Pause.GameIsPaused == true)
        {
            gems.SetActive(false);
        }
        if (Pause.GameIsPaused == false)
        {
            gems.SetActive(true);
        }
    }

    public void BlueGemEquip()
    {
        blueGemisActive = true;
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
        blueGemisActive = false;
        blueGemImage.fillAmount = 1;
        blueGemLetter.fillAmount = 1;
        temp1 = blueGem.localScale;
        temp1.x = 1;
        temp1.y = 1;

        blueGem.localScale = temp1;
    }

    public void RedGemEquip()
    {
        redGemisActive = true;
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
        redGemisActive = false;
        redGemImage.fillAmount = 1;
        redGemLetter.fillAmount = 1;
        temp2 = redGem.localScale;
        temp2.x = 1;
        temp2.y = 1;

        redGem.localScale = temp2;
    }

    public void YellowGemEquip()
    {
        yellowGemisActive = true;
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
        yellowGemisActive = false;
        yellowGemImage.fillAmount = 1;
        yellowGemLetter.fillAmount = 1;
        temp3 = yellowGem.localScale;
        temp3.x = 1;
        temp3.y = 1;

        yellowGem.localScale = temp3;
    }
}
