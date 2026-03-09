using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    private CharacterInput Input;
    private Rigidbody rb;

    [SerializeField] private float minFlySpeed;
    [SerializeField] private float maxFlySpeed;
    [SerializeField] private float speedChangeMagnitude = 5f;
    [SerializeField] private float turnSensitivity = 30f;
    [SerializeField] private float rollSensitivity = 30f;
    [SerializeField, Range(1f, 20f)] private float rotationSmoothing = 5f;

    [SerializeField, ReadOnly] private float flySpeed;
    private Vector3 flyDirection;
    private Quaternion targetRotation;

    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private Image speedometer;

    [Header("Crosshair")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform imageRect;
    [SerializeField] private float maxRadius = 150f;
    [SerializeField] private float mouseSensitivity = 40f;
    [SerializeField] private float deadzoneRadius = 20f;

    private Vector2 virtualMousePos;

    void Awake()
    {
        Input = GetComponent<CharacterInput>();
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 5f;

        flyDirection = transform.forward;
        targetRotation = transform.rotation;

        flySpeed = minFlySpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        Vector2 crosshairOffset = UpdateCrosshair();
        Vector2 normalizedOffset = crosshairOffset / maxRadius;

        float pitchInput = -normalizedOffset.y * turnSensitivity * 1.5f;
        float yawInput   =  normalizedOffset.x * turnSensitivity;
        float rollInput = -Input.GetMoveInput().x * rollSensitivity;

        Quaternion pitch = Quaternion.AngleAxis(pitchInput * Time.fixedDeltaTime, Vector3.right);
        Quaternion yaw   = Quaternion.AngleAxis(yawInput * Time.fixedDeltaTime, Vector3.up);
        Quaternion roll = Quaternion.AngleAxis(rollInput * Time.fixedDeltaTime, Vector3.forward);

        targetRotation *= pitch * yaw * roll;
        targetRotation.Normalize();

        Quaternion smoothedRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSmoothing * Time.fixedDeltaTime);
        rb.MoveRotation(smoothedRotation);

        UpdateSpeed();

        flyDirection = transform.forward;
        transform.position += flyDirection * flySpeed * Time.deltaTime;
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
        // Accumulate virtual position from delta, scaled by mouseSensitivity
        Vector2 delta = Mouse.current.delta.ReadValue();
        virtualMousePos += delta * mouseSensitivity * Time.deltaTime;

        if (virtualMousePos.magnitude > maxRadius)
            virtualMousePos = virtualMousePos.normalized * maxRadius;

        Vector2 output = virtualMousePos.magnitude < deadzoneRadius ? Vector2.zero : virtualMousePos;

        imageRect.anchoredPosition = output;
        return output;
    }
}
