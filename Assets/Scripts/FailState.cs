using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FailState : MonoBehaviour
{
    [SerializeField] private GameObject failCamera;
    [SerializeField] private HealthController playerHealthController;
    [SerializeField] private TextMeshProUGUI failCountdown;

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
        failCountdown.gameObject.SetActive(true);
        StartCoroutine(FailCountdown());
        
        onFailState.Invoke();
    }

    IEnumerator FailCountdown()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 5; i > 0; --i)
        {
            failCountdown.text = "FAIL! Respawn in " + i;
            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
