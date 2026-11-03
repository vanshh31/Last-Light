
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    [Header("Audio Setup")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    [Header("Combat Sounds")]
    [SerializeField] private AudioClip projectileFire;
    [SerializeField] private AudioClip enemyHit;
    [SerializeField] private AudioClip enemyDeath;

    [Header("Player Sounds")]
    [SerializeField] private AudioClip playerDamage;
    [SerializeField] private AudioClip playerHeal;
    [SerializeField] private AudioClip playerDeath;

    [Header("Energy Sounds")]
    [SerializeField] private AudioClip energyPickup;

    [Header("Upgrade Sounds")]
    [SerializeField] private AudioClip upgradePurchase;
    [SerializeField] private AudioClip upgradeMenuOpen;
    [SerializeField] private AudioClip upgradeMenuClose;

    [Header("UI Sounds")]
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip pause;
    [SerializeField] private AudioClip resume;
    [SerializeField] private AudioClip gameOver;

    [Header("Wave Sounds")]
    [SerializeField] private AudioClip waveStart;

    [Header("Daily Challenge Sounds")]
    [SerializeField] private AudioClip challengeStart;
    [SerializeField] private AudioClip challengeComplete;
    [SerializeField] private AudioClip challengeFailed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null || audioSource == null)
            return;

        audioSource.PlayOneShot(clip);
    }

    // Combat
    public void PlayProjectileFire() => PlaySound(projectileFire);
    public void PlayEnemyHit() => PlaySound(enemyHit);
    public void PlayEnemyDeath() => PlaySound(enemyDeath);

    // Player
    public void PlayPlayerDamage() => PlaySound(playerDamage);
    public void PlayPlayerHeal() => PlaySound(playerHeal);
    public void PlayPlayerDeath() => PlaySound(playerDeath);

    // Energy
    public void PlayEnergyPickup() => PlaySound(energyPickup);

    // Upgrades
    public void PlayUpgradePurchase() => PlaySound(upgradePurchase);
    public void PlayUpgradeMenuOpen() => PlaySound(upgradeMenuOpen);
    public void PlayUpgradeMenuClose() => PlaySound(upgradeMenuClose);

    // UI
    public void PlayButtonClick() => PlaySound(buttonClick);
    public void PlayPause() => PlaySound(pause);
    public void PlayResume() => PlaySound(resume);
    public void PlayGameOver() => PlaySound(gameOver);

    // Waves
    public void PlayWaveStart() => PlaySound(waveStart);

    // Daily Challenge
    public void PlayChallengeStart() => PlaySound(challengeStart);
    public void PlayChallengeComplete() => PlaySound(challengeComplete);
    public void PlayChallengeFailed() => PlaySound(challengeFailed);

    [ContextMenu("Test SFX")]
    private void TestSFX()
    {
        PlaySound(buttonClick);
    }
}
