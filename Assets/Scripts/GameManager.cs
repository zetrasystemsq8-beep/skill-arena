using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score;
    public int coins;
    public float challengeTime = 60f;

    private float timeRemaining;
    private bool challengeActive;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartChallenge();
    }

    private void Update()
    {
        if (!challengeActive)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            EndChallenge();
        }
    }

    public void StartChallenge()
    {
        score = 0;
        timeRemaining = challengeTime;
        challengeActive = true;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    private void EndChallenge()
    {
        challengeActive = false;
        Debug.Log("Challenge Complete! Score: " + score);
    }
}
