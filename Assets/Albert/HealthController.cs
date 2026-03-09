using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Health Values")]
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageToTake)
    {
        currentHealth -= damageToTake; 
        if(currentHealth <= 0)
        {
            //you die
        }
    }

    private void Die()
    {
        //die
        Debug.Log("Player Died");
    }


}
