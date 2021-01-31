using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    private GameObject player;      //Where the player is in the scene
    private float camOffset;        //The base offset of how far away the camera is from the player
    private float yOffset;          //Offset the position of the camera for the linear interpolation
    private float lookOffset;       //Offset for the camera looking forward
    private float timeOffset;       //How fast the camera interpolates

    public BoxCollider2D cameraBox;//Use for camera boundaries
    [SerializeField]
    Vector2 ar_16_10,               //Aspect Ratios
        ar_16_9,
        ar_3_2;

    void Start()
    {
        //Set the variables
        player = GameObject.FindGameObjectWithTag("Player");
        camOffset = -15f;
        yOffset = 0.5f;
        lookOffset = 4.5f;
        timeOffset = 3.5f;

        cameraBox = GetComponent<BoxCollider2D>();
        ar_16_10 = new Vector2(23f, 14.3f);
        ar_16_9 = new Vector2(25.47f, 14.3f);
        ar_3_2 = new Vector2(21.6f, 14.3f);
        AspectRatioBox();

        //Set the camera position
        transform.position = player.transform.position + new Vector3(0f, yOffset, camOffset);
        ClampToBoundary();
    }

    void Update()
    {
        CameraMovement();
    }

    //Move the camera
    public void CameraMovement()
    {
        //Camera's start position
        Vector3 startPos = transform.position;

        //Player's Current Position
        Vector3 endPos = player.transform.position;
        endPos.x += CalculateLookForward();
        endPos.y += yOffset;
        endPos.z = camOffset;

        //if (Mathf.Abs(startPos.x - endPos.x) < 0.05f) transform.position = startPos;
        //else 
        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);
        ClampToBoundary();
    }

    public float CalculateLookForward()
    {
        return player.GetComponent<PlayerController>().GetMoveInput() * lookOffset;
    }

    void ClampToBoundary()
    {
        if(GameObject.Find("Boundary"))
        {
            transform.position = new Vector3
            (
                Mathf.Clamp(player.transform.position.x, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.x, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.x),
                Mathf.Clamp(player.transform.position.y, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.y, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.y),
                transform.position.z
            );
        }
    }

    void AspectRatioBox()
    {
        //3:2
        if (Camera.main.aspect >= 1.5f && Camera.main.aspect < 1.6f)
            cameraBox.size = ar_3_2;
        
        //16:10
        if (Camera.main.aspect >= 1.6f && Camera.main.aspect < 1.7f)
            cameraBox.size = ar_16_10;

        //16:9
        if (Camera.main.aspect >= 1.7f && Camera.main.aspect < 1.8f)
            cameraBox.size = ar_16_9;
    }
}
