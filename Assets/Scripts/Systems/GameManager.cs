
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalTimeText;

    private float survivalTime;
    private bool gameEnded;

    private void Awake()
    {
        GamePauseState.ResetState();
    }

    private void Update()
    {
        if (gameEnded)
            return;

        if (playerHealth == null)
            return;

        if (playerHealth.CurrentHealth <= 0)
        {
            GameOver();
            return;
        }

        survivalTime += Time.deltaTime;
    }

    private void GameOver()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        GamePauseState.SetGameOver(true);

        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayGameOver();
        }

        if (hudPanel != null)
            hudPanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        int minutes = Mathf.FloorToInt(survivalTime / 60);
        int seconds = Mathf.FloorToInt(survivalTime % 60);

        if (finalTimeText != null)
        {
            finalTimeText.text =
                "SURVIVAL TIME: " +
                string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        GamePauseState.ResetState();

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
