using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerEnergy playerEnergy;
    [SerializeField] private PlayerCombat playerCombat;

    [Header("Upgrade Costs")]
    [SerializeField] private int damageCost = 5;
    [SerializeField] private int fireRateCost = 5;
    [SerializeField] private int rangeCost = 5;

    private void Awake()
    {
        if (playerEnergy == null)
            playerEnergy = GetComponent<PlayerEnergy>();

        if (playerCombat == null)
            playerCombat = GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            BuyDamageUpgrade();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            BuyFireRateUpgrade();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            BuyRangeUpgrade();
    }

    private void BuyDamageUpgrade()
    {
        if (playerEnergy.SpendEnergy(damageCost))
            playerCombat.UpgradeDamage();
        else
            Debug.Log("Not enough energy for Damage Upgrade!");
    }

    private void BuyFireRateUpgrade()
    {
        if (playerEnergy.SpendEnergy(fireRateCost))
            playerCombat.UpgradeFireRate();
        else
            Debug.Log("Not enough energy for Fire Rate Upgrade!");
    }

    private void BuyRangeUpgrade()
    {
        if (playerEnergy.SpendEnergy(rangeCost))
            playerCombat.UpgradeRange();
        else
            Debug.Log("Not enough energy for Range Upgrade!");
    }
}