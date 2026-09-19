using System.Collections;
using UnityEngine;

public class ClashDashArenaDirector : MonoBehaviour
{
    [Header("Arena")]
    [SerializeField] private ArenaCoreController arena;
    [SerializeField] private ClashDashWorldDirector world;

    [Header("Obstacle Groups")]
    [SerializeField] private GameObject[] openingObstacles;
    [SerializeField] private GameObject[] speedObstacles;
    [SerializeField] private GameObject[] precisionObstacles;
    [SerializeField] private GameObject[] finalObstacles;

    [Header("Arena Gates")]
    [SerializeField] private GameObject openingGate;
    [SerializeField] private GameObject speedGate;
    [SerializeField] private GameObject finalGate;

    [Header("Dynamic Difficulty")]
    [SerializeField] private float openingDelay = 2f;
    [SerializeField] private float speedDelay = 12f;
    [SerializeField] private float precisionDelay = 25f;
    [SerializeField] private float finalDelay = 42f;

    private bool openingStarted;
    private bool speedStarted;
    private bool precisionStarted;
    private bool finalStarted;

    private void Start()
    {
        if (arena == null)
            arena = FindFirstObjectByType<ArenaCoreController>();

        if (world == null)
            world = FindFirstObjectByType<ClashDashWorldDirector>();

        DisableAllGroups();

        StartCoroutine(ArenaSequence());
    }

    private void Update()
    {
        if (arena == null)
            return;

        if (!arena.IsRunning())
            return;

        float elapsed =
            GetElapsedTime();

        if (!openingStarted && elapsed >= openingDelay)
        {
            openingStarted = true;
            ActivateGroup(openingObstacles);
            OpenGate(openingGate);
        }

        if (!speedStarted && elapsed >= speedDelay)
        {
            speedStarted = true;
            ActivateGroup(speedObstacles);
            OpenGate(speedGate);

            TriggerWorldEvent();
        }

        if (!precisionStarted && elapsed >= precisionDelay)
        {
            precisionStarted = true;
            ActivateGroup(precisionObstacles);

            TriggerWorldEvent();
        }

        if (!finalStarted && elapsed >= finalDelay)
        {
            finalStarted = true;
            ActivateGroup(finalObstacles);
            OpenGate(finalGate);

            TriggerWorldEvent();
        }
    }

    private IEnumerator ArenaSequence()
    {
        yield return new WaitForSeconds(0.5f);

        if (world != null)
            world.TriggerArenaAwakening();

        yield return new WaitForSeconds(1.5f);

        if (openingGate != null)
            openingGate.SetActive(true);
    }

    private float GetElapsedTime()
    {
        return 60f -
               arena.TimeRemaining;
    }

    private void DisableAllGroups()
    {
        DisableGroup(openingObstacles);
        DisableGroup(speedObstacles);
        DisableGroup(precisionObstacles);
        DisableGroup(finalObstacles);

        if (openingGate != null)
            openingGate.SetActive(false);

        if (speedGate != null)
            speedGate.SetActive(false);

        if (finalGate != null)
            finalGate.SetActive(false);
    }

    private void ActivateGroup(GameObject[] group)
    {
        if (group == null)
            return;

        foreach (GameObject obstacle in group)
        {
            if (obstacle != null)
                obstacle.SetActive(true);
        }
    }

    private void DisableGroup(GameObject[] group)
    {
        if (group == null)
            return;

        foreach (GameObject obstacle in group)
        {
            if (obstacle != null)
                obstacle.SetActive(false);
        }
    }

    private void OpenGate(GameObject gate)
    {
        if (gate != null)
            gate.SetActive(true);
    }

    private void TriggerWorldEvent()
    {
        if (world != null)
            world.TriggerArenaPulse();
    }
}
