using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int damage;

    private void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("Collision Detected: " + col.tag);
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().TakeHazardDamage(damage);
        }
    }
}
