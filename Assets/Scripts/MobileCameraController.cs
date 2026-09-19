using UnityEngine;

public class MobileCameraController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Position")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 4.5f, -7f);
    [SerializeField] private float followSmoothness = 10f;

    [Header("Rotation")]
    [SerializeField] private float rotationSmoothness = 12f;
    [SerializeField] private float lookHeight = 1.2f;

    [Header("Dynamic Camera")]
    [SerializeField] private float movementTilt = 3f;
    [SerializeField] private float maxTiltSpeed = 8f;
    [SerializeField] private float speedZoom = 0.12f;

    private Vector3 lastTargetPosition;
    private float currentTilt;

    private void Start()
    {
        if (target != null)
            lastTargetPosition = target.position;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        FollowTarget();
        RotateTowardsTarget();
        ApplyMovementEffects();

        lastTargetPosition = target.position;
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition =
            target.TransformPoint(offset);

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSmoothness * Time.deltaTime
        );
    }

    private void RotateTowardsTarget()
    {
        Vector3 lookPoint =
            target.position + Vector3.up * lookHeight;

        Vector3 direction =
            lookPoint - transform.position;

        if (direction.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSmoothness * Time.deltaTime
        );
    }

    private void ApplyMovementEffects()
    {
        Vector3 movement =
            target.position - lastTargetPosition;

        float speed =
            movement.magnitude / Mathf.Max(Time.deltaTime, 0.001f);

        float targetTilt =
            Mathf.Clamp(
                movement.x * movementTilt,
                -maxTiltSpeed,
                maxTiltSpeed
            );

        currentTilt = Mathf.Lerp(
            currentTilt,
            targetTilt,
            8f * Time.deltaTime
        );

        transform.Rotate(
            0f,
            0f,
            -currentTilt,
            Space.Self
        );

        float zoom =
            Mathf.Clamp(speed * speedZoom, 0f, 1.5f);

        Vector3 dynamicOffset =
            offset + Vector3.back * zoom;

        transform.position = Vector3.Lerp(
            transform.position,
            target.TransformPoint(dynamicOffset),
            4f * Time.deltaTime
        );
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        if (target != null)
            lastTargetPosition = target.position;
    }
}
