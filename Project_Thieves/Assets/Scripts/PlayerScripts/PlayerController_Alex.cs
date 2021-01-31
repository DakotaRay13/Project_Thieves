using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Alex : MonoBehaviour
{
    BasePlayerController pc;
    PlayerInputControls inputActions;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<BasePlayerController>();
        inputActions = pc.GetInputActions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
