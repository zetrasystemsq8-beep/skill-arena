using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string[] arenaScenes =
    {
        "Arena_01",
        "Arena_02",
        "Arena_03",
        "Arena_04",
        "Arena_05"
    };

    public void LoadLevel(int level)
    {
        int index = level - 1;

        if (index < 0 || index >= arenaScenes.Length)
            return;

        SceneManager.LoadScene(arenaScenes[index]);
    }

    public void LoadNextLevel()
    {
        if (ProgressionManager.Instance == null)
            return;

        int nextLevel = ProgressionManager.Instance.GetLevel() + 1;

        if (nextLevel <= arenaScenes.Length)
        {
            ProgressionManager.Instance.UnlockNextLevel();
            LoadLevel(nextLevel);
        }
    }
}
