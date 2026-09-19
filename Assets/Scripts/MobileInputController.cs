using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInputController : MonoBehaviour
{
    public static MobileInputController Instance;

    [Header("Movement")]
    [SerializeField] private float joystickX;
    [SerializeField] private float joystickY;

    [Header("Actions")]
    public bool jumpPressed;

    private void Awake()
    {
        Instance = this;
    }

    public void SetMovement(Vector2 input)
    {
        joystickX = Mathf.Clamp(input.x, -1f, 1f);
        joystickY = Mathf.Clamp(input.y, -1f, 1f);
    }

    public Vector2 GetMovement()
    {
        return new Vector2(joystickX, joystickY);
    }

    public void PressJump()
    {
        jumpPressed = true;
    }

    public bool ConsumeJump()
    {
        if (!jumpPressed)
            return false;

        jumpPressed = false;
        return true;
    }

    public void StopMovement()
    {
        joystickX = 0f;
        joystickY = 0f;
    }
}
