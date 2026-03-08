using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    [SerializeField, ReadOnly] private Vector2 move;
    public Vector2 GetMoveInput() => move;

    [SerializeField, ReadOnly] private Vector2 look;
    public Vector2 GetLookInput() => look;

    [SerializeField, ReadOnly] private bool isInteracting;
    public bool GetInteractInput() => isInteracting;

    private PlayerInput playerInput;
    private InputAction interactAction;

    void Awake()
    {
        SetMouseActive(false);

        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
    }

    void OnEnable()
    {
        interactAction = GetComponent<PlayerInput>().actions["Interact"];
        interactAction.performed += OnInteractPerformed;
        interactAction.canceled += OnInteractCanceled;
    }

    void OnDisable()
    {
        interactAction.performed -= OnInteractPerformed;
        interactAction.canceled -= OnInteractCanceled;
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        isInteracting = true;
    }

    private void OnInteractCanceled(InputAction.CallbackContext context)
    {
        isInteracting = false;
    }

    public void SetMouseActive(bool newActive)
    {
        if (newActive == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}