using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Garrett : Player
{
    public int AMMO;
    public int MAX_AMMO;
    
    //The point where Garrett can shoot from
    public Transform shootPoint;

    //Garrett's Nail Gun / Light Attack
    public Weapon_Gun nailGun;

    //Garrett's currently equiped gun / Heavy Attack
    public Weapon_Gun equippedGun;

    public bool isDodging = false;
    public float dodgeSpeed = 9f;

    public override void LightAttack(InputAction.CallbackContext context)
    {
        if(!movementLock || (isAttacking && canAttack))
            if (context.started)
            {
                if (anim.direction == -moveInput) anim.TurnCharacter();
                anim.anim.Play("FireNailGun");
                if (isAttacking)
                {
                    StopAllCoroutines();
                }
                StartCoroutine(WeaponAnim(0.3f));
                FireWeapon(nailGun);
            }
    }

    public override void HeavyAttack(InputAction.CallbackContext context)
    {
        if(AMMO > 0f)
            if (!movementLock || (isAttacking && canAttack))
                if (context.started)
                {
                    if (anim.direction == -moveInput) anim.TurnCharacter();
                    anim.anim.Play("FireShotgun");
                    if (isAttacking)
                    {
                        StopAllCoroutines();
                    }
                    StartCoroutine(WeaponAnim(2.5f));
                    FireWeapon(equippedGun);
                }
    }

    public override void DefensiveAction(InputAction.CallbackContext context)
    {
        if (!movementLock || isAttacking)
            if (context.started)
            {
                if ((controller.collisions.below || controller.collisions.grounded) && !isDodging)
                {
                    if (anim.direction == -moveInput) anim.TurnCharacter();
                    StartDodge();
                }
            }
    }

    private void LateUpdate()
    {
        if(isDodging)
        {
            if((!controller.collisions.grounded && !controller.collisions.below) || anim.jumping || controller.collisions.left || controller.collisions.right)
            {
                Debug.Log("Dodged into a wall or off an edge. Or the player jumped.");
                StopDodge();
            }
        }
        if(isAttacking)
        {
            if ((anim.jumping && wasGrounded))
            {
                StopAllCoroutines();
                isAttacking = false;
                movementLock = false;
            }  
            else if(isDodging)
            {
                StopAllCoroutines();
                isAttacking = false;
            }
        }
    }

    //Make Garrett dash across the screen
    //public IEnumerator Dodge()
    //{
    //    anim.animation.Play("Dodge");
    //    isDodging = true;
    //    movementLock = true;
    //    Debug.Log("Animation Time: " + anim.animation["Dodge"].length * anim.animation["Dodge"].speed);
    //    yield return new WaitForSeconds(anim.animation["Dodge"].length * anim.animation["Dodge"].speed);

    //    isDodging = false;
    //    movementLock = false;
    //}

    public void StartDodge()
    {
        anim.anim.Play("Dodge");
        anim.anim.SetBool("isDodging", true);
        isDodging = true;
        movementLock = true;
    }

    public void StopDodge()
    {
        isDodging = false;
        movementLock = false;
        anim.anim.SetBool("isDodging", false);
    }

    public void FireWeapon(Weapon_Gun gun)
    {
        gun.Fire(shootPoint.position, anim.direction);
        AMMO -= gun.ammoCost;
        if (AMMO < 0) AMMO = 0;
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

    //Get the Target velocity for if the movement lock is on;
    public override float GetTargetVelocity()
    {
        if (isDodging)
        {
            return anim.direction * dodgeSpeed;
        }
        else
        {
            return 0f;
        }
    }
}
