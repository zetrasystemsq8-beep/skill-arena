using UnityEngine;

public class ArenaTrigger : MonoBehaviour
{
    [SerializeField] private int arenaLevel = 1;
    [SerializeField] private LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (levelManager == null)
            return;

        levelManager.LoadLevel(arenaLevel);
    }
}
