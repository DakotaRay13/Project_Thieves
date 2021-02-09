﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    //This Character's Animator
    public Animator anim;
    public GameObject model;

    float direction;            //Player Direction
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
        direction *= -1;
        model.transform.localScale = new Vector3(1f, 1f, direction);
    }
    
    public void CheckAnims(float moveInput, Vector2 velocity, bool isGrounded)
    {
        //if the move input is the opposite of the character's direction, turn around.
        if (direction == -moveInput)
        {
            TurnCharacter();
        }

        //if the character is grounded and moving, start the running animation
        if(moveInput != 0 && isGrounded)
        {
            anim.Play("Running");
        }

        //If the character is grounded and not moving, start the idle animation
        if(moveInput == 0 && isGrounded)
        {
            anim.Play("Idle");
        }

        //Check the jumping variable
        if(jumping && velocity.y < 0)
        {
            jumping = false;
        }
        
        //If the character is not grounded and not jumping, start Airbourne
        if(!isGrounded && !jumping)
        {
            anim.Play("Airbourne");
        }

        if (!isGrounded && jumping)
        {
            anim.Play("Jumping");
        }
    }
}
