using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Header("Health Values")]
    [SerializeField] private int maxHealth = 5;
    private int currentHealth;
    
    public UnityEvent onDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageToTake)
    {
        currentHealth -= damageToTake; 
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void AutoKill()
    {
        TakeDamage(maxHealth);
    }

    private void Die()
    {
        //die
        onDeath.Invoke();
        Debug.Log("Player Died");
    }


}
