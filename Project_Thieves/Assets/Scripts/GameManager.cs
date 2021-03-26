﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject garrettUI;
    public GameObject alexUI;

    public static GameObject CHOSEN_CHARACTER;
    public GameObject player_Alex;
    public GameObject player_Garrett;
    public bool playAsGarrett;

    public GameObject pickUp_Health;
    public GameObject pickUp_Stamina;
    public GameObject pickUp_Ammo;

    public GameObject pauseMenu;
    public bool paused;

    public LayerMask[] deathLayers;
    public GameObject deathUI;

    void Start()
    {
        //Load Player
        LoadPlayer();
        
        //Load the correct UI
        LoadPlayerUI();
    }

    public void LoadPlayer()
    {
        if(FindObjectOfType<Player>())
        {
            Debug.Log("Player already in scene");
            return;
        }

        Vector3 spawnPoint = GameObject.Find("SpawnPoint").transform.position;

        if(CHOSEN_CHARACTER == null)
        {
            if (playAsGarrett)
            {
                Instantiate(player_Garrett, spawnPoint, player_Garrett.transform.rotation);
                Debug.Log("Chosen Character is null. Garrett used in place.");
            }

            else
            {
                Instantiate(player_Alex, spawnPoint, player_Alex.transform.rotation);
                Debug.Log("Chosen Character is null. Alex used in place.");
            }
            
        }

        else
        {
            Instantiate(CHOSEN_CHARACTER, spawnPoint, CHOSEN_CHARACTER.transform.rotation);
        }
    }

    void LoadPlayerUI()
    {
        Player player = FindObjectOfType<Player>();
        GameObject canvas = FindObjectOfType<Canvas>().gameObject;

        if (player.GetComponent<Player_Garrett>())
        {
            Instantiate(garrettUI, canvas.transform);
        }
        else if (player.GetComponent<Player_Alex>())
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

    public void PauseGame(bool set)
    {
        GameObject canvas = FindObjectOfType<Canvas>().gameObject;

        //Pause the Game
        if (set && !paused)
        {
            Time.timeScale = 0f;
            Instantiate(pauseMenu, canvas.transform);
            paused = set;
        }

        //Unpause the game
        else if (!set && paused)
        {
            Time.timeScale = 1f;
            Destroy(GameObject.Find("PauseMenuPanel(Clone)"));
            FindObjectOfType<Player>().SetInputToPlatforming();
            paused = set;
        }
    }

    public void Death()
    {
        FindObjectOfType<Camera>().cullingMask = deathLayers[0] + deathLayers[1];

        if (GameObject.Find("PlayerUI_Garrett(Clone)")) GameObject.Find("PlayerUI_Garrett(Clone)").SetActive(false);
        else if (GameObject.Find("PlayerUI_Alex(Clone)"))  GameObject.Find("PlayerUI_Alex(Clone)").SetActive(false);
    }

    public void CreateDeathUI()
    {
        GameObject canvas = FindObjectOfType<Canvas>().gameObject;
        Instantiate(deathUI, canvas.transform);
    }
}
