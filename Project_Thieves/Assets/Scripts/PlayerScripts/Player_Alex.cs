using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Alex : Player
{
    public override void LightAttack(InputAction.CallbackContext context)
    {
        if (!movementLock || (isAttacking && canAttack))
            if (context.started)
            {
                if (anim.direction == -moveInput) anim.TurnCharacter();
                anim.anim.Play("LightSwing");

                if (isAttacking)
                {
                    StopAllCoroutines();
                }
                StartCoroutine(WeaponAnim(0.8f));

                /*
                movementLock = true;
                isAttacking = true;
                canAttack = false;
                */
            }
    }

    public override void HeavyAttack(InputAction.CallbackContext context)
    {
        if (!movementLock || (isAttacking && canAttack))
            if (context.started)
            {
                if (anim.direction == -moveInput) anim.TurnCharacter();
                anim.anim.Play("HeavySwing");

                if (isAttacking)
                {
                    StopAllCoroutines();
                }
                StartCoroutine(WeaponAnim(1.0f));

            }
    }

    public override void DefensiveAction(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
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
}
