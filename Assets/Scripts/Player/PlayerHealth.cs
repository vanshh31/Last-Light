
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    private bool deathSoundPlayed;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        deathSoundPlayed = false;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || currentHealth <= 0)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            if (!deathSoundPlayed)
            {
                deathSoundPlayed = true;

                if (SFXManager.Instance != null)
                {
                    SFXManager.Instance.PlayPlayerDeath();
                }
            }
        }
        else
        {
            if (SFXManager.Instance != null)
            {
                SFXManager.Instance.PlayPlayerDamage();
            }
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || currentHealth <= 0)
            return;

        int previousHealth = currentHealth;

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);

        // Play sound only if health actually increased.
        if (currentHealth > previousHealth)
        {
            if (SFXManager.Instance != null)
            {
                SFXManager.Instance.PlayPlayerHeal();
            }
        }
    }
}
