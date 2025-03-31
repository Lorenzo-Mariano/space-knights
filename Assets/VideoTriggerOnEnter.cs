using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoTriggerOnEnter : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName; // Assign the next scene name in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && videoPlayer != null)
        {
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoFinished; // Event to detect when video ends
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("Menu"); // Load the next scene
    }
}
