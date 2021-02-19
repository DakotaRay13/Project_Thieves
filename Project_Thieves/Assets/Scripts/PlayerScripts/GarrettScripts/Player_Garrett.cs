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
    public Base_GarrettWeapon nailGun;

    //Garrett's currently equiped gun / Heavy Attack
    public Base_GarrettWeapon equippedGun;

    public override void LightAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            FireWeapon(nailGun);
        }
    }

    public override void HeavyAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            FireWeapon(equippedGun);
        }
    }

    public override void DefensiveAction(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void FireWeapon(Base_GarrettWeapon gun)
    {
        if(gun.ammoCost <= AMMO)
        {
            gun.Fire(shootPoint.position, anim.direction);
            AMMO -= gun.ammoCost;
        }
        //Else, play a click sound effect
    }
}
