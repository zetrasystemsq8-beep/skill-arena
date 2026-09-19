using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    [Header("Joystick")]
    [SerializeField] private RectTransform joystickArea;
    [SerializeField] private RectTransform handle;
    [SerializeField] private float handleRange = 80f;

    [Header("Response")]
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private bool normalizeInput = true;

    private Vector2 input;
    private Vector2 startPosition;

    private void Awake()
    {
        if (handle != null)
            startPosition = handle.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateJoystick(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateJoystick(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;

        if (handle != null)
            handle.anchoredPosition = startPosition;

        SendInput();
    }

    private void UpdateJoystick(PointerEventData eventData)
    {
        if (joystickArea == null)
            return;

        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickArea,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        Vector2 center = joystickArea.rect.center;
        Vector2 offset = localPoint - center;

        input = offset / handleRange;
        input *= sensitivity;

        if (normalizeInput)
            input = Vector2.ClampMagnitude(input, 1f);
        else
            input = new Vector2(
                Mathf.Clamp(input.x, -1f, 1f),
                Mathf.Clamp(input.y, -1f, 1f)
            );

        if (handle != null)
        {
            handle.anchoredPosition =
                startPosition + input * handleRange;
        }

        SendInput();
    }

    private void SendInput()
    {
        if (MobileInputController.Instance != null)
        {
            MobileInputController.Instance.SetMovement(input);
        }
    }

    public Vector2 GetInput()
    {
        return input;
    }

    public void ResetJoystick()
    {
        input = Vector2.zero;

        if (handle != null)
            handle.anchoredPosition = startPosition;

        SendInput();
    }
}
