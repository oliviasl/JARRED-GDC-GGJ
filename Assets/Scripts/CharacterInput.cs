using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    [SerializeField, ReadOnly] private Vector3 move;
    public Vector3 GetMoveInput() => move;

    [SerializeField, ReadOnly] private Vector2 look;
    public Vector2 GetLookInput() => look;

    [SerializeField, ReadOnly] private bool isInteracting;
    public bool GetInteractInput() => isInteracting;

    [SerializeField, ReadOnly] private float speedChange;
    public float GetSpeedChange() => speedChange;

    private PlayerInput playerInput;
    private InputAction interactAction;
    private InputAction qteAction;

    private QTEMinigame qteMinigame;

    private bool isInputEnabled = true;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        qteMinigame = FindAnyObjectByType<QTEMinigame>();
        interactAction = playerInput.actions["Interact"];
        qteAction = playerInput.actions["QTE"];
    }

    void OnEnable()
    {
        interactAction = GetComponent<PlayerInput>().actions["Interact"];
        interactAction.performed += OnInteractPerformed;
        interactAction.canceled += OnInteractCanceled;
        qteAction.performed += OnQTEPerformed;
    }

    void OnDisable()
    {
        interactAction.performed -= OnInteractPerformed;
        interactAction.canceled -= OnInteractCanceled;
        qteAction.performed -= OnQTEPerformed;
    }

    public void OnMove(InputValue value)
    {
        if (!isInputEnabled) return;

        move = value.Get<Vector3>();
    }

    public void OnLook(InputValue value)
    {
        if (!isInputEnabled) return;

        look = value.Get<Vector2>();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (!isInputEnabled) return;

        isInteracting = true;
    }

    private void OnInteractCanceled(InputAction.CallbackContext context)
    {
        if (!isInputEnabled) return;

        isInteracting = false;
    }

    private void OnQTEPerformed(InputAction.CallbackContext context)
    {
        if (qteMinigame)
        {
            qteMinigame.Press();
        }
    }

    private void OnSpeedChange(InputValue value)
    {
        if (!isInputEnabled) return;

        speedChange = value.Get<float>();
    }
    public void SetInputEnabled(bool toSet)
    {
        isInputEnabled = toSet;
    }
}