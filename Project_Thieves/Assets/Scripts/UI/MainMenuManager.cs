﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Button btn_StartGame;
    public Button btn_Alex;
    public Button btn_Back;

    public GameObject startMenu;
    public GameObject characterSelect;
    public GameObject howToPlayMenu;

    private void Start()
    {
        BackButton();
    }

    public void FirstEnable(Button first)
    {
        first.Select();
    }

    public void StartGame()
    {
        //Go to Character Select
        startMenu.SetActive(false);
        characterSelect.SetActive(true);
        FirstEnable(btn_Alex);
    }

    public void HowToPlay()
    {
        //Go to How to Play
        startMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
        FirstEnable(btn_Back);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectCharacter(GameObject character)
    {
        GameManager.CHOSEN_CHARACTER = character;
        SceneManager.LoadScene("DemoLevel");
    }

    public void BackButton()
    {
        startMenu.SetActive(true);
        characterSelect.SetActive(false);
        howToPlayMenu.SetActive(false);
        FirstEnable(btn_StartGame);
    }
}
