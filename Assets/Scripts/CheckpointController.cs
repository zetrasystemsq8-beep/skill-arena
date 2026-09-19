using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] private int checkpointNumber = 1;
    [SerializeField] private float timeBonus = 5f;

    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (!other.CompareTag("Player"))
            return;

        activated = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(250);
        }

        Debug.Log(
            "CHECKPOINT " + checkpointNumber +
            " ACTIVATED  +" + timeBonus + "s"
        );
    }
}
