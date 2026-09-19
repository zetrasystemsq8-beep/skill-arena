using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClashDashResultsController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private TMP_Text resultTitle;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text rewardText;

    [Header("Next Arena")]
    [SerializeField] private string nextArena = "Arena_02";

    private ArenaCoreController arena;

    private void Start()
    {
        arena =
            FindFirstObjectByType<
                ArenaCoreController
            >();

        if (resultsPanel != null)
            resultsPanel.SetActive(false);
    }

    private void Update()
    {
        if (arena == null)
            return;

        if (arena.IsComplete())
        {
            ShowResults(true);
        }
        else if (arena.IsFailed())
        {
            ShowResults(false);
        }
    }

    private void ShowResults(bool victory)
    {
        if (resultsPanel != null)
            resultsPanel.SetActive(true);

        if (resultTitle != null)
        {
            resultTitle.text =
                victory
                ? "ARENA CLEARED"
                : "RUN FAILED";
        }

        if (scoreText != null)
        {
            scoreText.text =
                "SCORE  " +
                arena.Score.ToString("000000");
        }

        if (rewardText != null)
        {
            rewardText.text =
                victory
                ? "ZETRA REWARD // UNLOCKED"
                : "RETRY // IMPROVE YOUR RUN";
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void NextArena()
    {
        SceneManager.LoadScene(nextArena);
    }
}
