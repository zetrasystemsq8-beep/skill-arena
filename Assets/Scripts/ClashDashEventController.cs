using System.Collections;
using UnityEngine;

public class ClashDashEventController : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private ArenaCoreController arena;
    [SerializeField] private ClashDashWorldDirector world;
    [SerializeField] private ClashDashRealityController reality;

    [Header("Event Timing")]
    [SerializeField] private float dangerTime = 35f;
    [SerializeField] private float finalPhaseTime = 48f;

    private bool dangerTriggered;
    private bool finalTriggered;

    private void Start()
    {
        if (arena == null)
            arena = FindFirstObjectByType<ArenaCoreController>();

        if (world == null)
            world = FindFirstObjectByType<ClashDashWorldDirector>();

        if (reality == null)
            reality =
                FindFirstObjectByType<
                    ClashDashRealityController
                >();
    }

    private void Update()
    {
        if (arena == null ||
            !arena.IsRunning())
            return;

        float elapsed =
            60f - arena.TimeRemaining;

        if (!dangerTriggered &&
            elapsed >= dangerTime)
        {
            dangerTriggered = true;
            TriggerDangerPhase();
        }

        if (!finalTriggered &&
            elapsed >= finalPhaseTime)
        {
            finalTriggered = true;
            TriggerFinalPhase();
        }
    }

    private void TriggerDangerPhase()
    {
        if (world != null)
            world.TriggerDangerMode();

        if (reality != null)
            reality.RealityShift();

        if (ClashDashVFXController.Instance != null)
            ClashDashVFXController.Instance.PlayArenaPulse();

        Debug.Log(
            "CLASHDASH // DANGER PHASE"
        );
    }

    private void TriggerFinalPhase()
    {
        StartCoroutine(FinalPhaseSequence());
    }

    private IEnumerator FinalPhaseSequence()
    {
        if (world != null)
            world.TriggerArenaPulse();

        if (reality != null)
            reality.RealityShift();

        yield return new WaitForSeconds(0.2f);

        if (world != null)
            world.TriggerArenaPulse();

        Debug.Log(
            "CLASHDASH // FINAL PHASE // ZETRA"
        );
    }
}
