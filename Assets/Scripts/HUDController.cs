using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("HUD Text")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text arenaText;
    [SerializeField] private TMP_Text comboText;

    [Header("Progress")]
    [SerializeField] private Slider challengeProgress;

    [Header("Animation")]
    [SerializeField] private float pulseSpeed = 5f;
    [SerializeField] private float pulseAmount = 0.04f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;

        SetArenaName("THE TEST");
        SetCombo(0);
    }

    private void Update()
    {
        UpdateGameData();
        AnimateHUD();
    }

    private void UpdateGameData()
    {
        if (GameManager.Instance == null)
            return;

        if (scoreText != null)
            scoreText.text = GameManager.Instance.score.ToString("000000");

        if (timerText != null)
        {
            float time = GameManager.Instance.GetTimeRemaining();
            timerText.text = Mathf.CeilToInt(time).ToString("00");
        }
    }

    public void SetArenaName(string arenaName)
    {
        if (arenaText != null)
            arenaText.text = arenaName.ToUpper();
    }

    public void SetCombo(int combo)
    {
        if (comboText != null)
        {
            comboText.text = combo > 1
                ? "COMBO  x" + combo
                : "";
        }
    }

    public void SetProgress(float value)
    {
        if (challengeProgress != null)
            challengeProgress.value = Mathf.Clamp01(value);
    }

    public void Pulse()
    {
        StopAllCoroutines();
        StartCoroutine(PulseEffect());
    }

    private System.Collections.IEnumerator PulseEffect()
    {
        float duration = 0.12f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float scale = 1f +
                Mathf.Sin((elapsed / duration) * Mathf.PI) * pulseAmount;

            transform.localScale = originalScale * scale;

            yield return null;
        }

        transform.localScale = originalScale;
    }

    private void AnimateHUD()
    {
        float glow = 1f +
            Mathf.Sin(Time.time * pulseSpeed) * 0.01f;

        transform.localScale = originalScale * glow;
    }
}
