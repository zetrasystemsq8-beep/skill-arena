using UnityEngine;

public class ClashDashStructureAnimator : MonoBehaviour
{
    [Header("Motion")]
    [SerializeField] private float rotationSpeed = 12f;
    [SerializeField] private float floatHeight = 0.35f;
    [SerializeField] private float floatSpeed = 1.2f;

    [Header("Energy")]
    [SerializeField] private float pulseSpeed = 3f;
    [SerializeField] private float pulseAmount = 0.15f;

    private Vector3 startPosition;
    private Vector3 startScale;

    private void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    private void Update()
    {
        AnimateRotation();
        AnimateFloating();
        AnimateEnergyPulse();
    }

    private void AnimateRotation()
    {
        transform.Rotate(
            Vector3.up,
            rotationSpeed * Time.deltaTime,
            Space.World
        );
    }

    private void AnimateFloating()
    {
        float height =
            Mathf.Sin(
                Time.time * floatSpeed
            ) * floatHeight;

        transform.position =
            startPosition +
            Vector3.up * height;
    }

    private void AnimateEnergyPulse()
    {
        float pulse =
            1f +
            Mathf.Sin(
                Time.time * pulseSpeed
            ) * pulseAmount;

        transform.localScale =
            startScale * pulse;
    }
}
