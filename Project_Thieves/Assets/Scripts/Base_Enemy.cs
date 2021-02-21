using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Enemy : MonoBehaviour
{
    public float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
