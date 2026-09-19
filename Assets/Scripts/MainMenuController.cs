using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] private string arenaScene = "Arena_01";

    [Header("UI Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        ShowMainPanel();
    }

    public void Play()
    {
        SceneManager.LoadScene(arenaScene);
    }

    public void OpenSettings()
    {
        if (mainPanel != null)
            mainPanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        ShowMainPanel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ShowMainPanel()
    {
        if (mainPanel != null)
            mainPanel.SetActive(true);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
}
