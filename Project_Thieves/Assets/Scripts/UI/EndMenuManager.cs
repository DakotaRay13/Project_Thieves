using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuManager : MonoBehaviour
{
    public void GoToMainMenu()
    {
        StartCoroutine(ChangeLevel());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator ChangeLevel()
    {
        GameObject.Find("ScreenTransition").GetComponent<Animator>().Play("FadeScreenOut");

        yield return new WaitForSeconds(1.3f);

        SceneManager.LoadScene("MainMenu");
    }
}
