using UnityEngine;
using System.Collections;

public class ObstacleImpactController : MonoBehaviour
{
    [Header("Impact")]
    [SerializeField] private float knockbackForce = 7f;
    [SerializeField] private float upwardForce = 3f;
    [SerializeField] private float stunDuration = 0.35f;

    [Header("Penalty")]
    [SerializeField] private int scorePenalty = 100;
    [SerializeField] private int comboLoss = 1;

    [Header("Feedback")]
    [SerializeField] private float cameraShake = 0.15f;

    private bool canHit = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (!canHit)
            return;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        StartCoroutine(ImpactPlayer(collision.gameObject));
    }

    private IEnumerator ImpactPlayer(GameObject player)
    {
        canHit = false;

        Rigidbody playerBody =
            player.GetComponent<Rigidbody>();

        CharacterController controller =
            player.GetComponent<CharacterController>();

        Vector3 direction =
            (player.transform.position - transform.position).normalized;

        direction.y = 0f;

        if (playerBody != null)
        {
            playerBody.AddForce(
                direction * knockbackForce +
                Vector3.up * upwardForce,
                ForceMode.Impulse
            );
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(-scorePenalty);
        }

        ArenaCoreController arena =
            FindFirstObjectByType<ArenaCoreController>();

        if (arena != null)
        {
            arena.RegisterFailure();
        }

        yield return new WaitForSeconds(stunDuration);

        canHit = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canHit)
            return;

        if (!other.CompareTag("Player"))
            return;

        StartCoroutine(ImpactPlayer(other.gameObject));
    }
}
