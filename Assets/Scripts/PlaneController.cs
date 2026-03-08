using System;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private CharacterInput Input;
    private Rigidbody rb;

    [SerializeField] private float flySpeed;
    [SerializeField] private float verticalTurnMagnitude = 50f;
    [SerializeField] private float horizontalTurnMagnitude = 50f;
    [SerializeField] private float rollTurnMagnitude = 50f;
    [SerializeField, Range(1f, 20f)] private float rotationSmoothing = 5f;
    
    private Vector3 flyDirection;
    private Quaternion targetRotation;

    void Awake()
    {
        Input = GetComponent<CharacterInput>();
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 5f;

        flyDirection = transform.forward;
        targetRotation = transform.rotation;
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

        MoveForward();
        flyDirection = transform.forward;
    }

    private void MoveForward()
    {
        transform.position += flyDirection * flySpeed * Time.deltaTime;
    }
}