
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DarknessController : MonoBehaviour
{
    [Header("Light Reference")]
    [SerializeField] private Light2D playerLight;

    [Header("Visibility Settings")]
    [SerializeField] private float startingRadius = 6f;
    [SerializeField] private float minimumRadius = 2.5f;
    [SerializeField] private float shrinkRate = 0.04f;

    private float currentRadius;

    public float CurrentRadius => currentRadius;

    private float baseShrinkRate;
    private float activeMaxRadius;

    private void Start()
    {
        baseShrinkRate = shrinkRate;
        activeMaxRadius = startingRadius;
        if (playerLight == null)
        {
            Debug.LogError("DarknessController: Player Light is missing!");
            enabled = false;
            return;
        }

        currentRadius = startingRadius;
        playerLight.pointLightOuterRadius = currentRadius;
    }

    private void Update()
    {
        currentRadius -= shrinkRate * Time.deltaTime;

        currentRadius = Mathf.Max(
            currentRadius,
            minimumRadius
        );

        playerLight.pointLightOuterRadius = currentRadius;
    }

    public void RestoreLight(float amount)
    {
        if (amount <= 0) return;

        currentRadius = Mathf.Min(
            currentRadius + amount,
            activeMaxRadius
        );

        playerLight.pointLightOuterRadius = currentRadius;
    }

    public void ApplyDailyModifier(
    float radiusMultiplier,
    float shrinkMultiplier)
    {
        activeMaxRadius = startingRadius * radiusMultiplier;

        currentRadius = Mathf.Min(
            currentRadius,
            activeMaxRadius
        );

        shrinkRate = baseShrinkRate * shrinkMultiplier;

        playerLight.pointLightOuterRadius = currentRadius;
    }
}
