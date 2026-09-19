using System.Collections;
using UnityEngine;

public class ClashDashPlayerImpact : MonoBehaviour
{
    [Header("Impact")]
    [SerializeField] private float knockbackForce = 7f;
    [SerializeField] private float upwardForce = 3f;
    [SerializeField] private float stunDuration = 0.3f;

    [Header("Penalty")]
    [SerializeField] private int scorePenalty = 100;

    private CharacterController controller;
    private Vector3 knockbackVelocity;
    private bool stunned;

    private void Awake()
    {
        controller =
            GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller == null)
            return;

        if (knockbackVelocity.sqrMagnitude > 0.01f)
        {
            controller.Move(
                knockbackVelocity *
                Time.deltaTime
            );

            knockbackVelocity =
                Vector3.Lerp(
                    knockbackVelocity,
                    Vector3.zero,
                    8f * Time.deltaTime
                );
        }
    }

    public void Hit(Vector3 sourcePosition)
    {
        if (stunned)
            return;

        Vector3 direction =
            transform.position -
            sourcePosition;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
            direction = -transform.forward;

        direction.Normalize();

        knockbackVelocity =
            direction * knockbackForce +
            Vector3.up * upwardForce;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(
                -scorePenalty
            );
        }

        ArenaCoreController arena =
            FindFirstObjectByType<
                ArenaCoreController
            >();

        if (arena != null)
            arena.RegisterFailure();

        StartCoroutine(StunRoutine());

        if (ClashDashVFXController.Instance != null)
            ClashDashVFXController.Instance.PlayImpact();

        if (ClashDashAudioManager.Instance != null)
            ClashDashAudioManager.Instance.PlayImpact();
    }

    private IEnumerator StunRoutine()
    {
        stunned = true;

        yield return new WaitForSeconds(
            stunDuration
        );

        stunned = false;
    }

    public bool IsStunned()
    {
        return stunned;
    }
}
