using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UpdateJoystickEnabledEvent : UnityEvent<bool> { }
public class PlayerJoystick : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool canJoystick = true;
    public Joystick joystick;
    public Transform arrow;
    private Rigidbody2D rb;
    private ToolCaster toolCaster;
    private Interacter interacter;

    private Vector2 movement;

    public Animator animator;

    public bool isMoving = true;
    private bool canInteractHint = true;
    public GameObject interactHint;
    private SpriteRenderer interactHintImage;

    [SerializeField] public Transform aim;
    [SerializeField] private float aimOffset;

    public static UpdateJoystickEnabledEvent onUpdateJoystickEnabledEvent = new UpdateJoystickEnabledEvent();
    private void Start()
    {
        rb = rb ? rb : GetComponent<Rigidbody2D>();
        aim.position = (Vector2)transform.position + (aimOffset * movement);
        interactHintImage = interactHintImage ? interactHintImage : interactHint.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        toolCaster = toolCaster ? toolCaster : PlayerManager.instance.player.GetComponent<ToolCaster>();
        interacter = interacter ? interacter : PlayerManager.instance.player.GetComponent<Interacter>();
   
    }

    private void Awake()
    {
        ToolManager.onResourceNodeFinishedEvent.AddListener(OnResourceNodeFinishedEvent);
        UIManager.onGameplayModeChangedEvent.AddListener(OnGameplayModeChangedEvent);

        onUpdateJoystickEnabledEvent.AddListener(UpdateJoystickEnabled);
    }

    private void OnDestroy()
    {
        ToolManager.onResourceNodeFinishedEvent.RemoveListener(OnResourceNodeFinishedEvent);
        UIManager.onGameplayModeChangedEvent.RemoveListener(OnGameplayModeChangedEvent);
        onUpdateJoystickEnabledEvent.RemoveListener(UpdateJoystickEnabled);
    }
    private void OnEnable()
    {

    }
    void UpdateJoystickEnabled(bool p_bool)
    {
        canJoystick = p_bool;
    }
    void OnResourceNodeFinishedEvent()
    {
        Debug.Log("IT DIED");
        interactHint.SetActive(false);


    }
    private void OnGameplayModeChangedEvent(bool p_isActive)
    {
        //Debug.Log("RARARA");
        interactHint.gameObject.SetActive(!p_isActive);
        canInteractHint = !p_isActive;


        //joystick.HandleRange = p_isActive ? 0 : 1;
        //joystick.enabled = (!p_isActive);
        //joystick.gameObject.SetActive(!p_isActive);
   
        //canJoystick = !p_isActive;
    
        movement = new Vector2(0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (canJoystick && isMoving)
        {
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            Vector3 moveArrow = new Vector3(joystick.Horizontal, joystick.Vertical);
            arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, moveArrow);
        }
    }

    private void FixedUpdate()
    {
        if (canJoystick)
        {
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
            if (movement != Vector2.zero)
            {
                animator.SetFloat("FaceFloat", aim.localPosition.y);
                aim.position = (Vector2)transform.position + (aimOffset * movement);
                HintAction();

            }
        }
    }

    public void HintAction()
    {
        if (canInteractHint)
        {
            //if (!interactHint.activeSelf)
            //{
                ResourceNode resourceNode = toolCaster.GetResourceNode();
                Infrastructure targetInfrastructure = toolCaster.GetInfrastructure();
                InteractibleObject interactibleObject = interacter.GetInteractibleObject();
                if (resourceNode)
                {
                    interactHintImage.sprite = resourceNode.hintSprite;
                    interactHint.SetActive(true);
                }
                else if (targetInfrastructure)
                {
                    if (targetInfrastructure.canInteract)
                    {
                        interactHintImage.sprite = targetInfrastructure.hintSprite;
                        interactHint.SetActive(true);
                    }
                }
      
                else if (interactibleObject)
                {
                    if (interactibleObject.canInteract)
                    {
                        interactHintImage.sprite = interactibleObject.hintSprite;
                        interactHint.SetActive(true);
                    }
                
                }
                else
                {
                    if (interactHint.activeSelf)
                    {
                        interactHint.SetActive(false);
                    }
               
                }
                
            //}
           
            //else if (interactHint.activeSelf)
            //{
            //    interactHint.SetActive(false);
            //}

        }
        
    }
  
}
