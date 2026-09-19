using UnityEngine;

public class ClashDashAudioManager : MonoBehaviour
{
    public static ClashDashAudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource ambienceSource;

    [Header("Master Volume")]
    [Range(0f, 1f)]
    [SerializeField] private float masterVolume = 1f;

    [Header("Arena Music")]
    [SerializeField] private AudioClip arenaMusic;
    [SerializeField] private AudioClip victoryMusic;
    [SerializeField] private AudioClip failureMusic;

    [Header("Arena Effects")]
    [SerializeField] private AudioClip countdownSound;
    [SerializeField] private AudioClip checkpointSound;
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip finishSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ApplyVolume();
        PlayArenaMusic();
    }

    public void PlayArenaMusic()
    {
        if (musicSource == null || arenaMusic == null)
            return;

        musicSource.clip = arenaMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayVictory()
    {
        StopMusic();

        if (musicSource != null && victoryMusic != null)
        {
            musicSource.clip = victoryMusic;
            musicSource.loop = false;
            musicSource.Play();
        }

        PlayEffect(finishSound);
    }

    public void PlayFailure()
    {
        StopMusic();

        if (musicSource != null && failureMusic != null)
        {
            musicSource.clip = failureMusic;
            musicSource.loop = false;
            musicSource.Play();
        }
    }

    public void PlayCountdown()
    {
        PlayEffect(countdownSound);
    }

    public void PlayCheckpoint()
    {
        PlayEffect(checkpointSound);
    }

    public void PlayImpact()
    {
        PlayEffect(impactSound);
    }

    public void PlayJump()
    {
        PlayEffect(jumpSound);
    }

    public void PlayFinish()
    {
        PlayEffect(finishSound);
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        ApplyVolume();
    }

    private void ApplyVolume()
    {
        if (musicSource != null)
            musicSource.volume = masterVolume;

        if (effectsSource != null)
            effectsSource.volume = masterVolume;

        if (ambienceSource != null)
            ambienceSource.volume = masterVolume;
    }

    private void PlayEffect(AudioClip clip)
    {
        if (effectsSource == null || clip == null)
            return;

        effectsSource.PlayOneShot(clip);
    }

    private void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }
}
