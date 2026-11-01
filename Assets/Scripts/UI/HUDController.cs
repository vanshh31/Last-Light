using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerEnergy playerEnergy;
    [SerializeField] private DarknessController darknessController;

    [Header("UI References")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider lightBar;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text timerText;

    [Header("Light Settings")]
    [SerializeField] private float startingLightRadius = 6f;

    private float survivalTime;

    private void Start()
    {
        healthBar.minValue = 0;
        healthBar.maxValue = playerHealth.MaxHealth;

        lightBar.minValue = 0;
        lightBar.maxValue = startingLightRadius;
    }

    private void Update()
    {
        survivalTime += Time.deltaTime;

        UpdateHealth();
        UpdateEnergy();
        UpdateLight();
        UpdateTimer();
    }

    private void UpdateHealth()
    {
        healthBar.value = playerHealth.CurrentHealth;
    }

    private void UpdateEnergy()
    {
        energyText.text = "ENERGY: " + playerEnergy.CurrentEnergy;
    }

    private void UpdateLight()
    {
        lightBar.value = darknessController.CurrentRadius;
    }

    private void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(survivalTime / 60);
        int seconds = Mathf.FloorToInt(survivalTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}