using System.Collections;
using UnityEngine;

public class PickUp_Ammo : MonoBehaviour
{
    int ammoToGive = 10;

    private void Awake()
    {
        StartCoroutine(LiveTimer());
    }

    public IEnumerator LiveTimer()
    {
        yield return new WaitForSeconds(60f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if(col.GetComponent<Player_Garrett>())
            {
                if(col.GetComponent<Player_Garrett>().AMMO < col.GetComponent<Player_Garrett>().MAX_AMMO)
                {
                    col.GetComponent<Player_Garrett>().AMMO = Mathf.Clamp(col.GetComponent<Player_Garrett>().AMMO + ammoToGive, 0, col.GetComponent<Player_Garrett>().MAX_AMMO);
                    Destroy(this.gameObject);
                }
                
            }
        }
    }
}
