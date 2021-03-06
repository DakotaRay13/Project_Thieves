﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    //This Character's Animator
    public Animator anim;
    public GameObject model;

    public float direction;            //Player Direction
    public bool jumping = false;       //Bools for animation states

    //Player Controllers
    //public BasePlayerController pc;

    private void Start()
    {
        direction = model.transform.localScale.z;
    }

    //Flip the character model on the Z axis
    public void TurnCharacter()
    {
        Debug.Log("Character Turned");
        direction *= -1;
        model.transform.localScale = new Vector3(1f, 1f, direction);
    }
    
    public void CheckAnims(float moveInput, Vector2 velocity, bool isGrounded, bool isCrouched, bool isAttacking)
    {
        //if the move input is the opposite of the character's direction, turn around.
        if (direction == -moveInput)
        {
            TurnCharacter();
        }

        //if the character is grounded and moving, start the running animation
        if(moveInput != 0f && isGrounded && !isCrouched && !isAttacking)
        {
            anim.SetFloat("WalkSpeed", moveInput * direction);
            if (Mathf.Abs(moveInput) < 1f)
            {
                anim.Play("Walking");
            }
            else
            {
                anim.Play("Running");
            }
            //anim.SetFloat("WalkSpeed", moveInput * direction);
        }

        //If the character is grounded and not moving, start the idle animation
        if(moveInput == 0 && isGrounded && !isCrouched && !isAttacking)
        {
            anim.Play("Idle");
        }

        //Check the jumping variable
        if(jumping && velocity.y <= 0)
        {
            jumping = false;
        }

        //If the character is not grounded and not jumping, start Airbourne
        if (!isGrounded && !jumping && velocity.y <= 0f && !isAttacking)
        {
            //StartCoroutine(FallAnim());
            anim.Play("Airbourne");
        }

        if (!isGrounded && jumping && !isAttacking)
        {
            anim.Play("Jumping");
        }

        if(moveInput == 0 && isCrouched && isGrounded && !isAttacking)
        {
            anim.Play("Crouch");
        }

        if (moveInput != 0 && isCrouched && isGrounded && !isAttacking)
        {
            anim.SetFloat("WalkSpeed", moveInput * direction);
            anim.Play("Crouch Walk");
        }
    }

    public void CanNowAttack()
    {
        GetComponentInParent<Player>().canAttack = true;
    }

    public void PullUpDeathUI()
    {
        FindObjectOfType<GameManager>().CreateDeathUI();
    }

    ///////////////////////////////////////////////////
    ///GARRETT SPECIFIC
    ///
    public void Garrett_StopDodge()
    {
        GetComponentInParent<Player_Garrett>().StopDodge();
    }
}
