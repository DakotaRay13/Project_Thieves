﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Alex : Player
{
    public bool isBlocking = false;
    public int MAX_STAMINA;
    public int STAMINA;

    Coroutine currentAttack = null;

    public override void LightAttack(InputAction.CallbackContext context)
    {
        if ((!movementLock || (isAttacking && canAttack)) && !anim.anim.GetBool("isHit"))
            if (context.started)
            {
                if (anim.direction == -moveInput) anim.TurnCharacter();
                anim.anim.SetTrigger("LightAttack");

                if (isAttacking)
                {
                    StopAttack();
                }
                currentAttack = StartCoroutine(WeaponAnim(0.4f));

                /*
                movementLock = true;
                isAttacking = true;
                canAttack = false;
                */
            }
    }

    public override void HeavyAttack(InputAction.CallbackContext context)
    {
        if ((!movementLock || (isAttacking && canAttack)) && !anim.anim.GetBool("isHit"))
            if (context.started)
            {
                if (anim.direction == -moveInput) anim.TurnCharacter();
                anim.anim.SetTrigger("HeavyAttack");

                if (isAttacking)
                {
                    StopAttack();
                }
                currentAttack = StartCoroutine(WeaponAnim(1.0f));

            }
    }

    public override void DefensiveAction(InputAction.CallbackContext context)
    {
        if (!movementLock || isAttacking)
            if (context.started)
            {
                if ((controller.collisions.below || controller.collisions.grounded) && !isBlocking)
                {
                    if (anim.direction == -moveInput) anim.TurnCharacter();
                    StartBlock();
                }
            }
        if (context.canceled) StopBlock();
    }

    private void LateUpdate()
    {
        if(isAttacking || isBlocking)
        {
            if ((anim.jumping && wasGrounded))
            {
                StopAttack();
                StopBlock();
            }
        }
    }

    public override float GetTargetVelocity()
    {
        return 0f;
    }

    public IEnumerator WeaponAnim(float time)
    {
        movementLock = true;
        isAttacking = true;
        canAttack = false;

        yield return new WaitForSeconds(time);

        isAttacking = false;
        movementLock = false;
        canAttack = true;
    }

    public void StopAttack()
    {
        if(currentAttack != null)
            StopCoroutine(currentAttack);
        isAttacking = false;
        movementLock = false;
        canAttack = true;
    }

    public void StartBlock()
    {
        anim.anim.Play("Block");
        anim.anim.SetBool("isBlocking", true);
        isBlocking = true;
        movementLock = true;
    }

    public void StopBlock()
    {
        isBlocking = false;
        movementLock = false;
        anim.anim.SetBool("isBlocking", false);
    }

    public override void TakeDamage(int damage, float enemyPos)
    {
        if (!isBlocking && !anim.anim.GetBool("Invinsibility Frames"))
        {
            GetComponentInChildren<SFX_Manager_Alex>().PlayAudio2("hit");
            TurnTowardsEnemy(enemyPos);
            StopAttack();
            HEALTH -= damage;
            if (HEALTH > 0f) StartCoroutine(HitStun());
        }
        else if (isBlocking && !anim.anim.GetBool("Invinsibility Frames"))
        {
            if(STAMINA > 0)
            {
                GetComponentInChildren<SFX_Manager_Alex>().PlayAudio2("block");
                TurnTowardsEnemy(enemyPos);
                STAMINA = Mathf.Clamp(STAMINA - (damage / 2), 0, MAX_STAMINA);
                StartCoroutine(BlockInvinsibility());
            }
            else
            {
                StopBlock();
                TakeDamage(damage, enemyPos);
            }
        }
    }

    public IEnumerator BlockInvinsibility()
    {
        anim.anim.SetBool("Invinsibility Frames", true);

        yield return new WaitForSeconds(1f);

        anim.anim.SetBool("Invinsibility Frames", false);
    }

    public override void LandingSFX()
    {
        GetComponentInChildren<SFX_Manager_Alex>().PlayAudio("walk");
    }
}
