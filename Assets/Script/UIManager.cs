using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject healthBarPrefab; // Assign in Inspector
    private GameObject currentHealthBar;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep UI Manager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnHealthBar()
    {
        if (currentHealthBar == null)
        {
            currentHealthBar = Instantiate(
                healthBarPrefab,
                FindFirstObjectByType<Canvas>().transform
            );
        }
    }
}
