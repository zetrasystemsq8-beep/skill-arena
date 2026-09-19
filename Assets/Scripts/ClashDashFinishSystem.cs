using System.Collections;
using UnityEngine;

public class ClashDashFinishSystem : MonoBehaviour
{
    [Header("Arena")]
    [SerializeField] private ArenaCoreController arena;

    [Header("Finish")]
    [SerializeField] private int finishBonus = 1000;

    [Header("FX")]
    [SerializeField] private ParticleSystem finishParticles;
    [SerializeField] private Light finishLight;

    private bool finished;

    private void Start()
    {
        if (arena == null)
            arena = FindFirstObjectByType<ArenaCoreController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (finished)
            return;

        if (!other.CompareTag("Player"))
            return;

        finished = true;

        if (arena != null)
        {
            arena.ForceComplete();
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(
                finishBonus
            );
        }

        if (finishParticles != null)
            finishParticles.Play();

        if (ClashDashAudioManager.Instance != null)
            ClashDashAudioManager.Instance.PlayFinish();

        if (ClashDashVFXController.Instance != null)
            ClashDashVFXController.Instance.PlayVictory();

        if (finishLight != null)
            StartCoroutine(FinishLightPulse());

        Debug.Log(
            "CLASHDASH // FINISH // +" +
            finishBonus
        );
    }

    private IEnumerator FinishLightPulse()
    {
        float original =
            finishLight.intensity;

        for (int i = 0; i < 4; i++)
        {
            finishLight.intensity =
                original * 3f;

            yield return new WaitForSeconds(
                0.12f
            );

            finishLight.intensity =
                original;

            yield return new WaitForSeconds(
                0.12f
            );
        }
    }

    public void ResetFinish()
    {
        finished = false;
    }
}
