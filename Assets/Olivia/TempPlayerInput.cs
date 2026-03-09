using UnityEngine;
using UnityEngine.InputSystem; // Required namespace

public class TempPlayerInput : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private Vector2 movementInput;
    private QTEMinigame qteMinigame;

    public static TempPlayerControls Controls;
    

    void Awake()
    {
        Controls = new TempPlayerControls();
        rb = GetComponent<Rigidbody>();
        qteMinigame = GetComponentInChildren<QTEMinigame>();

        // Subscribe to the 'Move' action's performed and canceled events
        Controls.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        Controls.Player.Move.canceled += ctx => movementInput = Vector2.zero;
        Controls.Player.QTE.performed += ctx => qteMinigame.Press();
    }

    void OnEnable()
    {
        if (Controls != null)
        {
            Controls.Enable();
        }
    }

    void OnDisable()
    {
        if (Controls != null)
        {
            Controls.Disable();
        }
    }

    void FixedUpdate() // Use FixedUpdate for Rigidbody manipulation
    {
        // Calculate movement direction relative to the world
        Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);
        
        // Apply force or set velocity. Setting velocity is a simple way for basic movement.
        // We preserve the current Y velocity for gravity/jumping
        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
    }
}