using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public GameObject EndOfLevelPanel;
    public string nextScene;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            FinishLevel();
        }
    }

    public void FinishLevel()
    {
        GameObject canvas = FindObjectOfType<Canvas>().gameObject;
        GameObject eol = Instantiate(EndOfLevelPanel, canvas.transform);
        eol.GetComponent<EndLevel>().nextScene = nextScene;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }
}
