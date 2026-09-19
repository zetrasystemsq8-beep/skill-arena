using UnityEngine;

public class FinishGateController : MonoBehaviour
{
    [SerializeField] private int completionBonus = 1000;

    private bool completed;

    private void OnTriggerEnter(Collider other)
    {
        if (completed)
            return;

        if (!other.CompareTag("Player"))
            return;

        completed = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(completionBonus);
        }

        Debug.Log("ARENA COMPLETE!");
    }
}
