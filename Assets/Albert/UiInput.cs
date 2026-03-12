using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UiInput : MonoBehaviour
{
    public UnityEvent InteractAction;
    private InputAction interactAction;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = FindAnyObjectByType<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
    }

    void OnEnable()
    {
        interactAction.performed += Interact;
    }

    private void OnDisable()
    {
        interactAction.performed -= Interact;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (interactAction.WasPressedThisFrame())
        {
            InteractAction.Invoke();
        }
    }
}
