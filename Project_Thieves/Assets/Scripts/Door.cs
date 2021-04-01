using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Player_Garrett>())
        {
            anim.SetTrigger("OpenDoor");
        }
    }
}
