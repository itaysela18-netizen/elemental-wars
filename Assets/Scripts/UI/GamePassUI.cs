using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI for Game Pass system and cosmetic shop
/// </summary>
public class GamePassUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tierText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private Button premiumButton;
    [SerializeField] private Button premiumPlusButton;

    private MonetizationManager monetizationManager;

    private void Start()
    {
        monetizationManager = MonetizationManager.Instance;
        
        // Setup button listeners
        if (premiumButton != null)
        {
            premiumButton.onClick.AddListener(() => OnUpgradeClicked("Premium"));
        }
        
        if (premiumPlusButton != null)
        {
            premiumPlusButton.onClick.AddListener(() => OnUpgradeClicked("PremiumPlus"));
        }
    }

    private void Update()
    {
        UpdateUI();
    }

    /// <summary>
    /// Update UI elements
    /// </summary>
    private void UpdateUI()
    {
        if (monetizationManager == null) return;
        
        if (tierText != null)
        {
            tierText.text = $"Tier: {monetizationManager.GetCurrentTier()}";
        }
        
        if (levelText != null)
        {
            levelText.text = $"Level: {monetizationManager.GetBattlePassLevel()}";
        }
        
        if (currencyText != null)
        {
            int gold = monetizationManager.GetGoldBalance();
            int gems = monetizationManager.GetGemBalance();
            currencyText.text = $"Gold: {gold}  Gems: {gems}";
        }
    }

    /// <summary>
    /// Handles upgrade button click
    /// </summary>
    private void OnUpgradeClicked(string tier)
    {
        Debug.Log($"Upgrade to {tier} clicked");
        monetizationManager.UpgradeGamePass(tier);
        
        // TODO: Handle payment processing
    }
}
