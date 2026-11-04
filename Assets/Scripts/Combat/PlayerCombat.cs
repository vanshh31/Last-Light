
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private float attackRange = 7f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int attackDamage = 1;

    [Header("Hit Cooldown")]
    [SerializeField] private float hitCooldown = 2.25f;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private float fireTimer;
    private float hitCooldownTimer;

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (hitCooldownTimer > 0f)
        {
            hitCooldownTimer -= Time.deltaTime;
            return;
        }

        if (fireTimer >= fireRate)
        {
            Transform target = FindNearestEnemy();

            if (target != null)
            {
                ShootAtTarget(target);
                fireTimer = 0f;
            }
        }
    }

    private Transform FindNearestEnemy()
    {
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemy");

        Transform nearestEnemy = null;
        float shortestDistance = attackRange;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
                continue;

            float distance = Vector2.Distance(
                transform.position,
                enemy.transform.position
            );

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    public void UpgradeDamage()
    {
        attackDamage += 1;
        Debug.Log("Damage upgraded! Current damage: " + attackDamage);
    }

    public void UpgradeFireRate()
    {
        fireRate = Mathf.Max(0.25f, fireRate * 0.85f);
        Debug.Log("Fire rate upgraded! Current delay: " + fireRate);
    }

    public void UpgradeRange()
    {
        attackRange += 1f;
        Debug.Log("Attack range upgraded! Current range: " + attackRange);
    }

    public void ApplyHitCooldown()
    {
        hitCooldownTimer = hitCooldown;
    }

    private void ShootAtTarget(Transform target)
    {
        if (projectilePrefab == null)
            return;

        Vector2 origin = firePoint != null
            ? firePoint.position
            : transform.position;

        Vector2 direction =
            ((Vector2)target.position - origin).normalized;

        GameObject projectile = Instantiate(
            projectilePrefab,
            origin,
            Quaternion.identity
        );

        Projectile projectileScript =
            projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(direction, attackDamage);
        }

        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayProjectileFire();
        }
    }
}
