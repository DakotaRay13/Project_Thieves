using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Garrett : Player
{
    //The point where Garrett can shoot from
    public Transform shootPoint;

    //Garrett's Nail Gun / Light Attack
    public Base_GarrettWeapon nailGun;

    //Garrett's currently equiped gun / Heavy Attack

    public override void LightAttack()
    {
        Debug.Log("Light Attack Used");
        FireWeapon(nailGun);
    }

    public override void HeavyAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void DefensiveAction()
    {
        throw new System.NotImplementedException();
    }

    public void FireWeapon(Base_GarrettWeapon gun)
    {
        Base_Bullet bullet = Instantiate<Base_Bullet>(gun.bullet, shootPoint.position, Quaternion.identity);
        bullet.rb = bullet.GetComponent<Rigidbody2D>();
        bullet.rb.velocity = Vector2.right * bullet.speed * anim.direction;
    }
}
