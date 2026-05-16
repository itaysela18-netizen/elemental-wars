using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Central game state manager for Elemental Wars
/// Handles game initialization, state management, and event coordination
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private bool isPaused = false;
    [SerializeField] private float gameSpeed = 1f;
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int playerExperience = 0;

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

    private void Start()
    {
        Debug.Log("GameManager initialized");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        Debug.Log($"Game {(isPaused ? "paused" : "resumed")}");
    }

    /// <summary>
    /// Sets game speed (for slow-motion effects)
    /// </summary>
    public void SetGameSpeed(float speed)
    {
        gameSpeed = speed;
        Time.timeScale = speed;
    }

    /// <summary>
    /// Adds experience to player
    /// </summary>
    public void AddExperience(int amount)
    {
        playerExperience += amount;
        Debug.Log($"Experience gained: {amount}. Total: {playerExperience}");
    }

    /// <summary>
    /// Loads a new scene
    /// </summary>
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // Resume time before loading
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Gets current game state
    /// </summary>
    public bool IsPaused => isPaused;
    public int CurrentLevel => currentLevel;
    public int PlayerExperience => playerExperience;
}
