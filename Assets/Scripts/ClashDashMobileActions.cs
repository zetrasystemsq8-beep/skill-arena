using UnityEngine;

public class ClashDashMobileActions : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController player;

    [Header("Input")]
    [SerializeField] private MobileInputController input;

    private void Start()
    {
        if (player == null)
            player =
                FindFirstObjectByType<PlayerController>();

        if (input == null)
            input =
                FindFirstObjectByType<MobileInputController>();
    }

    public void Jump()
    {
        if (input != null)
            input.PressJump();
    }

    public void StopMovement()
    {
        if (input != null)
            input.StopMovement();
    }
}
