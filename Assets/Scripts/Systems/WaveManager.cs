
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TMP_Text waveText;

    [Header("Wave Settings")]
    [SerializeField] private float waveDuration = 60f;
    [SerializeField] private int startingWave = 1;

    [Header("Difficulty Settings")]
    [SerializeField] private float startingSpawnInterval = 3f;
    [SerializeField] private float spawnIntervalDecrease = 0.25f;
    [SerializeField] private float minimumSpawnInterval = 0.75f;

    [SerializeField] private int startingMaxEnemies = 10;
    [SerializeField] private int additionalEnemiesPerWave = 3;

    private int currentWave;
    private float waveTimer;

    public int CurrentWave => currentWave;
    public float WaveTimeRemaining =>
        Mathf.Max(0f, waveDuration - waveTimer);

    private void Start()
    {
        currentWave = startingWave;
        waveTimer = 0f;

        ApplyWaveDifficulty();
        UpdateWaveUI();

        PlayWaveStartSound();
    }

    private void Update()
    {
        waveTimer += Time.deltaTime;

        if (waveTimer >= waveDuration)
        {
            waveTimer -= waveDuration;
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        currentWave++;

        ApplyWaveDifficulty();
        UpdateWaveUI();

        Debug.Log("WAVE " + currentWave + " STARTED!");

        PlayWaveStartSound();
    }

    private void ApplyWaveDifficulty()
    {
        float newSpawnInterval =
            startingSpawnInterval -
            ((currentWave - 1) * spawnIntervalDecrease);

        newSpawnInterval = Mathf.Max(
            minimumSpawnInterval,
            newSpawnInterval
        );

        int newMaxEnemies =
            startingMaxEnemies +
            ((currentWave - 1) * additionalEnemiesPerWave);

        enemySpawner.ConfigureForWave(
            newSpawnInterval,
            newMaxEnemies
        );
    }

    private void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = "WAVE " + currentWave;
        }
    }

    private void PlayWaveStartSound()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayWaveStart();
        }
    }
}
