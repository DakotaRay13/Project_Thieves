using UnityEngine;
using Cinemachine;

public class RoomCamBarrier : MonoBehaviour
{
    //When the player enters the room, it activates. When they exit, it deactivates.

    public GameObject virtualCam;

    private void Start()
    {
        virtualCam.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);
        }
    }
}
