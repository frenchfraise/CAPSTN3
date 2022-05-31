using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item :InteractibleObject
{
    public SO_Item so_Item;

    private Vector2 currentSplashVelocity;
    [HideInInspector] public Vector2 startPosition;

    [SerializeField] private float downwardVelocity = 15f;
    [SerializeField] private float downwardVelocityLimit = -15f;

    private float offsetY;
    [SerializeField] private float minOffsetY;
    [SerializeField] private float maxOffsetY;

    private float offsetX;
    [SerializeField] private float minOffsetX;
    [SerializeField] private float maxOffsetX;

    private Vector2 splashVelocity;
    [SerializeField] private float minBurstVelocity;
    [SerializeField] private float maxBurstVelocity;

    [SerializeField] private float hoverUpPeakOffset;
    [SerializeField] private float hoverUpSpeed;
    [SerializeField] private float hoverUpRate;
    [SerializeField] private float delayTime;
    [SerializeField] private float hoverDownSpeed;
    [SerializeField] private float hoverDownRate;


    private GameObject floaterPrefab;

    //magnetize
    private Rigidbody2D rb;
    private GameObject player;
    private bool isMagnetic = false;
    private float magnetizeDelay = 2f;
    protected override void OnInteract()
    {
        if (canInteract)
        {
            InventoryManager.AddItem(so_Item, 1);
            Destroy(gameObject);
        }
        
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        canInteract = false;
        rb.gravityScale = 0f;
        rb.isKinematic = true;
       
        offsetY = Random.Range(minOffsetY, maxOffsetY);
    }
    private void Awake()
    {
        splashVelocity = Vector2.up * Random.Range(minBurstVelocity, maxBurstVelocity);
        splashVelocity += new Vector2(Random.Range(-1f, 1f) * Random.Range(minOffsetX,maxOffsetX), 0);
        currentSplashVelocity = splashVelocity;
        rb = GetComponent<Rigidbody2D>();



    }

    private void Start()
    {
   
      

        //Magnetize
        //Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)transform.position, 3f);
        //foreach (Collider2D hit in collider)
        //{
        //    if (hit.gameObject != gameObject)
        //    {
        //        if (hit != null)
        //        {
        //            if (hit.gameObject.GetComponent<Joystick>())
        //            {
        //                player = hit.gameObject;
        //            }


        //        }
        //    }

        //}

        // StartCoroutine(Magnetize());
    }

    private void Update()
    {
        Splash();
        //if (when >= delay)
        //{
        //    pastTime = Time.deltaTime;
        //    transform.position += new Vector3(currentSplashRadius, currentSplashRadius,transform.position.z) * Time.deltaTime;
        //    delay += pastTime;
        //}

        //if (magnetize)
        //{
        //    Vector3 playerPoint = Vector3.MoveTowards(transform.position, 
        //        player.transform.position + new Vector3(0, -0.3f, 0),
        //        20 * Time.deltaTime);
        //    rb.MovePosition(playerPoint);
        //}
    }

    void ShowFloatingText()
    {
        var go=Instantiate(floaterPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = "+1";
    }

    void Splash()
    {
        if (!canInteract)
        {
            rb.position += currentSplashVelocity * Time.deltaTime;
            if (currentSplashVelocity.y < downwardVelocityLimit)
            {
                currentSplashVelocity.y = downwardVelocityLimit;
            }
            else
            {
                currentSplashVelocity -= Vector2.up * downwardVelocity * Time.deltaTime;
            }

            if (currentSplashVelocity.y < 0f)
            {
                if (rb.position.y < startPosition.y - offsetY)
                {
                    //Debug.Log("STOP" + rb.position.y + " - " + (startPosition.y - offsetY));

                    currentSplashVelocity = Vector2.zero;
                    rb.velocity = Vector2.zero;
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    canInteract = true;

                    StartCoroutine(Co_Hover());
                }
            }
        }
       
     
  
    }
    IEnumerator Co_Hover()
    {
        float startYPosition = transform.position.y;
    
        while (transform.position.y < startYPosition + hoverUpPeakOffset)
        {
            transform.position  += new Vector3(0, hoverUpSpeed);
            yield return new WaitForSeconds(hoverUpRate);
        }
       
        
        yield return new WaitForSeconds(delayTime);

        while (transform.position.y > startYPosition)
        {
            transform.position -= new Vector3(0, hoverDownSpeed);
            yield return new WaitForSeconds(hoverDownRate);
        }
        StartCoroutine(Co_Hover());
    }
    IEnumerator Co_Magnetize()
    {
        yield return new WaitForSeconds(magnetizeDelay);
        isMagnetic = true;
    }
}

     

