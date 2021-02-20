using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_GarrettWeapon : MonoBehaviour
{
    public enum ShotType
    {
        SINGLE,
        SPREAD
    }
    public ShotType shotType;

    public Base_Bullet bullet;      //Which bullet prefab will this weapon fire
    public float ammoCost;          //How much ammo does this weapon cost

    [Range(0f, 180f)]
    public float spreadAngle;       //How much spread on the shot
    public int numberOfBullets;     //How many bullets

    //Fires the weapon based on the shot type of this gun and what bullet it uses.
    public void Fire(Vector2 shootPoint, float direction)
    {
        switch (shotType)
        {
            case (ShotType.SINGLE):
                {
                    Base_Bullet newBullet = Instantiate(bullet, shootPoint, Quaternion.identity);
                    newBullet.startPos = shootPoint;
                    newBullet.rb.velocity = Vector2.right * newBullet.speed * direction;
                    break;
                }
            case (ShotType.SPREAD):
                {
                    //Fire the shots off in different angles
                    float angleBetweenBullets = spreadAngle / numberOfBullets;
                    float angleBetweenBullets = spreadAngle / (numberOfBullets - 1);
                    float fireAngle = spreadAngle / 2f;

                    Base_Bullet[] newBullets = new Base_Bullet[numberOfBullets];

                    for (int i = 0; i < numberOfBullets; i++)
                    {
                        newBullets[i] = Instantiate(bullet, shootPoint, Quaternion.identity);
                        newBullets[i].startPos = shootPoint;

                        newBullets[i].transform.Rotate(0f, 0f, fireAngle);
                        fireAngle -= angleBetweenBullets;

                        newBullets[i].rb.velocity = newBullets[i].transform.right * newBullets[i].speed * direction;
                    }

                    break;
                }
            default:
                {
                    Base_Bullet newBullet = Instantiate(bullet, shootPoint, Quaternion.identity);
                    newBullet.startPos = shootPoint;
                    newBullet.rb.velocity = Vector2.right * newBullet.speed * direction;
                    break;
                }
        }
    }
}
