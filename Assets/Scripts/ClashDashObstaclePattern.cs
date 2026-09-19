using UnityEngine;

public class ClashDashObstaclePattern : MonoBehaviour
{
    public enum PatternType
    {
        Sweep,
        Gate,
        Pulse,
        Spiral
    }

    [Header("Pattern")]
    [SerializeField] private PatternType pattern =
        PatternType.Sweep;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 6f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 60f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition =
            transform.position;
    }

    private void Update()
    {
        switch (pattern)
        {
            case PatternType.Sweep:
                Sweep();
                break;

            case PatternType.Gate:
                Gate();
                break;

            case PatternType.Pulse:
                Pulse();
                break;

            case PatternType.Spiral:
                Spiral();
                break;
        }
    }

    private void Sweep()
    {
        float movement =
            Mathf.Sin(
                Time.time * speed
            ) * distance;

        transform.position =
            startPosition +
            transform.right * movement;
    }

    private void Gate()
    {
        float movement =
            Mathf.Abs(
                Mathf.Sin(
                    Time.time * speed
                )
            );

        transform.position =
            startPosition +
            transform.forward *
            movement *
            distance;
    }

    private void Pulse()
    {
        float scale =
            1f +
            Mathf.Sin(
                Time.time * speed
            ) * 0.35f;

        transform.localScale =
            Vector3.one * scale;
    }

    private void Spiral()
    {
        transform.Rotate(
            Vector3.up,
            rotationSpeed *
            Time.deltaTime,
            Space.World
        );

        float height =
            Mathf.Sin(
                Time.time * speed
            ) * distance;

        transform.position =
            startPosition +
            Vector3.up * height;
    }
}
