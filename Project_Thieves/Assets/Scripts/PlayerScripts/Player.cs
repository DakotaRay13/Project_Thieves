using System.Collections;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public abstract class Player : MonoBehaviour
{
    //Player Health
    public static float MAX_HEALTH;
    public static float HEALTH;

    //Player character's Defense
    public float defense;

    //Locks the player's ground movement
    public bool movementLock;

    Controller2D controller;

    //Player Controls
    private PlayerInputControls inputActions;

    //Player Animations
    public PlayerAnimations anim;

    public float moveInput;
    float moveSpeed = 6f;
    float gravity;

    public Vector3 velocity;
    float maxJumpVelocity;
    float minJumpVelocity;
    float velocityXSmoothing;

    float accelTime = 0.1f;

    bool startJump = false;
    bool stopJump = false;

    bool startCrouch = false;
    bool stayCrouch = false;

    public bool isCoyoteTime = false;
    float coyoteTime = 0.1f;
    bool wasGrounded = true;

    bool bufferJump = false;
    public float bufferTime = 0.1f;

    //camera control stuff
    bool cameraControlDelay = false;
    bool cameraControlDelayStart = false;
    public float cameraDealyTime = 0.1f;
    Cinemachine.CinemachineImpulseSource source;
    bool impulseSent = false;

    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float jumpTime = 0.4f;

    private void Awake()
    {
        inputActions = new PlayerInputControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(jumpTime, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * jumpTime;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        UnityEngine.Debug.Log("Gravity: " + gravity + ", Jump Velocity: " + maxJumpVelocity + " CT: " + coyoteTime);

        //Set up Input Functions
        inputActions.Platforming.Move.started += _ => moveInput = inputActions.Platforming.Move.ReadValue<float>();
        inputActions.Platforming.Move.performed += _ => moveInput = inputActions.Platforming.Move.ReadValue<float>();
        inputActions.Platforming.Move.canceled += _ => moveInput = inputActions.Platforming.Move.ReadValue<float>();

        inputActions.Platforming.Crouch.performed += _ => StartCrouch();
        //inputActions.Platforming.Crouch.performed += _ => StayCrouch();
        inputActions.Platforming.Crouch.canceled += _ => CancelCrouch();

        inputActions.Platforming.Jump.performed += _ => StartJump();
        inputActions.Platforming.Jump.canceled += _ => StopJump();
    }

    // Update is called once per frame
    void Update()
    {
        //set velocity Y to 0 if has collisions
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0f;
        }
        
        //start Jump
        
        if(wasGrounded == true && controller.collisions.below == false && velocity.y < 0)
        {
            StartCoroutine(CoyoteTime());
        }
        wasGrounded = controller.collisions.below;

        if(startJump || (bufferJump && controller.collisions.below))
        {
            velocity.y = maxJumpVelocity;
            startJump = false;
            anim.jumping = true;

            if (bufferJump) bufferJump = false;
        }

        if(stopJump)
        {
            if(velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
            stopJump = false;
        }

        //start crouch
        if(controller.collisions.below && startCrouch)
        {
            //edit here for crouch animation

            //camera control
            if (!cameraControlDelayStart && !cameraControlDelay) StartCoroutine(CameraControlDelay());
            if (cameraControlDelay)
            {
                //for shaking screen apparently
                //impulseSent = true;
                //source = GetComponent<Cinemachine.CinemachineImpulseSource>();
                //source.GenerateImpulse(Camera.main.transform.up * -1);
                //insert impulse here for virtual camera
            }
            //else impulseSent = false;
        }

        //float moveInput = inputActions.Platforming.Move.ReadValue<float>();
        float targetVelocityX = moveSpeed * moveInput;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Check Animations
        anim.CheckAnims(moveInput, velocity, controller.collisions.below);
    }

    // JUMP FUNCTIONS //
    void StartJump()
    {
        if (controller.collisions.below || isCoyoteTime)
        {
            startJump = true;
            isCoyoteTime = false;
        }

        else
        {
            if (bufferJump) StopCoroutine(BufferJump());

            StartCoroutine(BufferJump());
        }
    }

    void StopJump()
    {
        stopJump = true;
    }

    public IEnumerator CoyoteTime()
    {
        UnityEngine.Debug.Log("Coyote Time has begun!");

        isCoyoteTime = true;

        //Wait for seconds
        yield return new WaitForSeconds(coyoteTime);

        isCoyoteTime = false;
    }

    public IEnumerator BufferJump()
    {
        UnityEngine.Debug.Log("Buffer Jump Used");

        bufferJump = true;

        yield return new WaitForSeconds(bufferTime);

        bufferJump = false;
    }
    /************************************************************************
     * Function: Crouch stuff
     * started(): transition to crouch
     * Perform(): keep crouch animation
     * cancel(): get out of crouch
     ***********************************************************************/
    
    //===========================Started()===================================
    void StartCrouch()
    {
        startCrouch = true;
    }

    //==========================Perform()====================================
    void StayCrouch()
    {
        stayCrouch = true;
    }
    //===========================cancel()====================================
    void CancelCrouch()
    {
        startCrouch = false;
        stayCrouch = false;
        cameraControlDelay = false;
        
    }
    //==========================CameraControlDelay()=========================
    public IEnumerator CameraControlDelay()
    {
        UnityEngine.Debug.Log("Camera Control Delay");

        cameraControlDelayStart = true;
        cameraControlDelay = false;

        yield return new WaitForSeconds(cameraDealyTime);

        cameraControlDelay = true;
        cameraControlDelayStart = false;

    }

    /************************************************************************
     * BASIC ACTIONS
     ***********************************************************************/

    public abstract void LightAttack();
    public abstract void HeavyAttack();
    public abstract void DefensiveAction();
}
