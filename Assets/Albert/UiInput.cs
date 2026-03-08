using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UiInput : MonoBehaviour
{
    public UnityEvent InteractAction;

    public void OnInteract(InputValue value)
    {
        InteractAction.Invoke();
    }
}
