using System.Collections;
using UnityEngine;

public class ClashDashVFXController : MonoBehaviour
{
    public static ClashDashVFXController Instance;

    [Header("Arena FX")]
    [SerializeField] private ParticleSystem arenaParticles;
    [SerializeField] private ParticleSystem checkpointParticles;
    [SerializeField] private ParticleSystem impactParticles;
    [SerializeField] private ParticleSystem victoryParticles;

    [Header("Screen FX")]
    [SerializeField] private CanvasGroup flashOverlay;
    [SerializeField] private float flashDuration = 0.12f;

    [Header("World Pulse")]
    [SerializeField] private Light arenaLight;
    [SerializeField] private float pulseIntensity = 2f;
    [SerializeField] private float pulseDuration = 0.25f;

    private float originalIntensity;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (arenaLight != null)
            originalIntensity = arenaLight.intensity;

        if (flashOverlay != null)
            flashOverlay.alpha = 0f;
    }

    public void PlayArenaPulse()
    {
        StartCoroutine(LightPulse());

        if (arenaParticles != null)
            arenaParticles.Play();
    }

    public void PlayCheckpoint()
    {
        if (checkpointParticles != null)
            checkpointParticles.Play();

        StartCoroutine(ScreenFlash());
        StartCoroutine(LightPulse());
    }

    public void PlayImpact()
    {
        if (impactParticles != null)
            impactParticles.Play();

        StartCoroutine(ScreenFlash());
    }

    public void PlayVictory()
    {
        if (victoryParticles != null)
            victoryParticles.Play();

        StartCoroutine(VictorySequence());
    }

    private IEnumerator LightPulse()
    {
        if (arenaLight == null)
            yield break;

        arenaLight.intensity =
            originalIntensity + pulseIntensity;

        yield return new WaitForSeconds(pulseDuration);

        arenaLight.intensity = originalIntensity;
    }

    private IEnumerator ScreenFlash()
    {
        if (flashOverlay == null)
            yield break;

        flashOverlay.alpha = 0.8f;

        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;

            flashOverlay.alpha =
                Mathf.Lerp(0.8f, 0f, elapsed / flashDuration);

            yield return null;
        }

        flashOverlay.alpha = 0f;
    }

    private IEnumerator VictorySequence()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return LightPulse();
            yield return new WaitForSeconds(0.12f);
        }

        if (arenaParticles != null)
            arenaParticles.Play();
    }
}
