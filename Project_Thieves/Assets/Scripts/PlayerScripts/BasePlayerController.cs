using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerController : MonoBehaviour
{
    //Player Controls
    private PlayerInputControls inputActions;

    //this character's Rigidbody
    private Rigidbody2D rb;
    
    //Character's Movement States
    private enum EMovement
    {
        GROUNDED,       //The Character is on the Ground
        JUMPING,        //The Character is Jumping
        RISING,         //The Character is in the air after a jump (Hang Time)
        FALLING         //The Character is falling through the air
    }
    [SerializeField] private EMovement MoveState;

    //Move Variables
    private float moveSpeed, moveVelocity;

    //EdgeCollisions
    public BoxCollider2D footPos, leftPos, rightPos;
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
        //Set Move Variables
        moveSpeed = 9f;
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
        MoveCharacter();
    }

    void FixedUpdate()
    {
        //Read the movement value and move the player
        //MoveCharacter();
        
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
                    if (footPos.IsTouchingLayers(levelLayer) == false)
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
                    if (footPos.IsTouchingLayers(levelLayer) == true)
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

                    break;
                }
        }
    }
        
    //Move the Character on the Horizontal Axis
    public void MoveCharacter()
    {
        //Read the Movement Value from the inputActions
        float moveInput = GetMoveInput();

        //if the character isn't moving into the wall, move. Else, don't.
        if ((moveInput < 0 && leftPos.IsTouchingLayers(levelLayer)) ||
            (moveInput > 0 && rightPos.IsTouchingLayers(levelLayer)))
        {
            moveVelocity = 0f;
        }

        //Calculate the movement
        else
        {
            moveVelocity = moveInput * moveSpeed * Time.deltaTime;
        }

        //Move the Character
        //rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

        Vector3 newPos = transform.position;
        newPos += new Vector3(moveVelocity, 0f, 0f);
        transform.position = newPos;
    }

    //Get the value of the movement input
    public float GetMoveInput()
    {
        return inputActions.Platforming.Move.ReadValue<float>();
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
