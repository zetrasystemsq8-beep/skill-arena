using UnityEngine;

public class ClashDashCheckpointSystem : MonoBehaviour
{
    [Header("Arena")]
    [SerializeField] private ArenaCoreController arena;

    [Header("Checkpoint")]
    [SerializeField] private int checkpointScore = 250;
    [SerializeField] private float timeBonus = 5f;

    [Header("Feedback")]
    [SerializeField] private ClashDashVFXController vfx;

    private bool activated;

    private void Start()
    {
        if (arena == null)
            arena = FindFirstObjectByType<ArenaCoreController>();

        if (vfx == null)
            vfx = FindFirstObjectByType<ClashDashVFXController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (!other.CompareTag("Player"))
            return;

        activated = true;

        if (arena != null)
            arena.ReachCheckpoint();

        if (GameManager.Instance != null)
            GameManager.Instance.AddScore(
                checkpointScore
            );

        if (vfx != null)
            vfx.PlayCheckpoint();

        if (ClashDashAudioManager.Instance != null)
            ClashDashAudioManager.Instance.PlayCheckpoint();

        Debug.Log(
            "CHECKPOINT REACHED // +" +
            checkpointScore +
            " // +" +
            timeBonus +
            "s"
        );
    }

    public void ResetCheckpoint()
    {
        activated = false;
    }
}
