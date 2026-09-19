using UnityEngine;
using UnityEngine.SceneManagement;

public class ClashDashGameFlow : MonoBehaviour
{
    public static ClashDashGameFlow Instance;

    [Header("Scenes")]
    [SerializeField] private string menuScene = "MainMenu";
    [SerializeField] private string arenaScene = "Arena_01";

    [Header("Systems")]
    [SerializeField] private GameObject pausePanel;

    private bool paused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(arenaScene);
    }

    public void OpenMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuScene);
    }

    public void TogglePause()
    {
        paused = !paused;

        Time.timeScale =
            paused ? 0f : 1f;

        if (pausePanel != null)
            pausePanel.SetActive(paused);
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1f;

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void RestartArena()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    private void OnApplicationPause(
        bool applicationPaused
    )
    {
        if (applicationPaused &&
            !paused)
        {
            TogglePause();
        }
    }
}
