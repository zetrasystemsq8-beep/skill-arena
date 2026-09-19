using UnityEngine;
using UnityEngine.SceneManagement;

public class ClashDashBootstrap : MonoBehaviour
{
    public static ClashDashBootstrap Instance;

    [Header("Game Identity")]
    [SerializeField] private string gameName = "CLASHDASH";
    [SerializeField] private string studioName = "ZETRA";
    [SerializeField] private string creatorName = "CONNECT";

    [Header("Starting Arena")]
    [SerializeField] private string firstArena = "Arena_01";

    [Header("Systems")]
    [SerializeField] private GameObject progressionSystem;
    [SerializeField] private GameObject saveSystem;
    [SerializeField] private GameObject audioSystem;
    [SerializeField] private GameObject performanceSystem;

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
            return;
        }

        InitializeGame();
    }

    private void InitializeGame()
    {
        Application.targetFrameRate = 60;

        Debug.Log(
            "================================"
        );

        Debug.Log(
            gameName +
            " // " +
            studioName
        );

        Debug.Log(
            "CREATED BY " +
            creatorName
        );

        Debug.Log(
            "SYSTEM STATUS: ONLINE"
        );

        Debug.Log(
            "================================"
        );

        SpawnSystem(progressionSystem);
        SpawnSystem(saveSystem);
        SpawnSystem(audioSystem);
        SpawnSystem(performanceSystem);
    }

    private void SpawnSystem(GameObject system)
    {
        if (system == null)
            return;

        GameObject instance =
            Instantiate(system);

        DontDestroyOnLoad(instance);
    }

    public void LaunchFirstArena()
    {
        SceneManager.LoadScene(firstArena);
    }

    public string GetGameName()
    {
        return gameName;
    }

    public string GetStudioName()
    {
        return studioName;
    }

    public string GetCreatorName()
    {
        return creatorName;
    }
}
