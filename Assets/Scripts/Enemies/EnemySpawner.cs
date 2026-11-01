
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxEnemies = 10;

    [Header("Arena Bounds")]
    [SerializeField] private float arenaWidth = 16f;
    [SerializeField] private float arenaHeight = 12f;
    [SerializeField] private float edgeOffset = 0.5f;

    private float spawnTimer;

    private float dailySpawnMultiplier = 1f;
    private int dailyExtraEnemyLimit = 0;

    private void Update()
    {
        if (enemyPrefab == null || player == null)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            TrySpawnEnemy();
        }
    }

    private void TrySpawnEnemy()
    {
        int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (currentEnemies >= maxEnemies)
            return;

        Vector2 spawnPosition = GetSpawnPosition();

        Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
    public void ConfigureForWave(
    float newSpawnInterval,
    int newMaxEnemies)
    {
        spawnInterval = Mathf.Max(
            0.5f,
            newSpawnInterval * dailySpawnMultiplier
        );

        maxEnemies = Mathf.Max(
            1,
            newMaxEnemies + dailyExtraEnemyLimit
        );

        Debug.Log(
            "Spawner updated: Interval = " +
            spawnInterval +
            ", Max Enemies = " +
            maxEnemies
        );
    }

    public void SetDailyModifiers(
    float spawnMultiplier,
    int extraEnemyLimit)
    {
        dailySpawnMultiplier = spawnMultiplier;
        dailyExtraEnemyLimit = extraEnemyLimit;
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 position = Vector2.zero;

        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0:
                position = new Vector2(
                    Random.Range(-arenaWidth / 2, arenaWidth / 2),
                    arenaHeight / 2 - edgeOffset
                );
                break;

            case 1:
                position = new Vector2(
                    Random.Range(-arenaWidth / 2, arenaWidth / 2),
                    -arenaHeight / 2 + edgeOffset
                );
                break;

            case 2:
                position = new Vector2(
                    -arenaWidth / 2 + edgeOffset,
                    Random.Range(-arenaHeight / 2, arenaHeight / 2)
                );
                break;

            case 3:
                position = new Vector2(
                    arenaWidth / 2 - edgeOffset,
                    Random.Range(-arenaHeight / 2, arenaHeight / 2)
                );
                break;
        }

        return position;
    }
}
