using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Gun : MonoBehaviour
{
    public enum ShotType
    {
        SINGLE,
        SPREAD
    }
    public ShotType shotType;

    public Bullet bullet;      //Which bullet prefab will this weapon fire
    public float ammoCost;          //How much ammo does this weapon cost

    [Range(0f, 360f)]
    public float spreadAngle;       //How much spread on the shot
    public int numberOfBullets;     //How many bullets

    public float speed;
    public float damage;
    public float range;

    //Fires the weapon based on the shot type of this gun and what bullet it uses.
    public void Fire(Vector2 shootPoint, float direction)
    {
        switch (shotType)
        {
            case (ShotType.SINGLE):
                {
                    Bullet newBullet = Instantiate(bullet, shootPoint, Quaternion.identity);
                    newBullet.AssignGun(this);

                    newBullet.startPos = shootPoint;
                    newBullet.rb.velocity = Vector2.right * speed * direction;
                    break;
                }
            case (ShotType.SPREAD):
                {
                    //Fire the shots off in different angles
                    float angleBetweenBullets = spreadAngle / (numberOfBullets - 1);
                    float fireAngle = spreadAngle / 2f;

                    Bullet[] newBullets = new Bullet[numberOfBullets];

                    for (int i = 0; i < numberOfBullets; i++)
                    {
                        newBullets[i] = Instantiate(bullet, shootPoint, Quaternion.identity);
                        newBullets[i].AssignGun(this);
                        newBullets[i].startPos = shootPoint;

                        newBullets[i].transform.Rotate(0f, 0f, fireAngle);
                        fireAngle -= angleBetweenBullets;

                        newBullets[i].rb.velocity = newBullets[i].transform.right * speed * direction;
                    }

                    break;
                }
            default:
                {
                    Bullet newBullet = Instantiate(bullet, shootPoint, Quaternion.identity);
                    newBullet.AssignGun(this);

                    newBullet.startPos = shootPoint;
                    newBullet.rb.velocity = Vector2.right * speed * direction;
                    break;
                }
        }
    }
}
