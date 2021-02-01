using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    //This Character's Animator
    public Animator anim;
    public GameObject model;

    //Player Controllers
    public BasePlayerController pc;

    private void Update()
    {
        CheckMoveAnims();
    }

    

    //flip the character model on the Z axis
    public void TurnCharacter(float moveInput)
    {
        if (moveInput < 0)
        {
            model.transform.localScale = new Vector3(1f, 1f, -1f);
        }
        else if (moveInput > 0)
        {
            model.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    
    //Sets the character to moving
    public void SetIsMoving(float moveVelocity)
    {
        if (moveVelocity != 0f && anim.GetBool("isGrounded") == true)
        {
            anim.SetBool("isRunning", true);
        }
        else anim.SetBool("isRunning", false);
    }
    
    //Reset all the animations
    public void ResetMovementAnims()
    {
        anim.SetBool("isGrounded", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("isRising", false);
        anim.SetBool("isFalling", false);
    }

    public void CheckMoveAnims()
    {
        ResetMovementAnims();
        if (pc.MoveState == BasePlayerController.EMovement.GROUNDED)
        {
            anim.SetBool("isGrounded", true);
        }
        if (pc.MoveState == BasePlayerController.EMovement.JUMPING)
        {
            anim.SetBool("isJumping", true);
        }
        if (pc.MoveState == BasePlayerController.EMovement.RISING)
        {
            anim.SetBool("isRising", true);
        }
        if (pc.MoveState == BasePlayerController.EMovement.FALLING)
        {
            anim.SetBool("isFalling", true);
        }
    }
}
