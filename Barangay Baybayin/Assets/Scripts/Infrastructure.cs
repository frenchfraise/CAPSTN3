using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InfrastructureHitEvent : UnityEvent<int, int, UnityEvent> { }
[RequireComponent(typeof(Health))]
public class Infrastructure : MonoBehaviour
{
    bool canInteract = true;
    private HealthOverheadUI healthOverheadUI;
    [SerializeField] public SO_Infrastructure so_Infrastructure;
    //private int currentHealth;
    private SpriteRenderer sr;
    [SerializeField] public int currentLevel = 1;
    public InfrastructureHitEvent OnInfrastructureHitEvent = new InfrastructureHitEvent();
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {


    }

    protected void OnEnable()
    {
        //base.OnEnable();


        StartCoroutine(Co_TempLoad());

       // currentHealth = 0;
    }

    IEnumerator Co_TempLoad()
    {
        Health health = GetComponent<Health>();
        health.SetValues(so_Infrastructure.maxHealth);
        health.enabled = true;
        yield return new WaitForSeconds(0.5f);
     
        health.OnDeathEvent.AddListener(Constructed);
        //GenericObjectPool objectPool = ObjectPoolManager.GetPool(typeof(HealthOverheadUI)); //URGENT FIX
        //PoolableObject healthOverheadUIObject = objectPool.pool.Get();
        //healthOverheadUI = healthOverheadUIObject.GetComponent<HealthOverheadUI>();

        //healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
        //health.onHealthModifiedEvent.AddListener(healthOverheadUI.OnHealthChanged);
        //health.OnDeathEvent.AddListener(healthOverheadUI.OnHealthDied);

        //OnInfrastructureHitEvent.AddListener(Hit);
    }
    protected void Hit( int p_craftLevel, int p_currentDamage, UnityEvent p_eventCallback)
    {
        if (canInteract)
        {
            //foreach (SO_Infrastructure useForResourceNode in p_useForResourceNode)
            //{
                //if (useForResourceNode == so_Infrastructure)
                //{

                    //if (p_craftLevel >= levelRequirement)
                    //{

                    Health health = GetComponent<Health>();

                    health.onHealthModifyEvent.Invoke(-p_currentDamage);

                    p_eventCallback.Invoke();

                    //}
               // }
            //}


            



        }

    }

    void Constructed()
    {
        
        if (currentLevel >= so_Infrastructure.sprites.Count)
        {
            Debug.Log("MAX LEVEL REACHED");
        }
        else
        {
            currentLevel++;
            //currentHealth = 0;

            healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
            sr.sprite = so_Infrastructure.sprites[currentLevel - 1];
        }


        
    }
    //protected override void OnInteract()
    //{
    //    if (canInteract)
    //    {

    //        currentHealth++;

    //        if (currentHealth >= so_Infrastructure.maxHealth)
    //        {
    //            if (currentLevel >= so_Infrastructure.sprites.Count)
    //            {
    //                Debug.Log("MAX LEVEL REACHED");
    //            }
    //            else
    //            {
    //                currentLevel++;
    //                currentHealth = 0;
    //                sr.sprite = so_Infrastructure.sprites[currentLevel-1];
    //            }


    //        }


    //    }

    //}
}
