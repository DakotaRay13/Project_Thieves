using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject garrettUI;
    public GameObject alexUI;

    public GameObject pickUp_Health;
    public GameObject pickUp_Stamina;
    public GameObject pickUp_Ammo;
    
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
    
    public void DropPickUp(Vector3 spawnLocation)
    {
        int RNGesus = Random.Range(0, 100);

        Debug.Log("Pick Up Chance: " + RNGesus);

        if(RNGesus <= 50)
        {
            //Spawn Nothing
            return;
        }
        else if (RNGesus > 50 && RNGesus <= 75)
        {
            //Spawn Health
            Instantiate(pickUp_Health, spawnLocation, pickUp_Health.transform.rotation);
        }
        else
        {
            //Spawn Stamina or Ammo
            Player player = FindObjectOfType<Player>();
            if (player.name == "PlayerChar_Garrett")
            {
                Instantiate(pickUp_Ammo, spawnLocation, pickUp_Ammo.transform.rotation);
            }
            else if (player.name == "PlayerChar_Alex")
            {
                Instantiate(pickUp_Stamina, spawnLocation, pickUp_Stamina.transform.rotation);
            }
        }
    }
}
