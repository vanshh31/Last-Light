
using UnityEngine;

public class LightRestoration : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerEnergy playerEnergy;
    [SerializeField] private DarknessController darknessController;

    [Header("Restoration Settings")]
    [SerializeField] private int energyCost = 3;
    [SerializeField] private float restorationAmount = 1.5f;

    private void Awake()
    {
        if (playerEnergy == null)
            playerEnergy = GetComponent<PlayerEnergy>();

        if (darknessController == null)
            darknessController = FindFirstObjectByType<DarknessController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TryRestoreLight();
        }
    }

    public void TryRestoreLight()
    {
        if (playerEnergy == null || darknessController == null)
        {
            Debug.LogError("LightRestoration: Missing references!");
            return;
        }

        if (playerEnergy.SpendEnergy(energyCost))
        {
            darknessController.RestoreLight(restorationAmount);
            Debug.Log("Light restoration successful!");
        }
        else
        {
            Debug.Log("Not enough energy to restore light!");
        }
    }
}
