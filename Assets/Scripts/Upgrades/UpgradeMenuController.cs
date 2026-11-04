
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeMenuController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerEnergy playerEnergy;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private GameObject upgradePanel;

    [Header("UI")]
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Button damageButton;
    [SerializeField] private Button fireRateButton;
    [SerializeField] private Button rangeButton;

    [Header("Costs")]
    [SerializeField] private int upgradeCost = 5;

    private bool isMenuOpen;

    private void Start()
    {
        isMenuOpen = false;

        if (upgradePanel != null)
            upgradePanel.SetActive(false);

        GamePauseState.SetUpgradeMenu(false);

        RefreshMenu();
    }

    private void Update()
    {
        if (GamePauseState.GameOver)
            return;

        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleMenu();
        }

        if (isMenuOpen)
        {
            RefreshMenu();
        }
    }

    public void ToggleMenu()
    {
        if (isMenuOpen)
            CloseMenu();
        else
            OpenMenu();
    }

    public void OpenMenu()
    {
        if (isMenuOpen || GamePauseState.GameOver)
            return;

        isMenuOpen = true;

        if (upgradePanel != null)
            upgradePanel.SetActive(true);

        GamePauseState.SetUpgradeMenu(true);

        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayUpgradeMenuOpen();
        }

        RefreshMenu();
    }

    public void CloseMenu()
    {
        if (!isMenuOpen)
            return;

        isMenuOpen = false;

        if (upgradePanel != null)
            upgradePanel.SetActive(false);

        GamePauseState.SetUpgradeMenu(false);

        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayUpgradeMenuClose();
        }
    }

    public void BuyDamageUpgrade()
    {
        if (playerEnergy == null || playerCombat == null)
            return;

        if (!playerEnergy.SpendEnergy(upgradeCost))
            return;

        playerCombat.UpgradeDamage();

        PlayPurchaseSound();
        RefreshMenu();
    }

    public void BuyFireRateUpgrade()
    {
        if (playerEnergy == null || playerCombat == null)
            return;

        if (!playerEnergy.SpendEnergy(upgradeCost))
            return;

        playerCombat.UpgradeFireRate();

        PlayPurchaseSound();
        RefreshMenu();
    }

    public void BuyRangeUpgrade()
    {
        if (playerEnergy == null || playerCombat == null)
            return;

        if (!playerEnergy.SpendEnergy(upgradeCost))
            return;

        playerCombat.UpgradeRange();

        PlayPurchaseSound();
        RefreshMenu();
    }

    private void PlayPurchaseSound()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayUpgradePurchase();
        }
    }

    private void RefreshMenu()
    {
        if (playerEnergy == null)
            return;

        int energy = playerEnergy.CurrentEnergy;

        if (energyText != null)
            energyText.text = "AVAILABLE ENERGY: " + energy;

        bool canAfford = energy >= upgradeCost;

        if (damageButton != null)
            damageButton.interactable = canAfford;

        if (fireRateButton != null)
            fireRateButton.interactable = canAfford;

        if (rangeButton != null)
            rangeButton.interactable = canAfford;
    }

    private void OnDisable()
    {
        if (isMenuOpen)
        {
            isMenuOpen = false;
            GamePauseState.SetUpgradeMenu(false);
        }
    }
}
