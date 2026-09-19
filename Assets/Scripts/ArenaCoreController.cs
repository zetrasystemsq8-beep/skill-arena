using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ArenaCoreController : MonoBehaviour
{
    public enum ArenaState
    {
        Loading,
        Countdown,
        Running,
        Complete,
        Failed
    }

    [Header("Arena Identity")]
    [SerializeField] private string arenaName = "THE TEST";
    [SerializeField] private int arenaNumber = 1;

    [Header("Challenge")]
    [SerializeField] private float challengeDuration = 60f;
    [SerializeField] private int requiredCheckpoints = 5;
    [SerializeField] private int minimumScore = 1000;

    [Header("Rewards")]
    [SerializeField] private int completionXP = 250;
    [SerializeField] private int completionCoins = 100;
    [SerializeField] private int perfectBonusXP = 150;
    [SerializeField] private int perfectBonusCoins = 75;

    [Header("Countdown")]
    [SerializeField] private float countdownDuration = 3f;

    [Header("Events")]
    public UnityEvent onArenaStarted;
    public UnityEvent onArenaCompleted;
    public UnityEvent onArenaFailed;
    public UnityEvent onCheckpointReached;

    public ArenaState State { get; private set; } = ArenaState.Loading;

    public string ArenaName => arenaName;
    public int ArenaNumber => arenaNumber;
    public float TimeRemaining { get; private set; }
    public int Score { get; private set; }
    public int Checkpoints { get; private set; }
    public int Combo { get; private set; }

    private bool finishing;

    private void Start()
    {
        PrepareArena();
    }

    private void Update()
    {
        if (State != ArenaState.Running)
            return;

        TimeRemaining -= Time.deltaTime;

        if (TimeRemaining <= 0f)
        {
            TimeRemaining = 0f;
            FailArena();
        }
    }

    public void PrepareArena()
    {
        State = ArenaState.Loading;

        TimeRemaining = challengeDuration;
        Score = 0;
        Checkpoints = 0;
        Combo = 0;
        finishing = false;

        StartCoroutine(BeginCountdown());
    }

    private IEnumerator BeginCountdown()
    {
        State = ArenaState.Countdown;

        float timer = countdownDuration;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        StartArena();
    }

    private void StartArena()
    {
        if (finishing)
            return;

        State = ArenaState.Running;
        TimeRemaining = challengeDuration;

        onArenaStarted?.Invoke();
    }

    public void AddSkillScore(int amount)
    {
        if (State != ArenaState.Running)
            return;

        int comboMultiplier = Mathf.Max(1, Combo);

        Score += Mathf.Max(0, amount) * comboMultiplier;
    }

    public void RegisterSuccess(int baseScore)
    {
        if (State != ArenaState.Running)
            return;

        Combo++;

        AddSkillScore(baseScore);

        onCheckpointReached?.Invoke();
    }

    public void RegisterFailure()
    {
        if (State != ArenaState.Running)
            return;

        Combo = 0;
    }

    public void ReachCheckpoint()
    {
        if (State != ArenaState.Running)
            return;

        Checkpoints++;

        Score += 250;

        Combo++;

        onCheckpointReached?.Invoke();

        if (Checkpoints >= requiredCheckpoints)
        {
            TryCompleteArena();
        }
    }

    public void TryCompleteArena()
    {
        if (State != ArenaState.Running || finishing)
            return;

        if (Checkpoints < requiredCheckpoints)
            return;

        if (Score < minimumScore)
            return;

        CompleteArena();
    }

    public void ForceComplete()
    {
        if (State != ArenaState.Running || finishing)
            return;

        CompleteArena();
    }

    private void CompleteArena()
    {
        if (finishing)
            return;

        finishing = true;
        State = ArenaState.Complete;

        int finalXP = completionXP;
        int finalCoins = completionCoins;

        bool perfectRun =
            TimeRemaining > challengeDuration * 0.5f &&
            Combo >= requiredCheckpoints;

        if (perfectRun)
        {
            finalXP += perfectBonusXP;
            finalCoins += perfectBonusCoins;
        }

        if (ProgressionManager.Instance != null)
        {
            ProgressionManager.Instance.AddXP(finalXP);
            ProgressionManager.Instance.AddCoins(finalCoins);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(Score);
            GameManager.Instance.AddCoins(finalCoins);
        }

        onArenaCompleted?.Invoke();

        Debug.Log(
            "ARENA COMPLETE | " +
            arenaName +
            " | SCORE: " +
            Score +
            " | XP: +" +
            finalXP +
            " | COINS: +" +
            finalCoins
        );
    }

    private void FailArena()
    {
        if (finishing)
            return;

        finishing = true;
        State = ArenaState.Failed;
        Combo = 0;

        onArenaFailed?.Invoke();

        Debug.Log(
            "ARENA FAILED | " +
            arenaName +
            " | FINAL SCORE: " +
            Score
        );
    }

    public float GetProgress()
    {
        return Mathf.Clamp01(
            1f - (TimeRemaining / challengeDuration)
        );
    }

    public float GetCheckpointProgress()
    {
        if (requiredCheckpoints <= 0)
            return 1f;

        return Mathf.Clamp01(
            (float)Checkpoints / requiredCheckpoints
        );
    }

    public bool IsRunning()
    {
        return State == ArenaState.Running;
    }

    public bool IsComplete()
    {
        return State == ArenaState.Complete;
    }

    public bool IsFailed()
    {
        return State == ArenaState.Failed;
    }
}
