using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    public Button btn_Retry;

    private void Start()
    {
        FirstEnable();
    }

    public void FirstEnable()
    {
        btn_Retry.Select();
    }

    public void RetryButton()
    {
        Debug.Log("Retry Clicked");
    }

    public void QuitToMenu()
    {
        Debug.Log("Quit clicked");
    }
}
