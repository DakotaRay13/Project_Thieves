using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 startPos;
    public float speed;
    public float damage;
    public float range;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            DestroyBullet();
        }

        if(collision.tag == "Enemy")
        {
            Debug.Log("Enemy Hit!");
            collision.gameObject.GetComponent<Base_Enemy>().TakeDamage(damage);
            DestroyBullet();
        }
    }

    private void Update()
    {
        //Check the distance of the bullet each frame. When it reaches or passes it's range, destroy it.
        if (Vector2.Distance(transform.position, startPos) >= range)
            DestroyBullet();
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
