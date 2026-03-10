using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Header("Health Values")]
    [SerializeField] private int maxHealth = 5;
    private int currentHealth;
    
    public UnityEvent onDeath;
    public UnityEvent onHealthChanged;

    public int GetHealth()
    {
        return currentHealth;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageToTake)
    {
        if (damageToTake == 0)
        {
            return;
        }
        
        currentHealth -= damageToTake;
        onHealthChanged.Invoke();
        
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
