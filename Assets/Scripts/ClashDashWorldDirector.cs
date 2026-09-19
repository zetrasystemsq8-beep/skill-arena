using System.Collections;
using UnityEngine;

public class ClashDashWorldDirector : MonoBehaviour
{
    public static ClashDashWorldDirector Instance;

    [Header("Arena Core")]
    [SerializeField] private Transform arenaCore;
    [SerializeField] private Transform player;

    [Header("World Energy")]
    [SerializeField] private Light coreLight;
    [SerializeField] private Light[] energyLights;
    [SerializeField] private ParticleSystem energyParticles;
    [SerializeField] private ParticleSystem skyParticles;

    [Header("Animated World")]
    [SerializeField] private Transform[] rotatingStructures;
    [SerializeField] private float rotationSpeed = 12f;

    [Header("Branding")]
    [SerializeField] private string primaryIdentity = "CONNECT";
    [SerializeField] private string studioIdentity = "ZETRA";

    [Header("Arena State")]
    [SerializeField] private Color normalEmission =
        new Color(0.2f, 0.8f, 1f);

    [SerializeField] private Color alertEmission =
        new Color(1f, 0.25f, 0.1f);

    private float originalLightIntensity;
    private bool awakening;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (coreLight != null)
            originalLightIntensity = coreLight.intensity;
    }

    private void Start()
    {
        InitializeWorld();
    }

    private void Update()
    {
        RotateStructures();
        TrackArenaEnergy();
    }

    private void InitializeWorld()
    {
        if (energyParticles != null)
            energyParticles.Play();

        if (skyParticles != null)
            skyParticles.Play();

        if (coreLight != null)
            coreLight.intensity =
                originalLightIntensity;

        Debug.Log(
            "ZETRA WORLD ONLINE // " +
            primaryIdentity
        );
    }

    public void TriggerArenaAwakening()
    {
        if (awakening)
            return;

        StartCoroutine(AwakeningSequence());
    }

    private IEnumerator AwakeningSequence()
    {
        awakening = true;

        if (coreLight != null)
            coreLight.intensity = 0f;

        yield return new WaitForSeconds(0.25f);

        if (coreLight != null)
        {
            float elapsed = 0f;
            float duration = 1.2f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                coreLight.intensity =
                    Mathf.Lerp(
                        0f,
                        originalLightIntensity,
                        elapsed / duration
                    );

                yield return null;
            }
        }

        if (energyParticles != null)
            energyParticles.Play();

        Debug.Log(
            "CLASHDASH // THE TEST // AWAKENED"
        );

        awakening = false;
    }

    public void TriggerArenaPulse()
    {
        StartCoroutine(ArenaPulse());
    }

    private IEnumerator ArenaPulse()
    {
        if (coreLight == null)
            yield break;

        float start =
            coreLight.intensity;

        float peak =
            start * 2.5f;

        float elapsed = 0f;

        while (elapsed < 0.18f)
        {
            elapsed += Time.deltaTime;

            coreLight.intensity =
                Mathf.Lerp(
                    start,
                    peak,
                    elapsed / 0.18f
                );

            yield return null;
        }

        elapsed = 0f;

        while (elapsed < 0.35f)
        {
            elapsed += Time.deltaTime;

            coreLight.intensity =
                Mathf.Lerp(
                    peak,
                    start,
                    elapsed / 0.35f
                );

            yield return null;
        }

        coreLight.intensity = start;
    }

    private void RotateStructures()
    {
        if (rotatingStructures == null)
            return;

        foreach (Transform structure in rotatingStructures)
        {
            if (structure == null)
                continue;

            structure.Rotate(
                Vector3.up,
                rotationSpeed * Time.deltaTime,
                Space.World
            );
        }
    }

    private void TrackArenaEnergy()
    {
        if (player == null || arenaCore == null)
            return;

        float distance =
            Vector3.Distance(
                player.position,
                arenaCore.position
            );

        float energy =
            Mathf.Clamp01(1f - distance / 35f);

        if (coreLight != null)
        {
            coreLight.intensity =
                Mathf.Lerp(
                    originalLightIntensity * 0.65f,
                    originalLightIntensity * 1.2f,
                    energy
                );
        }
    }

    public void TriggerDangerMode()
    {
        StartCoroutine(DangerPulse());
    }

    private IEnumerator DangerPulse()
    {
        if (coreLight == null)
            yield break;

        Color originalColor =
            coreLight.color;

        coreLight.color =
            alertEmission;

        coreLight.intensity *= 1.5f;

        yield return new WaitForSeconds(0.4f);

        coreLight.color =
            originalColor;

        coreLight.intensity =
            originalLightIntensity;
    }

    public string GetPrimaryIdentity()
    {
        return primaryIdentity;
    }

    public string GetStudioIdentity()
    {
        return studioIdentity;
    }
}
