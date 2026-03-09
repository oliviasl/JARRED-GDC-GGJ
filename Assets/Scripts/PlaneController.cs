using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    private CharacterInput Input;
    private Rigidbody rb;

    [SerializeField] private float minFlySpeed;
    [SerializeField] private float maxFlySpeed;
    [SerializeField] private float speedChangeMagnitude = 5f;
    [SerializeField] private float verticalTurnMagnitude = 50f;
    [SerializeField] private float horizontalTurnMagnitude = 50f;
    [SerializeField] private float rollTurnMagnitude = 50f;
    [SerializeField, Range(1f, 20f)] private float rotationSmoothing = 5f;
    
    [SerializeField, ReadOnly]private float flySpeed;
    private Vector3 flyDirection;
    private Quaternion targetRotation;

    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private Image speedometer;

    void Awake()
    {
        Input = GetComponent<CharacterInput>();
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 5f;

        flyDirection = transform.forward;
        targetRotation = transform.rotation;

        flySpeed = minFlySpeed;
    }

    void FixedUpdate()
    {
        float verticalAxis = Input.GetMoveInput().y;
        float horizAxis = Input.GetMoveInput().x;
        float rollAxis = Input.GetMoveInput().z;
        
        Quaternion pitch = Quaternion.AngleAxis(verticalAxis * verticalTurnMagnitude * Time.fixedDeltaTime, Vector3.right);
        Quaternion yaw = Quaternion.AngleAxis(-horizAxis * horizontalTurnMagnitude * Time.fixedDeltaTime, Vector3.down);
        Quaternion roll = Quaternion.AngleAxis(-rollAxis * rollTurnMagnitude * Time.fixedDeltaTime, Vector3.forward );

        targetRotation *= pitch * yaw * roll;
        targetRotation.Normalize();

        Quaternion smoothedRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSmoothing * Time.fixedDeltaTime);
        rb.MoveRotation(smoothedRotation);

        UpdateSpeed();

        MoveForward();
        flyDirection = transform.forward;
    }

    private void MoveForward()
    {
        transform.position += flyDirection * flySpeed * Time.deltaTime;
    }

    private void UpdateSpeed()
    {
        flySpeed += Input.GetSpeedChange() * speedChangeMagnitude * Time.deltaTime;
        flySpeed = Mathf.Clamp(flySpeed, minFlySpeed, maxFlySpeed);

        //Placeholder
        UpdateSpeedUI();
    }

    private void UpdateSpeedUI()
    {
        speedText.text = flySpeed.ToString("F0");
        speedometer.fillAmount = (flySpeed - minFlySpeed) / (maxFlySpeed - minFlySpeed);
    }
}