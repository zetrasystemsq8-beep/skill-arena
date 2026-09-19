using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = 20f;

    private CharacterController controller;
    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        Vector2 input = Vector2.zero;

        if (MobileInputController.Instance != null)
        {
            input = MobileInputController.Instance.GetMovement();
        }
        else
        {
            input = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            );
        }

        Vector3 direction =
            transform.right * input.x +
            transform.forward * input.y;

        direction = Vector3.ClampMagnitude(direction, 1f);

        controller.Move(
            direction * moveSpeed * Time.deltaTime
        );

        if (controller.isGrounded)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        controller.Move(
            velocity * Time.deltaTime
        );
    }

    private void Jump()
    {
        bool jump = false;

        if (MobileInputController.Instance != null)
        {
            jump = MobileInputController.Instance.ConsumeJump();
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (controller.isGrounded && jump)
        {
            velocity.y = jumpForce;
        }
    }
}
