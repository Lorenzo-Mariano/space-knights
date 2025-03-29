using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using UnityEngine.UI; // For UI elements

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Reference to the Game Over Panel
    public Button retryButton; // Reference to the Retry button
    public Button menuButton; // Reference to the Quit button

    void Start()
    {
        // Initially hide the Game Over panel
        gameOverPanel.SetActive(false);

        // Add listeners for the buttons
        retryButton.onClick.AddListener(ReloadScene);
    	menuButton.onClick.AddListener(MainMenu);
    }

    public void ShowGameOverScreen()
    {
        // Display the Game Over panel
        gameOverPanel.SetActive(true);
        
        // Pause the game
        Time.timeScale = 0f;
    }

    public void ReloadScene()
    {
        // Restart the scene (Reload the current scene)
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void MainMenu()
    {
        // Restart the scene (Reload the current scene)
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene("Menu"); 
    }
}
