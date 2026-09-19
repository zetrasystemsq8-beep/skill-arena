using UnityEngine;

public class ClashDashObstacleImpact : MonoBehaviour
{
    [Header("Impact")]
    [SerializeField] private float cooldown = 0.5f;

    [Header("Damage")]
    [SerializeField] private int scorePenalty = 100;

    private float nextHitTime;

    private void OnCollisionEnter(Collision collision)
    {
        TryImpact(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        TryImpact(other.gameObject);
    }

    private void TryImpact(GameObject target)
    {
        if (Time.time < nextHitTime)
            return;

        if (!target.CompareTag("Player"))
            return;

        ClashDashPlayerImpact impact =
            target.GetComponent<
                ClashDashPlayerImpact
            >();

        if (impact == null)
        {
            impact =
                target.AddComponent<
                    ClashDashPlayerImpact
                >();
        }

        nextHitTime =
            Time.time + cooldown;

        impact.Hit(transform.position);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(
                -scorePenalty
            );
        }
    }
}
