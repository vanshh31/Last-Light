
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pausePanel;

    [Header("Scene Settings")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isPaused;

    private void Start()
    {
        isPaused = false;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        GamePauseState.SetPauseMenu(false);
    }

    private void Update()
    {
        if (GamePauseState.GameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (isPaused || GamePauseState.GameOver)
            return;

        isPaused = true;

        if (pausePanel != null)
            pausePanel.SetActive(true);

        GamePauseState.SetPauseMenu(true);

        if (SFXManager.Instance != null)
            SFXManager.Instance.PlayPause();
    }

    public void ResumeGame()
    {
        if (!isPaused)
            return;

        isPaused = false;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        GamePauseState.SetPauseMenu(false);

        if (SFXManager.Instance != null)
            SFXManager.Instance.PlayResume();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        GamePauseState.ResetState();

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        GamePauseState.ResetState();

        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        GamePauseState.ResetState();

        Application.Quit();
    }
}
