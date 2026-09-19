using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Vector3 moveDirection = Vector3.right;
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float moveSpeed = 2f;

    [Header("Rotation")]
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float rotationSpeed = 60f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveObstacle();
        RotateObstacle();
    }

    private void MoveObstacle()
    {
        float movement = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        transform.position =
            startPosition + moveDirection.normalized * movement;
    }

    private void RotateObstacle()
    {
        transform.Rotate(
            rotationAxis.normalized *
            rotationSpeed *
            Time.deltaTime
        );
    }
}
