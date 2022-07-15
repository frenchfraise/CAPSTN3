using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
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


    private GameObject floaterPrefab;

    private bool isSplashing = false;

    private bool firstTime = true; //test
    private Transform plrTransform;

    private bool isMagnetizing = false;
    //magnetize
    private Rigidbody2D rb;
    [SerializeField] private float magnetizeDelay = 2f;
    [SerializeField] private float magnetizeSpeed = 40f;
    HoverEffect hoverEffect;
    //protected override void OnInteract()
    //{
    //    if (canInteract)
    //    {
    //        InventoryManager.AddItem(so_Item, 1);
    //        Destroy(gameObject);
    //    }

    //}
    protected void OnEnable()
    {
        //base.OnEnable();
        //canInteract = false;
        isSplashing = false;
        isMagnetizing = false;
        rb.gravityScale = 0f;
        rb.isKinematic = true;
        plrTransform = PlayerManager.instance.playerTransform;

        offsetY = Random.Range(minOffsetY, maxOffsetY);
        hoverEffect = GetComponent<HoverEffect>();
        if (hoverEffect.runningCoroutine != null)
        {
            hoverEffect.StopCoroutine(hoverEffect.runningCoroutine);
        }
    
    }
    private void Awake()
    {
        splashVelocity = Vector2.up * Random.Range(minBurstVelocity, maxBurstVelocity);
        splashVelocity += new Vector2(Random.Range(-1f, 1f) * Random.Range(minOffsetX, maxOffsetX), 0);
        currentSplashVelocity = splashVelocity;
        rb = GetComponent<Rigidbody2D>();



    }

    private void Start()
    {
        //Magnetize

    }

    private void Update()
    {
        if (!isSplashing)
        {
            Splash();
        }
        else if (isMagnetizing == true)
        {
            Magnetize();
        }
    }

    void ShowFloatingText()
    {
        var go = Instantiate(floaterPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = "+1";
    }

    void Splash()
    {
        //if (!canInteract)
        //{
        
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
                //canInteract = true;
                isSplashing = true;
               
                hoverEffect.startYPosition = transform.position.y;
                if (firstTime)
                {
                    firstTime = false;
                    StartCoroutine(Co_Magnetize());
                }
               
            }
        }
    }

    void Magnetize()
    {
        Vector3 plrPosition = plrTransform.position;
        
        if (Vector3.Distance(transform.position, plrPosition) > 1)
        {
            Vector3 playerPoint = Vector3.MoveTowards(transform.position,
            plrPosition + new Vector3(0, 0, 0),
            magnetizeSpeed * Time.deltaTime);
            transform.position = (playerPoint);
           
        }
        else
        {
            InventoryManager.AddItem(so_Item, 1);
            Destroy(gameObject);
        }
      
        
  
    }
    
    IEnumerator Co_Magnetize()
    {
        if (hoverEffect.runningCoroutine != null)
        {
            hoverEffect.StopCoroutine(hoverEffect.runningCoroutine);
        }
        hoverEffect.runningCoroutine = hoverEffect.Co_Hover();
        StartCoroutine(hoverEffect.runningCoroutine);
       // hoverEffect.runningCoroutine = StartCoroutine(hoverEffect.Co_Hover());
        yield return new WaitForSeconds(magnetizeDelay);
        if (hoverEffect.runningCoroutine != null)
        {
            hoverEffect.StopCoroutine(hoverEffect.runningCoroutine);
            //Debug.Log("DISABLED");
        }
        isMagnetizing = true;


    }
}

     

