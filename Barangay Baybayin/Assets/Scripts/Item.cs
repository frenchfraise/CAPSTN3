using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public SO_Item so_Item;

    private Vector2 currentSplashVelocity;
    public float startPositionY;

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

    public SpriteRenderer sr;
    [SerializeField] Animator anim;
    
    public bool isSetUped = false;
    //protected override void OnInteract()
    //{
    //    if (canInteract)
    //    {
    //        InventoryManager.AddItem(so_Item, 1);
    //        Destroy(gameObject);
    //    }

    //}
    public IEnumerator test(ResourceDrop chosenResourceDrop)
    {
        yield return new WaitForSeconds(3f);
        SetUp(chosenResourceDrop);
    }
    public void SetUp(ResourceDrop chosenResourceDrop)
    {
        //offsetY = Random.Range(minOffsetY, maxOffsetY);
        //splashVelocity = Vector2.up * Random.Range(minBurstVelocity, maxBurstVelocity);
        //splashVelocity += new Vector2(Random.Range(-1f, 1f) * Random.Range(minOffsetX, maxOffsetX), 0);
        //currentSplashVelocity = splashVelocity;
        //rb = GetComponent<Rigidbody2D>();
        //so_Item = chosenResourceDrop.so_Item;
        //sr.sprite = chosenResourceDrop.so_Item.icon;

        //isSplashing = false;
        //isMagnetizing = false;
        //rb.gravityScale = 0f;
        //rb.isKinematic = true;
        //plrTransform = PlayerManager.instance.playerTransform;


        ////transform.position = PlayerManager.instance.playerNodePosition;
        //startPositionY = transform.position.y;
        ////anim.enabled = false;
        //isSetUped = true;

    }
    protected void OnEnable()
    {
        //base.OnEnable();
        //canInteract = false;



    }
    private void Awake()
    {

    }

    private void Start()
    {
        //Magnetize

    }

    //private void Update()
    //{
    //    if (isSetUped)
    //    {
    //        if (isMagnetizing == true)
    //        {
    //            Magnetize();
    //        }
    //    }

    //}

    //private void FixedUpdate()
    //{
    //    if (isSetUped)
    //    {
    //        if (!isSplashing)
    //        {
    //            Splash();
    //        }
           
    //    }

    //}
    //void Splash()
    //{


    //    rb.position += currentSplashVelocity * Time.deltaTime;
    //    if (currentSplashVelocity.y < downwardVelocityLimit)
    //    {
    //        currentSplashVelocity.y = downwardVelocityLimit;
    //    }
    //    else
    //    {
    //        currentSplashVelocity -= Vector2.up * downwardVelocity * Time.deltaTime;
    //    }

    //    if (currentSplashVelocity.y < 0f)
    //    {
    //        if (rb.position.y < startPositionY - offsetY)
    //        {

    //            currentSplashVelocity = Vector2.zero;
    //            rb.velocity = Vector2.zero;
    //            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

    //            isSplashing = true;


    //            if (firstTime)
    //            {
    //                firstTime = false;
    //                StartCoroutine(Co_Magnetize());
    //            }

    //        }
    //    }
    //}

    //void Magnetize()
    //{
    //    Vector3 plrPosition = plrTransform.position;

    //    if (Vector3.Distance(transform.position, plrPosition) > 1)
    //    {
    //        Vector3 playerPoint = Vector3.MoveTowards(transform.position,
    //        plrPosition + new Vector3(0, 0, 0),
    //        magnetizeSpeed * Time.deltaTime);
    //        transform.position = (playerPoint);

    //    }
    //    else
    //    {
    //        if (so_Item != null)
    //        {
    //            PlayerManager.instance.SpawnNewItemFloater(so_Item,
    //          (1));
    //            Destroy(gameObject);
    //            Debug.Log("HAS SO_ITEM");
    //        }
    //        else
    //        {
    //            Debug.Log("NO SO_ITEM");
    //        }

    //    }



    //}

    //IEnumerator Co_Magnetize()
    //{
    //    //anim.enabled = (true);
    //  //  anim.SetTrigger("start");
    //    // hoverEffect.runningCoroutine = StartCoroutine(hoverEffect.Co_Hover());
    //    yield return new WaitForSeconds(magnetizeDelay);
    //    //anim.enabled = (false);
    //    //anim.SetTrigger("stop");
    //   // isMagnetizing = true;


    //}
}

     

