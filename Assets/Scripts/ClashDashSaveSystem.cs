using UnityEngine;

public class ClashDashSaveSystem : MonoBehaviour
{
    public static ClashDashSaveSystem Instance;

    private const string LEVEL_KEY = "CLASHDASH_LEVEL";
    private const string XP_KEY = "CLASHDASH_XP";
    private const string COINS_KEY = "CLASHDASH_COINS";
    private const string BEST_SCORE_KEY = "CLASHDASH_BEST_SCORE";

    public int CurrentLevel { get; private set; }
    public int XP { get; private set; }
    public int Coins { get; private set; }
    public int BestScore { get; private set; }

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

        Load();
    }

    public void AddXP(int amount)
    {
        XP += Mathf.Max(0, amount);
        Save();
    }

    public void AddCoins(int amount)
    {
        Coins += Mathf.Max(0, amount);
        Save();
    }

    public void UnlockLevel(int level)
    {
        if (level <= CurrentLevel)
            return;

        CurrentLevel = level;
        Save();
    }

    public void SetBestScore(int score)
    {
        if (score <= BestScore)
            return;

        BestScore = score;
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(LEVEL_KEY, CurrentLevel);
        PlayerPrefs.SetInt(XP_KEY, XP);
        PlayerPrefs.SetInt(COINS_KEY, Coins);
        PlayerPrefs.SetInt(BEST_SCORE_KEY, BestScore);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        CurrentLevel =
            PlayerPrefs.GetInt(LEVEL_KEY, 1);

        XP =
            PlayerPrefs.GetInt(XP_KEY, 0);

        Coins =
            PlayerPrefs.GetInt(COINS_KEY, 0);

        BestScore =
            PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
    }

    public void ResetProgress()
    {
        CurrentLevel = 1;
        XP = 0;
        Coins = 0;
        BestScore = 0;

        Save();
    }
}
