using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SimplePlaneController : MonoBehaviour
{
    private CharacterInput Input;
    private Rigidbody rb;

    [SerializeField] private float minFlySpeed = 20f;
    [SerializeField] private float maxFlySpeed = 80f;
    [SerializeField] private float speedChangeMagnitude = 5f;
    [SerializeField] private float pitchSensitivity = 80f;
    [SerializeField, Range(1f, 50f)] private float rotationSmoothing = 15f;

    [SerializeField, ReadOnly] private float flySpeed;

    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private Image speedometer;

    [Header("Crosshair")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform imageRect;
    [SerializeField] private float maxRadius = 150f;
    [SerializeField, Range(1f, 4f)] private float pitchExponent = 2f;
    [SerializeField, Range(0f, 0.3f)] private float deadzone = 0.1f;

    private RectTransform canvasRect;

    void Awake()
    {
        Input = GetComponent<CharacterInput>();
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 10f;

        canvasRect = canvas.GetComponent<RectTransform>();

        flySpeed = minFlySpeed;
    }

    void FixedUpdate()
    {
        Vector2 crosshairOffset = UpdateCrosshair();
        float pitchInput = ProcessPitchInput(crosshairOffset.y);

        Quaternion pitch = Quaternion.AngleAxis(-pitchInput * pitchSensitivity * Time.fixedDeltaTime, transform.right);
        Quaternion targetRotation = pitch * rb.rotation;
        targetRotation.Normalize();

        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSmoothing * Time.fixedDeltaTime));

        UpdateSpeed();
        rb.MovePosition(rb.position + transform.forward * flySpeed * Time.fixedDeltaTime);
    }

    private float ProcessPitchInput(float offsetY)
    {
        float normalized = offsetY / maxRadius;
        float mag = Mathf.Abs(normalized);
        float deadzoned = Mathf.Max(0f, mag - deadzone) / (1f - deadzone);
        float curved = Mathf.Pow(deadzoned, pitchExponent);
        return Mathf.Sign(normalized) * curved;
    }

    private void UpdateSpeed()
    {
        flySpeed += Input.GetSpeedChange() * speedChangeMagnitude * Time.deltaTime;
        flySpeed = Mathf.Clamp(flySpeed, minFlySpeed, maxFlySpeed);
        UpdateSpeedUI();
    }

    private void UpdateSpeedUI()
    {
        speedText.text = flySpeed.ToString("F0");
        speedometer.fillAmount = (flySpeed - minFlySpeed) / (maxFlySpeed - minFlySpeed);
    }

    private Vector2 UpdateCrosshair()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            canvas.worldCamera,
            out Vector2 mousePos
        );

        if (mousePos.magnitude > maxRadius)
            mousePos = mousePos.normalized * maxRadius;

        imageRect.anchoredPosition = mousePos;
        return mousePos;
    }
}
