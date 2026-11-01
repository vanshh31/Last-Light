
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;

    private Rigidbody2D rb;
    private Vector2 direction;
    private int damage = 1;
    private bool initialized;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 shootDirection, int projectileDamage)
    {
        direction = shootDirection.normalized;
        damage = projectileDamage;
        initialized = true;

        rb.linearVelocity = direction * speed;

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!initialized)
            return;

        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth =
                other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    private void OnDisable()
    {
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }
}
