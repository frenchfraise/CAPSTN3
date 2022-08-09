using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTest : MonoBehaviour
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
    public Rigidbody2D rb;
    [SerializeField] private float magnetizeDelay = 2f;
    [SerializeField] private float magnetizeSpeed = 40f;

    public SpriteRenderer sr;
    [SerializeField] Animator anim;

    public bool isSetUped = false;

    //public void SetUpInit(ResourceDrop chosenResourceDrop)
    //{

    //    so_Item = chosenResourceDrop.so_Item;
    //    PlayerManager.instance.SpawnNewItemFloater(so_Item,
    //       (1));
    //    Destroy(gameObject);

    //}
    //public IEnumerator test(ResourceDrop chosenResourceDrop)
    //{

    //    //offsetY = Random.Range(minOffsetY, maxOffsetY);
    //    //splashVelocity = Vector2.up * Random.Range(minBurstVelocity, maxBurstVelocity);
    //    //splashVelocity += new Vector2(Random.Range(-1f, 1f) * Random.Range(minOffsetX, maxOffsetX), 0);
    //    //currentSplashVelocity = splashVelocity;
 
     
    //    //sr.sprite = chosenResourceDrop.so_Item.icon;

    //    //isSplashing = false;
    //    //isMagnetizing = false;
    //    //plrTransform = PlayerManager.instance.playerTransform;
     
    //    //yield return new WaitForSeconds(2.8f); //2.75 og
    //    //SetUp(chosenResourceDrop);
    //}
    //public void SetUp(ResourceDrop chosenResourceDrop)
    //{

    //    transform.position = PlayerManager.instance.playerNodePosition;//.position;
    //    startPositionY = transform.position.y;
     
    //    isSetUped = true;

    //}
  
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
    //        PlayerManager.instance.SpawnNewItemFloater(so_Item,
    //        (1));
    //        Destroy(gameObject);
      
    //    }
    //}

    //IEnumerator Co_Magnetize()
    //{
    //    anim.SetTrigger("start");
    //    yield return new WaitForSeconds(magnetizeDelay);
    //    anim.SetTrigger("stop");
    //    isMagnetizing = true;


    //}
}
