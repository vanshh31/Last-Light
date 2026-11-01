using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || currentHealth <= 0)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        Debug.Log("Player Health: " + currentHealth);
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
            return;

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }
}