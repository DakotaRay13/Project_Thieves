using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button btn_Resume;

    private void Start()
    {
        FirstEnable();
    }

    public void FirstEnable()
    {
        btn_Resume.Select();
    }

    public void ResumeGame()
    {
        Debug.Log("Resume Game pressed.");
        FindObjectOfType<GameManager>().PauseGame(false);
    }

    public void Retry()
    {
        FindObjectOfType<GameManager>().PauseGame(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
    {
        FindObjectOfType<GameManager>().PauseGame(false);
        SceneManager.LoadScene("ThanksForPlaying");
    }
}
