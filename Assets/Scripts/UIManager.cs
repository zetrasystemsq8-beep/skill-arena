using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text timerText;

    private void Update()
    {
        UpdateScore();
        UpdateTimer();
    }

    private void UpdateScore()
    {
        if (scoreText == null || GameManager.Instance == null)
            return;

        scoreText.text = "SCORE  " + GameManager.Instance.score;
    }

    private void UpdateTimer()
    {
        if (timerText == null || GameManager.Instance == null)
            return;

        float time = GameManager.Instance.GetTimeRemaining();

        timerText.text = "TIME  " + Mathf.CeilToInt(time);
    }
}
