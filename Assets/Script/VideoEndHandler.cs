using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // If you want to load a new scene

public class VideoEndHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign this in the Inspector
    public int sceneBuildIndex = 1; // Set this in the Inspector for flexibility

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>(); // Try to get VideoPlayer component
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished; // Subscribe to event
        }
        else
        {
            Debug.LogError("No VideoPlayer found! Assign one in the Inspector.");
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video has finished playing.");

        // Load a new scene using the build index
        SceneManager.LoadScene(sceneBuildIndex);

        // Example Action: Disable VideoPlayer
        // videoPlayer.gameObject.SetActive(false);
    }
}
