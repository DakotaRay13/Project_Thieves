using System.Collections;
using UnityEngine;

public class PickUp_Stamina : MonoBehaviour
{
    int staminaToGive = 10;

    public IEnumerator LiveTimer()
    {
        yield return new WaitForSeconds(60f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (col.GetComponent<Player_Alex>())
            {
                if(col.GetComponent<Player_Alex>().STAMINA < col.GetComponent<Player_Alex>().MAX_STAMINA)
                {
                    col.GetComponent<Player_Alex>().STAMINA = Mathf.Clamp(col.GetComponent<Player_Alex>().STAMINA + staminaToGive, 0, col.GetComponent<Player_Alex>().MAX_STAMINA);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
