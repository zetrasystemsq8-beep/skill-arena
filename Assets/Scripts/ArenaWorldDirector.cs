using System.Collections;
using UnityEngine;

public class ArenaWorldDirector : MonoBehaviour
{
    [Header("Arena Identity")]
    [SerializeField] private string arenaTitle = "THE TEST";
    [SerializeField] private string creatorTag = "CONNECT // ZETRA";

    [Header("World Atmosphere")]
    [SerializeField] private Light mainLight;
    [SerializeField] private ParticleSystem atmosphere;
    [SerializeField] private ParticleSystem energyParticles;

    [Header("Arena Systems")]
    [SerializeField] private Transform arenaCenter;
    [SerializeField] private Transform player;
    [SerializeField] private float worldPulseSpeed = 1.5f;

    [Header("Holographic Branding")]
    [SerializeField] private GameObject[] holographicSigns;

    [Header("Easter Eggs")]
    [SerializeField] private string[] hiddenNames =
    {
        "ZETRA",
        "CONNECT",
        "ZetraMail",
        "Nigergram",
        "NaijaLearn",
        "ZETRA STORE",
        "CRUCIBLE",
        "TRIBUNAL",
        "NAI",
        "TOLUWANI",
        "TOFUMI",
        "FOLAKEMI",
        "MARVELLOUS"
    };

    private float pulse;

    private void Start()
    {
        InitializeWorld();
        StartCoroutine(WorldIntro());
    }

    private void Update()
    {
        pulse += Time.deltaTime * worldPulseSpeed;

        UpdateWorldPulse();
        TrackPlayer();
    }

    private void InitializeWorld()
    {
        if (mainLight != null)
        {
            mainLight.intensity = 1.2f;
        }

        if (atmosphere != null)
        {
            atmosphere.Play();
        }

        if (energyParticles != null)
        {
            energyParticles.Play();
        }

        if (holographicSigns != null)
        {
            foreach (GameObject sign in holographicSigns)
            {
                if (sign != null)
                    sign.SetActive(true);
            }
        }
    }

    private IEnumerator WorldIntro()
    {
        yield return new WaitForSeconds(0.5f);

        Debug.Log(
            "========================================"
        );

        Debug.Log(
            "ZETRA // CONNECT"
        );

        Debug.Log(
            "ARENA INITIALIZED: " + arenaTitle
        );

        Debug.Log(
            "WORLD SYSTEMS ONLINE"
        );

        Debug.Log(
            "CREATOR TAG: " + creatorTag
        );

        Debug.Log(
            "========================================"
        );
    }

    private void UpdateWorldPulse()
    {
        float energy =
            1f + Mathf.Sin(pulse) * 0.15f;

        if (mainLight != null)
        {
            mainLight.intensity =
                1.2f * energy;
        }

        if (energyParticles != null)
        {
            var emission =
                energyParticles.emission;

            emission.rateOverTime =
                25f * energy;
        }
    }

    private void TrackPlayer()
    {
        if (player == null || arenaCenter == null)
            return;

        Vector3 distance =
            player.position - arenaCenter.position;

        if (distance.magnitude > 40f)
        {
            Debug.Log(
                "WORLD ALERT // PLAYER OUTSIDE CORE ZONE"
            );
        }
    }

    public string GetArenaTitle()
    {
        return arenaTitle;
    }

    public string GetCreatorTag()
    {
        return creatorTag;
    }

    public string[] GetEasterEggNames()
    {
        return hiddenNames;
    }

    public void TriggerWorldPulse()
    {
        StopAllCoroutines();
        StartCoroutine(PulseWorld());
    }

    private IEnumerator PulseWorld()
    {
        float originalSpeed = worldPulseSpeed;

        worldPulseSpeed = 7f;

        yield return new WaitForSeconds(1f);

        worldPulseSpeed = originalSpeed;
    }
}
