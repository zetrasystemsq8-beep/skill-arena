using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClashDashMobileHUD : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text comboText;
    [SerializeField] private TMP_Text checkpointText;
    [SerializeField] private TMP_Text statusText;

    [Header("Progress")]
    [SerializeField] private Slider progressBar;

    [Header("Mobile")]
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject jumpButton;

    private ArenaCoreController arena;

    private void Start()
    {
        arena =
            FindFirstObjectByType<
                ArenaCoreController
            >();

        SetStatus("CONNECT // INITIALIZING");
    }

    private void Update()
    {
        if (arena == null)
            return;

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (scoreText != null)
        {
            scoreText.text =
                "SCORE  " +
                arena.Score.ToString("000000");
        }

        if (timerText != null)
        {
            timerText.text =
                Mathf.CeilToInt(
                    arena.TimeRemaining
                ).ToString("00");
        }

        if (comboText != null)
        {
            comboText.text =
                arena.Combo > 1
                ? "COMBO  x" + arena.Combo
                : "";
        }

        if (checkpointText != null)
        {
            checkpointText.text =
                "CHECKPOINT  " +
                arena.Checkpoints +
                " / 5";
        }

        if (progressBar != null)
        {
            progressBar.value =
                arena.GetCheckpointProgress();
        }

        switch (arena.State)
        {
            case ArenaCoreController.ArenaState.Loading:
                SetStatus("ZETRA // LOADING");
                break;

            case ArenaCoreController.ArenaState.Countdown:
                SetStatus("GET READY");
                break;

            case ArenaCoreController.ArenaState.Running:
                SetStatus("CLASHDASH // LIVE");
                break;

            case ArenaCoreController.ArenaState.Complete:
                SetStatus("ARENA CLEARED");
                break;

            case ArenaCoreController.ArenaState.Failed:
                SetStatus("RUN FAILED");
                break;
        }
    }

    private void SetStatus(string value)
    {
        if (statusText != null)
            statusText.text = value;
    }

    public void HideControls()
    {
        if (joystick != null)
            joystick.SetActive(false);

        if (jumpButton != null)
            jumpButton.SetActive(false);
    }

    public void ShowControls()
    {
        if (joystick != null)
            joystick.SetActive(true);

        if (jumpButton != null)
            jumpButton.SetActive(true);
    }
}
