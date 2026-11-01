
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private int maxHealth = 3;

    [Header("Energy Drop")]
    [SerializeField] private GameObject energyPickupPrefab;
    [SerializeField] private int energyDropAmount = 1;

    private int currentHealth;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || isDead)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (energyPickupPrefab != null)
        {
            GameObject pickup = Instantiate(
                energyPickupPrefab,
                transform.position,
                Quaternion.identity
            );

            EnergyPickup energy =
                pickup.GetComponent<EnergyPickup>();

            if (energy != null)
            {
                energy.SetEnergyValue(energyDropAmount);
            }
        }

        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
