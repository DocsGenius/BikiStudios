using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Simple Singleton pattern
    public static TimeManager Instance { get; private set; }

    [Header("Settings")]
    public float CustomTimeScale = 0.1f;
    
    // Auto-property: readable by all, writable only by this script
    public float CurrentTime { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Use Time.unscaledDeltaTime if you want this clock to keep 
        // ticking even when the game is paused (Time.timeScale = 0)
        CurrentTime += Time.unscaledDeltaTime * CustomTimeScale;
    }

    public void SetGlobalTimeScale(float scale)
    {
        Time.timeScale = Mathf.Max(0, scale); // Prevent negative time
    }

    public void PauseGame(bool isPaused)
    {
        Time.timeScale = 0f;
    }
}