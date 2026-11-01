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

    private void Update()
    {
        if (gameEnded)
            return;

        survivalTime += Time.deltaTime;

        if (playerHealth.CurrentHealth <= 0)
            GameOver();
    }

    private void GameOver()
    {
        gameEnded = true;

        Time.timeScale = 0f;

        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        int minutes = Mathf.FloorToInt(survivalTime / 60);
        int seconds = Mathf.FloorToInt(survivalTime % 60);

        finalTimeText.text =
            "SURVIVAL TIME: " +
            string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}