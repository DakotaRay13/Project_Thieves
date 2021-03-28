using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            FindObjectOfType<GameManager>().UpdateCheckpoint(this);
            DeactivateCheckpoint();
        }
    }

    public void DeactivateCheckpoint()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
