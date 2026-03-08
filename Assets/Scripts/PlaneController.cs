using System;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private CharacterInput Input;
    private Rigidbody rb;

    [SerializeField] private float flySpeed;
    [SerializeField] private float verticalTurnMagnitude = 50f;
    [SerializeField] private float horizontalTurnMagnitude = 50f;
    private Vector3 flyDirection;
    void Awake()
    {
        Input = GetComponent<CharacterInput>();
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 5f;

        flyDirection = transform.forward;
    }

    void FixedUpdate()
    {
        flyDirection = transform.forward;
        MoveUpDown();
        MoveLeftRight();
        MoveForward();
    }

    private void MoveUpDown()
    {
        float verticalAxis = Input.GetMoveInput().y;

        rb.AddTorque(transform.right * verticalAxis * Time.deltaTime * verticalTurnMagnitude);
    }

    private void MoveLeftRight()
    {
        float horizAxis = Input.GetMoveInput().x;

        rb.AddTorque(transform.up * -1f * horizAxis * Time.deltaTime * horizontalTurnMagnitude);
    }

    private void MoveForward()
    {
        transform.position += flyDirection * flySpeed * Time.deltaTime;
    }
}