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
        btn_Retry.GetComponent<Animator>().SetTrigger("Selected");
    }

    public void RetryButton()
    {
        StartCoroutine(ChangeLevel(SceneManager.GetActiveScene().name));
    }

    public void QuitToMenu()
    {
        StartCoroutine(ChangeLevel("ThanksForPlaying"));
    }

    public IEnumerator ChangeLevel(string Level)
    {
        GameObject.Find("ScreenTransition").GetComponent<Animator>().Play("FadeScreenOut");

        yield return new WaitForSeconds(1.3f);

        SceneManager.LoadScene(Level);
    }
}
