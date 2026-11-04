
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string gameplaySceneName = "LastLight_Main";

    [Header("Menu Panels")]
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private GameObject settingsPanel;

    private const string PendingKey = "DailyChallengePending";
    private const string PreviewKey = "OpenDailyPreview";

    private void Start()
    {
        Time.timeScale = 1f;

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        // Clear any leftover challenge launch requests.
        PlayerPrefs.SetInt(PendingKey, 0);
        PlayerPrefs.SetInt(PreviewKey, 0);
        PlayerPrefs.Save();
    }

    public void StartSurvival()
    {
        PlayButtonClick();

        PlayerPrefs.SetInt(PendingKey, 0);
        PlayerPrefs.SetInt(PreviewKey, 0);
        PlayerPrefs.Save();

        Time.timeScale = 1f;
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OpenDailyChallenge()
    {
        PlayButtonClick();

        // Ask the gameplay scene to show the daily challenge preview.
        PlayerPrefs.SetInt(PendingKey, 0);
        PlayerPrefs.SetInt(PreviewKey, 1);
        PlayerPrefs.Save();

        Time.timeScale = 1f;
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OpenHowToPlay()
    {
        PlayButtonClick();

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        PlayButtonClick();

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        PlayButtonClick();

        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        PlayButtonClick();

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        PlayButtonClick();

        Debug.Log("Quitting LAST LIGHT");
        Application.Quit();
    }

    private void PlayButtonClick()
    {
        if (SFXManager.Instance != null)
            SFXManager.Instance.PlayButtonClick();
    }
}
