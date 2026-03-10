using System;
using UnityEngine;
using UnityEngine.Events;

public class FailState : MonoBehaviour
{
    [SerializeField] private GameObject failCamera;
    [SerializeField] private HealthController playerHealthController;

    public UnityEvent onFailState;

    private void OnEnable()
    {
        playerHealthController.onDeath.AddListener(Fail);
    }

    private void OnDisable()
    {
        playerHealthController.onDeath.RemoveListener(Fail);
    }

    private void Fail()
    {
        failCamera.SetActive(true);
        onFailState.Invoke();
    }
}
