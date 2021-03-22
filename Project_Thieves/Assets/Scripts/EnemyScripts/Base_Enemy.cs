using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Enemy : MonoBehaviour
{
    public float health;
    public int damage;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0f)
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().DropPickUp(transform.position);
        }
        else if (damage >= 5)
        {
            if(GetComponent<EnemyBehaviour>())
            {
                StartCoroutine(GetComponent<EnemyBehaviour>().HitStun(0.25f));
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if (col.gameObject.GetComponent<Player_Alex>())
                if (col.gameObject.GetComponent<Player_Alex>().isBlocking)
                {
                    StartCoroutine(GetComponent<EnemyBehaviour>().HitStun(0.35f));
                }

            col.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
