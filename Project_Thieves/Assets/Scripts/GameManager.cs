using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject garrettUI;
    public GameObject alexUI;
    // Start is called before the first frame update
    void Start()
    {
        //Load the correct UI
        LoadPlayerUI();
    }

    void LoadPlayerUI()
    {
        Player player = FindObjectOfType<Player>();
        GameObject canvas = FindObjectOfType<Canvas>().gameObject;

        if (player.name == "PlayerChar_Garrett")
        {
            Instantiate(garrettUI, canvas.transform);
        }
        else if (player.name == "PlayerChar_Alex")
        {
            Instantiate(alexUI, canvas.transform);
        }
    }
}
