using System.Collections;
using UnityEngine;
using Cinemachine;

public class RoomCamBarrier : MonoBehaviour
{
    //When the player enters the room, it activates. When they exit, it deactivates.

    public GameObject virtualCam;
    public GameObject EnemiesInRoom;

    private void Start()
    {
        StartCoroutine(GetPlayer());
        EnemiesInRoom.SetActive(false);
    }

    public IEnumerator GetPlayer()
    {
        yield return new WaitForEndOfFrame();
        virtualCam.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);
            EnemiesInRoom.SetActive(true);
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
