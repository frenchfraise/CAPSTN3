using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemDisplay : MonoBehaviour
{
    public Sprite[] gems;
    public Player player;
    public Gem gemScript;
    public SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        gemScript = player.GetComponent<Gem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gemScript.blueGemIsActive)
        {
            spriteRend.sprite = gems[0];
        }
        else if (gemScript.redGemIsActive)
        {
            spriteRend.sprite = gems[1];
        }
        else if (gemScript.yellowGemIsActive)
        {
            spriteRend.sprite = gems[2];
        }
    }
}
