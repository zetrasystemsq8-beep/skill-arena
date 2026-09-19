using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [Header("Arena")]
    public string arenaName = "The Test";
    public int arenaLevel = 1;

    [Header("Challenge")]
    public int targetScore = 1000;

    private bool arenaActive;

    private void Start()
    {
        BeginArena();
    }

    public void BeginArena()
    {
        arenaActive = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartChallenge();
        }

        Debug.Log("Arena Started: " + arenaName);
    }

    public void CompleteArena()
    {
        if (!arenaActive)
            return;

        arenaActive = false;

        Debug.Log("Arena Complete!");
        Debug.Log("Unlocking next level...");
    }

    public bool IsArenaActive()
    {
        return arenaActive;
    }
}
