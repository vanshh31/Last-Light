
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class DailyChallengeManager : MonoBehaviour
{
    [Header("Gameplay References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private DarknessController darknessController;
    [SerializeField] private EnemySpawner enemySpawner;

    [Header("Challenge UI")]
    [SerializeField] private GameObject dailyChallengePanel;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject challengeContent;

    [SerializeField] private TMP_Text dateText;
    [SerializeField] private TMP_Text modifierText;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private TMP_Text bestTimeText;
    [SerializeField] private TMP_Text challengeTimerText;
    [SerializeField] private TMP_Text resultTitleText;
    [SerializeField] private TMP_Text resultTimeText;
    [SerializeField] private TMP_Text modifierDescriptionText;

    [Header("Challenge Settings")]
    [SerializeField] private float objectiveDuration = 180f;

    private bool challengeActive;
    private bool challengeComplete;
    private float challengeTimer;

    private string today;
    private string modifierName;
    private int modifierIndex;

    private const string PendingKey = "DailyChallengePending";

    private void Start()
    {
        today = DateTime.Now.ToString("yyyy-MM-dd");

        GenerateDailyModifier();
        UpdateChallengeInformation();

        dailyChallengePanel.SetActive(false);
        resultPanel.SetActive(false);
        challengeContent.SetActive(true);

        if (PlayerPrefs.GetInt(PendingKey, 0) == 1)
        {
            PlayerPrefs.SetInt(PendingKey, 0);
            PlayerPrefs.Save();

            BeginChallenge();
        }
        else if (PlayerPrefs.GetInt("OpenDailyPreview", 0) == 1)
        {
            PlayerPrefs.SetInt("OpenDailyPreview", 0);
            PlayerPrefs.Save();

            dailyChallengePanel.SetActive(true);
            challengeContent.SetActive(true);
            resultPanel.SetActive(false);

            Time.timeScale = 0f;
        }
    }

    private void Update()
    {
        if (!challengeActive || challengeComplete)
            return;

        // Check whether the player has been defeated.
        if (playerHealth == null || playerHealth.CurrentHealth <= 0)
        {
            FailChallenge();
            return;
        }

        challengeTimer += Time.deltaTime;

        UpdateTimerUI();

        if (challengeTimer >= objectiveDuration)
        {
            CompleteChallenge();
        }
    }

    private void FailChallenge()
    {
        if (challengeComplete)
            return;

        Debug.Log("DAILY CHALLENGE FAILED");

        challengeComplete = true;
        challengeActive = false;

        Time.timeScale = 0f;

        // Show the result screen.
        dailyChallengePanel.SetActive(true);
        challengeContent.SetActive(false);
        resultPanel.SetActive(true);

        resultPanel.transform.SetAsLastSibling();

        // Update result text.
        resultTitleText.text = "CHALLENGE FAILED!";
        resultTimeText.text =
            "SURVIVED: " +
            FormatTime(Mathf.FloorToInt(challengeTimer));

        // Do not update DailyBest here.
    }

    private void GenerateDailyModifier()
    {
        DateTime date = DateTime.Now;

        int seed =
            date.Year * 10000 +
            date.Month * 100 +
            date.Day;

        modifierIndex = seed % 3;

        switch (modifierIndex)
        {
            case 0:
                modifierName = "FADING LIGHT";
                break;

            case 1:
                modifierName = "SWARM PROTOCOL";
                break;

            default:
                modifierName = "LOW VISIBILITY";
                break;
        }
    }

    private void UpdateChallengeInformation()
    {
        dateText.text = "DATE: " + today;
        modifierText.text = "MODIFIER: " + modifierName;
        objectiveText.text = "SURVIVE FOR 03:00";

        int best = PlayerPrefs.GetInt("DailyBest_" + today, 0);

        bestTimeText.text =
            "TODAY'S BEST: " + FormatTime(best);

        switch (modifierIndex)
        {
            case 0:
                modifierDescriptionText.text =
                    "Your light fades 50% faster. Collect energy to keep the darkness away.";
                break;

            case 1:
                modifierDescriptionText.text =
                    "Enemies spawn faster and swarm the arena. Stay mobile and survive.";
                break;

            default:
                modifierDescriptionText.text =
                    "Begin with a smaller light radius. Every movement matters.";
                break;
        }
    }

    public void OpenDailyPanel()
    {
        if (challengeActive || challengeComplete)
            return;

        dailyChallengePanel.SetActive(true);
        challengeContent.SetActive(true);
        resultPanel.SetActive(false);

        Time.timeScale = 0f;
    }

    public void CloseDailyPanel()
    {
        dailyChallengePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void StartDailyChallenge()
    {
        Time.timeScale = 1f;

        PlayerPrefs.SetInt(PendingKey, 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    private void BeginChallenge()
    {
        challengeActive = true;
        challengeComplete = false;
        challengeTimer = 0f;

        Time.timeScale = 1f;

        challengeContent.SetActive(true);
        resultPanel.SetActive(false);
        dailyChallengePanel.SetActive(false);

        switch (modifierIndex)
        {
            case 0:
                // Fading Light
                darknessController.ApplyDailyModifier(1f, 1.5f);
                enemySpawner.SetDailyModifiers(1f, 0);
                break;

            case 1:
                // Swarm Protocol
                darknessController.ApplyDailyModifier(1f, 1f);
                enemySpawner.SetDailyModifiers(0.8f, 3);
                break;

            default:
                // Low Visibility
                darknessController.ApplyDailyModifier(0.75f, 1f);
                enemySpawner.SetDailyModifiers(1f, 0);
                break;
        }

        enemySpawner.ConfigureForWave(3f, 10);

        Debug.Log("Daily Challenge Started: " + modifierName);
    }

    private void CompleteChallenge()
    {
        if (challengeComplete)
            return;

        Debug.Log("DAILY CHALLENGE COMPLETED");

        challengeComplete = true;
        challengeActive = false;

        Time.timeScale = 0f;

        int previousBest =
            PlayerPrefs.GetInt("DailyBest_" + today, 0);

        int completedSeconds =
            Mathf.FloorToInt(challengeTimer);

        if (completedSeconds > previousBest)
        {
            PlayerPrefs.SetInt(
                "DailyBest_" + today,
                completedSeconds
            );

            PlayerPrefs.Save();
        }

        // Show the result screen
        dailyChallengePanel.SetActive(true);
        challengeContent.SetActive(false);
        resultPanel.SetActive(true);

        // Ensure result panel renders above other UI
        resultPanel.transform.SetAsLastSibling();

        resultTitleText.text = "CHALLENGE COMPLETED!";
        resultTimeText.text = FormatTime(completedSeconds);

        Debug.Log(
            "Result panel activated: " +
            resultPanel.activeInHierarchy
        );
    }

    private void UpdateTimerUI()
    {
        challengeTimerText.text =
            FormatTime(Mathf.FloorToInt(challengeTimer));
    }

    private string FormatTime(int seconds)
    {
        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;

        return string.Format(
            "{0:00}:{1:00}",
            minutes,
            remainingSeconds
        );
    }

    public void RetryChallenge()
    {
        StartDailyChallenge();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
