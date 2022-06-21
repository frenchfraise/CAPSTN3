using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystick : MonoBehaviour
{
    [SerializeField] private float speed;
    public Joystick joystick;
    private Rigidbody2D rb;
    private Vector2 movement;

    public Animator animator;

    public GameObject interactHint;
    public SpriteRenderer interactHintImage;
    [SerializeField] public Transform aim;
    [SerializeField] private float aimOffset;
    [SerializeField] private SpriteRenderer sort;
    private void Start()
    {
        rb = rb ? rb : GetComponent<Rigidbody2D>();
        aim.position = (Vector2)transform.position + (aimOffset * movement);        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        //Vector3 t = transform.position;
        //sort.sortingOrder = t.y;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        if (movement != Vector2.zero)
        {
           
            aim.position = (Vector2)transform.position + (aimOffset * movement);
            ResourceNode re = GetComponent<ToolCaster>().GetResourceNode();
           
            if (re)
            {
                
                interactHintImage.sprite = re.hintSprite;
                interactHint.SetActive(true);
            }
            else
            {
                InteractibleObject test = InteractibleHint();
                if (test)
                {
                    interactHintImage.sprite = test.hintSprite;
                    interactHint.SetActive(true);

                }
                else
                {
                    if (interactHint.activeSelf)
                    {
                        interactHint.SetActive(false);
                    }
                }
                
                
                
            }
        }        
    }

    InteractibleObject InteractibleHint()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)aim.position, 3f);
        foreach (Collider2D hit in collider)
        {
            //Debug.Log(collider[0].gameObject.name);
            if (hit.gameObject != gameObject)
            {
                if (hit != null)
                {
                    InteractibleObject targetInteractibleObject = hit.gameObject.GetComponent<InteractibleObject>();
                    if (targetInteractibleObject)
                    {
                        return targetInteractibleObject;

                    }
                }
            }
        }
        return null;
    }
}
