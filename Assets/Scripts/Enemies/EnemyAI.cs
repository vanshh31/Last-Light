using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Contact Damage")]
    [SerializeField] private int contactDamage = 10;
    [SerializeField] private float damageCooldown = 1f;

    private Rigidbody2D rb;
    private Transform player;
    private PlayerHealth playerHealth;
    private float damageTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogError("EnemyAI: Player object not found!");
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        Vector2 direction = ((Vector2)player.position -
                             rb.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }

    private void Update()
    {
        if (damageTimer > 0)
            damageTimer -= Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (damageTimer > 0)
            return;

        if (playerHealth == null)
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage);
            damageTimer = damageCooldown;
        }
    }
}