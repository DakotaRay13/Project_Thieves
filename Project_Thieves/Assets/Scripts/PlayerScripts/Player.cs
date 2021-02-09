using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
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

    public bool isCoyoteTime = false;
    float coyoteTime = 0.1f;
    bool wasGrounded = true;

    bool bufferJump = false;
    public float bufferTime = 0.1f;

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

        Debug.Log("Gravity: " + gravity + ", Jump Velocity: " + maxJumpVelocity + " CT: " + coyoteTime);

        //Set up Input Functions
        inputActions.Platforming.Move.started += _ => moveInput = inputActions.Platforming.Move.ReadValue<float>();
        inputActions.Platforming.Move.performed += _ => moveInput = inputActions.Platforming.Move.ReadValue<float>();
        inputActions.Platforming.Move.canceled += _ => moveInput = inputActions.Platforming.Move.ReadValue<float>();

        inputActions.Platforming.Jump.performed += _ => StartJump();
        inputActions.Platforming.Jump.canceled += _ => StopJump();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0f;
        }

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
        Debug.Log("Coyote Time has begun!");

        isCoyoteTime = true;

        //Wait for seconds
        yield return new WaitForSeconds(coyoteTime);

        isCoyoteTime = false;
    }

    public IEnumerator BufferJump()
    {
        Debug.Log("Buffer Jump Used");

        bufferJump = true;

        yield return new WaitForSeconds(bufferTime);

        bufferJump = false;
    }
}
