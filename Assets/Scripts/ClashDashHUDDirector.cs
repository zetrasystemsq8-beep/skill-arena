using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ClashDashHUDDirector : MonoBehaviour
{
    [Header("Core HUD")]
    [SerializeField] private TMP_Text gameTitle;
    [SerializeField] private TMP_Text arenaLabel;
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private TMP_Text timerLabel;
    [SerializeField] private TMP_Text comboLabel;
    [SerializeField] private TMP_Text checkpointLabel;
    [SerializeField] private TMP_Text statusLabel;

    [Header("Progress")]
    [SerializeField] private Slider arenaProgress;
    [SerializeField] private Slider checkpointProgress;

    [Header("Panels")]
    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject failurePanel;

    [Header("Countdown")]
    [SerializeField] private TMP_Text countdownLabel;

    [Header("Identity")]
    [SerializeField] private string gameName = "CLASHDASH";
    [SerializeField] private string arenaName = "THE TEST";
    [SerializeField] private string creatorTag = "CONNECT // ZETRA";

    [Header("Animation")]
    [SerializeField] private float pulseSpeed = 5f;
    [SerializeField] private float pulseAmount = 0.025f;

    private ArenaCoreController arena;
    private Vector3 originalScale;

    private void Start()
    {
        arena = FindFirstObjectByType<ArenaCoreController>();

        originalScale = transform.localScale;

        SetIdentity();

        if (countdownPanel != null)
            countdownPanel.SetActive(false);

        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        if (failurePanel != null)
            failurePanel.SetActive(false);
    }

    private void Update()
    {
        if (arena == null)
            return;

        UpdateArenaData();
        AnimateHUD();
    }

    private void SetIdentity()
    {
        if (gameTitle != null)
            gameTitle.text = gameName;

        if (arenaLabel != null)
            arenaLabel.text = arenaName;

        if (statusLabel != null)
            statusLabel.text = creatorTag;
    }

    private void UpdateArenaData()
    {
        if (scoreLabel != null)
        {
            scoreLabel.text =
                arena.Score.ToString("000000");
        }

        if (timerLabel != null)
        {
            int seconds =
                Mathf.CeilToInt(arena.TimeRemaining);

            timerLabel.text =
                seconds.ToString("00");
        }

        if (comboLabel != null)
        {
            comboLabel.text =
                arena.Combo > 1
                ? "COMBO  x" + arena.Combo
                : "";
        }

        if (checkpointLabel != null)
        {
            checkpointLabel.text =
                "CHECKPOINT  " +
                arena.Checkpoints +
                " / " +
                5;
        }

        if (arenaProgress != null)
            arenaProgress.value =
                arena.GetProgress();

        if (checkpointProgress != null)
            checkpointProgress.value =
                arena.GetCheckpointProgress();

        UpdateStatus();
    }

    private void UpdateStatus()
    {
        if (statusLabel == null)
            return;

        switch (arena.State)
        {
            case ArenaCoreController.ArenaState.Loading:
                statusLabel.text =
                    "ZETRA SYSTEMS // LOADING";
                break;

            case ArenaCoreController.ArenaState.Countdown:
                statusLabel.text =
                    "CONNECT // READY";
                break;

            case ArenaCoreController.ArenaState.Running:
                statusLabel.text =
                    "CLASHDASH // LIVE";
                break;

            case ArenaCoreController.ArenaState.Complete:
                statusLabel.text =
                    "ARENA CLEARED";
                break;

            case ArenaCoreController.ArenaState.Failed:
                statusLabel.text =
                    "SYSTEM RESET REQUIRED";
                break;
        }
    }

    private void AnimateHUD()
    {
        float pulse =
            1f +
            Mathf.Sin(Time.time * pulseSpeed) *
            pulseAmount;

        transform.localScale =
            originalScale * pulse;
    }

    public void ShowCountdown(string value)
    {
        if (countdownPanel != null)
            countdownPanel.SetActive(true);

        if (countdownLabel != null)
            countdownLabel.text = value;
    }

    public void HideCountdown()
    {
        if (countdownPanel != null)
            countdownPanel.SetActive(false);
    }

    public void ShowVictory()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        StartCoroutine(VictoryPulse());
    }

    public void ShowFailure()
    {
        if (failurePanel != null)
            failurePanel.SetActive(true);
    }

    private IEnumerator VictoryPulse()
    {
        Vector3 startScale = transform.localScale;

        for (int i = 0; i < 3; i++)
        {
            transform.localScale =
                startScale * 1.08f;

            yield return new WaitForSeconds(0.08f);

            transform.localScale =
                startScale;

            yield return new WaitForSeconds(0.08f);
        }
    }
}
