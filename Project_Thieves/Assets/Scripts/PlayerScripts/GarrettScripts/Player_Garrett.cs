using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Garrett : Player
{
    public static float AMMO;
    
    //The point where Garrett can shoot from
    public Transform shootPoint;

    //Garrett's Nail Gun / Light Attack
    public Weapon_Gun nailGun;

    //Garrett's currently equiped gun / Heavy Attack
    public Weapon_Gun equippedGun;

    public bool isDashing = false;
    public float dashSpeed = 12f;
    public float dashTime = 0.25f;

    public override void LightAttack(InputAction.CallbackContext context)
    {
        if(!movementLock)
            if (context.started)
            {
                FireWeapon(nailGun);
            }
    }

    public override void HeavyAttack(InputAction.CallbackContext context)
    {
        if (!movementLock)
            if (context.started)
            {
                FireWeapon(equippedGun);
            }
    }

    public override void DefensiveAction(InputAction.CallbackContext context)
    {
        if (!movementLock)
            if (context.started)
            {
                if ((controller.collisions.below || controller.collisions.grounded) && !isDashing)
                {
                    StartCoroutine(Dash());
                }
            }
    }

    private void LateUpdate()
    {
        if(isDashing)
        {
            if((!controller.collisions.grounded && !controller.collisions.below) || controller.collisions.left || controller.collisions.right)
            {
                StopCoroutine(Dash());
                isDashing = false;
                movementLock = false;
            }
        }
    }

    //Make Garrett dash across the screen
    public IEnumerator Dash()
    {
        isDashing = true;
        movementLock = true;

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        movementLock = false;
    }

    public void FireWeapon(Weapon_Gun gun)
    {
        if(gun.ammoCost <= AMMO)
        {
            gun.Fire(shootPoint.position, anim.direction);
            AMMO -= gun.ammoCost;
        }
        //Else, play a click sound effect
    }

    //Get the Target velocity for if the movement lock is on;
    public override float GetTargetVelocity()
    {
        if (isDashing) return anim.direction * dashSpeed;
        else return 0f;
    }
}
