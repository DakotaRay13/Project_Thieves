using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Enemy : MonoBehaviour
{
    public float health;
    public int damage;
    private float damageThisFrame;
    private bool dead = false;

    private void Update()
    {
        StartCoroutine(ResetDamageThisFrame());
    }

    public IEnumerator ResetDamageThisFrame()
    {
        yield return new WaitForEndOfFrame();

        damageThisFrame = 0f;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        damageThisFrame += damage;

        if(health <= 0f && !dead)
        {
            dead = true;
            Destroy(gameObject);
            FindObjectOfType<GameManager>().DropPickUp(transform.position);
        }
        else if (damageThisFrame >= 5)
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

            col.gameObject.GetComponent<Player>().TakeDamage(damage, transform.position.x);
        }
    }
}
