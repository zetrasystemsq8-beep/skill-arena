using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zetra.ClashDash
{
    /// <summary>
    /// Final lightweight runtime coordinator for CLASHDASH.
    /// Uses the systems already present in the project instead of creating
    /// another gameplay architecture.
    /// </summary>
    public class ClashDashFinalGame : MonoBehaviour
    {
        public static ClashDashFinalGame Instance { get; private set; }

        [Header("Mobile")]
        [SerializeField] private bool forcePortrait = true;
        [SerializeField] private int targetFrameRate = 60;

        [Header("Performance")]
        [SerializeField] private bool disableVSync = true;

        [Header("Scene")]
        [SerializeField] private bool keepAlive = true;

        private bool paused;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            if (keepAlive)
                DontDestroyOnLoad(gameObject);

            ConfigureDevice();
        }

        private void Start()
        {
            Time.timeScale = 1f;
            paused = false;

            ConfigureScene();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Time.timeScale = 1f;
            paused = false;
            ConfigureScene();
        }

        private void ConfigureDevice()
        {
            Application.targetFrameRate = Mathf.Max(30, targetFrameRate);

            if (disableVSync)
                QualitySettings.vSyncCount = 0;

#if UNITY_ANDROID || UNITY_IOS
            if (forcePortrait)
            {
                Screen.orientation = ScreenOrientation.Portrait;
                Screen.autorotateToPortrait = true;
                Screen.autorotateToPortraitUpsideDown = false;
                Screen.autorotateToLandscapeLeft = false;
                Screen.autorotateToLandscapeRight = false;
            }
#endif
        }

        private void ConfigureScene()
        {
            // Make sure the main camera is active.
            Camera mainCamera = Camera.main;

            if (mainCamera != null)
            {
                mainCamera.enabled = true;

                AudioListener[] listeners =
                    FindObjectsByType<AudioListener>(
                        FindObjectsInactive.Include,
                        FindObjectsSortMode.None
                    );

                bool mainListenerKept = false;

                foreach (AudioListener listener in listeners)
                {
                    if (!listener.enabled)
                        continue;

                    if (!mainListenerKept && listener.gameObject == mainCamera.gameObject)
                    {
                        mainListenerKept = true;
                        continue;
                    }

                    if (!mainListenerKept)
                    {
                        mainListenerKept = true;
                        continue;
                    }

                    listener.enabled = false;
                }
            }

            // Keep gameplay running whenever a new arena scene loads.
            Time.timeScale = 1f;
        }

        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePause();
#endif
        }

        public void TogglePause()
        {
            if (paused)
                Resume();
            else
                Pause();
        }

        public void Pause()
        {
            paused = true;
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            paused = false;
            Time.timeScale = 1f;
        }

        public bool IsPaused()
        {
            return paused;
        }

        public void RestartArena()
        {
            Time.timeScale = 1f;
            paused = false;

            Scene activeScene = SceneManager.GetActiveScene();

            if (activeScene.buildIndex >= 0)
                SceneManager.LoadScene(activeScene.buildIndex);
        }

        public void LoadArena(string sceneName)
        {
            if (string.IsNullOrWhiteSpace(sceneName))
                return;

            Time.timeScale = 1f;
            paused = false;

            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
