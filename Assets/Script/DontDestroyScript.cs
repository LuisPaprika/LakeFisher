using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    public static DontDestroyScript Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Prevent duplicate
        }
    }
}
