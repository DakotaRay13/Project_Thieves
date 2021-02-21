using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("Collision Detected: " + collision.name);
            Destroy(gameObject);
        }
    }
}
