using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexAttack : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            col.gameObject.GetComponent<Base_Enemy>().TakeDamage(damage);
        }

        else if (col.tag == "Destructable")
        {
            GetComponentInParent<SFX_Manager_Alex>().PlayAudio2("boom");
        }
    }
}
