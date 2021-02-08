using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerController : MonoBehaviour
{
    //Player Controls
    private PlayerInputControls inputActions;

    //this character's Rigidbody
    private Rigidbody2D rb;

    //This Character's Animator
    public PlayerAnimations anim;
    
    //Character's Movement States
    public enum EMovement
    {
        GROUNDED,       //The Character is on the Ground
        JUMPING,        //The Character is Jumping
        RISING,         //The Character is in the air after a jump (Hang Time)
        FALLING         //The Character is falling through the air
    }
    public EMovement MoveState;

    //Move Variables
    public float moveInput;
    [SerializeField] private float moveSpeed, moveVelocity;

    //Edge Collisions
    private BoxCollider2D playerCollision;
    private bool isGrounded;
    public LayerMask levelLayer;

    //Jump Variables
    private float jumpSpeed;
    private float jumpTime, jumpTimeMax;
    
    //Coyote Time variables
    private bool isCoyoteTime;
    private float coyoteTime;

    //Gravity Variables
    private float moveGravity;
    private float jumpGravity;
    private float risingGravity;

    //Buffer Variables
    private bool jumpBuffer;
    private float jumpBufferTime;
    
    private void Awake()
    {
        inputActions = new PlayerInputControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        playerCollision = GetComponent<BoxCollider2D>();
        CheckForGround();

        //Set Move Variables
        moveSpeed = 430f;
        moveVelocity = 0f;

        //Set Jump Variables
        jumpSpeed = 1200f;
        jumpTime = 0f;
        jumpTimeMax = .1f;

        //Set Gravity Variables
        moveGravity = 15f;
        jumpGravity = 0f;
        risingGravity = 13f;
        rb.gravityScale = moveGravity;

        //Set Coyote Time Variables
        isCoyoteTime = false;
        coyoteTime = 0.05f;

        //Set Buffer Variables
        jumpBuffer = false;
        jumpBufferTime = 0.1f;

        //Set MoveState to GROUNDED
        UpdateMoveState(EMovement.GROUNDED);

        //Set up Input Functions
        inputActions.Platforming.Jump.performed += _ => StartJump();
        inputActions.Platforming.Jump.canceled  += _ => StopJump(true);
    }

    private void Update()
    {
        //Get Movement input
        GetMoveInput();

        //Check if the player is grounded
        CheckForGround();
    }

    void FixedUpdate()
    {
        //Read the movement value and move the player
        MoveCharacter();
        
        //Check the movement states to see if they're still valid
        CheckMovement();
    }

    // MOVE FUNCTIONS //

    //Check the movement states to see if they're still valid
    public void CheckMovement()
    {
        switch(MoveState)
        {
            case (EMovement.GROUNDED):
                {
                    //If there is no ground below the player, they are falling.
                    if (isGrounded == false)
                    {
                        UpdateMoveState(EMovement.FALLING);
                        StartCoroutine(PerformCoyoteTime());
                    }
                    break;
                }
            case (EMovement.JUMPING):
                {
                    //If the character is jumping, continue the jump
                    Jumping();
                    break;
                }
            case (EMovement.RISING):
                {
                    //EXIT PARAMETER: Velocity Y is <= 0
                    if (rb.velocity.y <= 0)
                    {
                        UpdateMoveState(EMovement.FALLING);
                    }
                    break;
                }
            case (EMovement.FALLING):
                {
                    //If there is ground below the player, they are grounded.
                    if (isGrounded == true)
                    {
                        UpdateMoveState(EMovement.GROUNDED);
                    }
                    break;
                }
        }
    }

    //What should happen when the MoveState is changed?
    private void UpdateMoveState(EMovement newMovement)
    {
        switch (newMovement)
        {
            case (EMovement.GROUNDED):
                {
                    //update the gravity
                    rb.gravityScale = moveGravity;

                    //Update the MoveState to newMovement
                    MoveState = newMovement;

                    //If bufferJump is true, start Jump
                    if(jumpBuffer == true)
                    {
                        StartJump();
                    }

                    break;
                }
            case (EMovement.JUMPING):
                {
                    //update the gravity
                    rb.gravityScale = jumpGravity;

                    //Update the MoveState to newMovement
                    MoveState = newMovement;

                    break;
                }
            case (EMovement.RISING):
                {
                    //update the gravity
                    rb.gravityScale = risingGravity;

                    //Update the MoveState to newMovement
                    MoveState = newMovement;

                    break;
                }
            case (EMovement.FALLING):
                {
                    //update the gravity
                    rb.gravityScale = moveGravity;
                    
                    //Update the MoveState to newMovement
                    MoveState = newMovement;

                    Debug.Log("Character is Falling.");

                    break;
                }
        }
    }

    //Check if the ground is underneath the player
    public void CheckForGround()
    {
        float checkDistance = 0.2f;
        RaycastHit2D groundCheck = Physics2D.BoxCast(playerCollision.bounds.center, playerCollision.bounds.size, 0f, Vector2.down, checkDistance, levelLayer);

        //Debug box shows ground check
        //Color rayColor;
        //if(groundCheck.collider != null)
        //{
        //    rayColor = Color.green;
        //}
        //else
        //{
        //    rayColor = Color.red;
        //}

        //Draw the box
        //Debug.DrawRay(playerCollision.bounds.center + new Vector3(playerCollision.bounds.extents.x, 0), Vector2.down * (playerCollision.bounds.extents.y + checkDistance), rayColor);
        //Debug.DrawRay(playerCollision.bounds.center - new Vector3(playerCollision.bounds.extents.x, 0), Vector2.down * (playerCollision.bounds.extents.y + checkDistance), rayColor);
        //Debug.DrawRay(playerCollision.bounds.center - new Vector3(playerCollision.bounds.extents.x, playerCollision.bounds.extents.y + checkDistance), Vector2.right * (playerCollision.bounds.extents.x * 2f), rayColor);

        isGrounded = groundCheck.collider != null;
    }

    //Check for collision in the direction the player is going
    public void HorizontalCollision()
    {
        float direction = Mathf.Sign(rb.velocity.x);
        float checkDistance = 0.2f;

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(playerCollision.bounds.extents.x, playerCollision.size.y / 2), Vector2.right * direction, checkDistance, levelLayer);

        Color rayColor;
        if (hit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(new Vector2((playerCollision.bounds.size.x / 2) * direction, playerCollision.bounds.size.y / 2), Vector2.right * direction, rayColor, checkDistance);
    }

    //Move the Character on the Horizontal Axis
    public void MoveCharacter()
    {
        //Set Character Direction
        anim.TurnCharacter(moveInput);

        HorizontalCollision();

        //if the character isn't moving into the wall, move. Else, don't.
        if (moveInput == 0)
        {
            moveVelocity = 0f;
        }

        //Calculate the movement
        else
        {
            //Set velocity
            moveVelocity = moveInput * moveSpeed * Time.deltaTime;
        }

        //Set Animation
        anim.SetIsMoving(moveVelocity);

        //Move the Character
        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
    }

    //Get the value of the movement input
    public void GetMoveInput()
    {
        moveInput = inputActions.Platforming.Move.ReadValue<float>();
    }

    // JUMP FUNCTIONS //

    //Starts the jump when the "Jump" button is pressed
    public void StartJump()
    {
        if(MoveState == EMovement.GROUNDED || isCoyoteTime == true)
        {
            UpdateMoveState(EMovement.JUMPING);

            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * Time.deltaTime);
        }
        else
        {
            //Start the Jump Buffer
            StartCoroutine(BufferJump());
        }
    }

    //Continues the jump until the jumpMaxTime is met
    public void Jumping()
    {
        //If the character hits their head, stop the jump sharp.
        if (rb.velocity.y <= 0)
        {
            StopJump(true);
        }

        //If the character can jump, continue the jump.
        else if(jumpTime < jumpTimeMax)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * Time.deltaTime);
            jumpTime += Time.deltaTime;
        }

        //If anything else, stop the jump.
        else
        {
            StopJump(false);
        }
    }

    //Stops the jump when either the jumpMaxTime is met or when the character releases the button
    public void StopJump(bool sharp)
    {
        jumpTime = 0f;

        if (MoveState == EMovement.JUMPING)
        {
            if (sharp == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                UpdateMoveState(EMovement.FALLING);
            }

            else
            {
                UpdateMoveState(EMovement.RISING);
            }
        }
    }

    public IEnumerator PerformCoyoteTime()
    {
        //Start Coyote Time
        isCoyoteTime = true;

        //Wait for seconds
        yield return new WaitForSeconds(coyoteTime);

        //Stop Coyote Time
        isCoyoteTime = false;
    }

    public IEnumerator BufferJump()
    {
        //Start Buffer Time
        jumpBuffer = true;

        //Wait for seconds
        yield return new WaitForSeconds(jumpBufferTime);

        //Stop Buffer Time
        jumpBuffer = false;
    }

    // GET FUNCTIONS //
    public PlayerInputControls GetInputActions()
    {
        return inputActions;
    }
}
