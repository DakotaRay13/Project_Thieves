using System.Collections;
using UnityEngine;

public class PickUp_Health : MonoBehaviour
{
    int healthToGive = 15;

    public IEnumerator LiveTimer()
    {
        yield return new WaitForSeconds(60f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if(col.GetComponent<Player>().HEALTH < col.GetComponent<Player>().MAX_HEALTH)
            {
                col.GetComponent<Player>().HEALTH = Mathf.Clamp(col.GetComponent<Player>().HEALTH + healthToGive, 0, col.GetComponent<Player>().MAX_HEALTH);
                Destroy(this.gameObject);
            }
        }
    }
}
