using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ArenaEventSystem : MonoBehaviour
{
    [Header("Arena Events")]
    public UnityEvent onIntro;
    public UnityEvent onCountdown;
    public UnityEvent onFirstCheckpoint;
    public UnityEvent onMidpoint;
    public UnityEvent onFinalApproach;
    public UnityEvent onVictory;
    public UnityEvent onFailure;

    [Header("Timing")]
    [SerializeField] private float midpointProgress = 0.5f;
    [SerializeField] private float finalApproachProgress = 0.82f;

    private ArenaCoreController arena;
    private bool introTriggered;
    private bool checkpointTriggered;
    private bool midpointTriggered;
    private bool finalApproachTriggered;
    private bool resultTriggered;

    private void Start()
    {
        arena = FindFirstObjectByType<ArenaCoreController>();

        StartCoroutine(IntroSequence());
    }

    private void Update()
    {
        if (arena == null)
            return;

        float progress =
            arena.GetProgress();

        if (!checkpointTriggered &&
            arena.Checkpoints >= 1)
        {
            checkpointTriggered = true;
            onFirstCheckpoint?.Invoke();
        }

        if (!midpointTriggered &&
            progress >= midpointProgress)
        {
            midpointTriggered = true;
            onMidpoint?.Invoke();
        }

        if (!finalApproachTriggered &&
            progress >= finalApproachProgress)
        {
            finalApproachTriggered = true;
            onFinalApproach?.Invoke();
        }

        if (resultTriggered)
            return;

        if (arena.IsComplete())
        {
            resultTriggered = true;
            onVictory?.Invoke();
        }
        else if (arena.IsFailed())
        {
            resultTriggered = true;
            onFailure?.Invoke();
        }
    }

    private IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(0.1f);

        if (!introTriggered)
        {
            introTriggered = true;
            onIntro?.Invoke();
        }

        yield return new WaitForSeconds(2f);

        onCountdown?.Invoke();
    }

    public void ResetEvents()
    {
        introTriggered = false;
        checkpointTriggered = false;
        midpointTriggered = false;
        finalApproachTriggered = false;
        resultTriggered = false;
    }
}
