using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    private static HealthBarManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevents this object from being destroyed
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }
}
