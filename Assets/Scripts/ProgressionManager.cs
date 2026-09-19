using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance;

    [Header("Progression")]
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int totalXP = 0;
    [SerializeField] private int coins = 0;

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

    public void AddXP(int amount)
    {
        totalXP += Mathf.Max(0, amount);
    }

    public void AddCoins(int amount)
    {
        coins += Mathf.Max(0, amount);
    }

    public void UnlockNextLevel()
    {
        currentLevel++;
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    public int GetXP()
    {
        return totalXP;
    }

    public int GetCoins()
    {
        return coins;
    }
}
