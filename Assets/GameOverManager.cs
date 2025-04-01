using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("GameOverManager started.");
    }

    public void ShowGameOverScreen()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
