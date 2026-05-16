using UnityEngine;

/// <summary>
/// Handles Game Pass system, cosmetics, and currency
/// </summary>
public class MonetizationManager : MonoBehaviour
{
    public static MonetizationManager Instance { get; private set; }

    [Header("Currency")]
    [SerializeField] private int goldBalance = 1000;
    [SerializeField] private int gemBalance = 0;

    [Header("Game Pass")]
    [SerializeField] private string currentTier = "Free"; // Free, Premium, PremiumPlus
    [SerializeField] private int battlePassLevel = 1;
    [SerializeField] private int battlePassXP = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Get Game Pass tier benefits
    /// </summary>
    public float GetProgressionMultiplier()
    {
        return currentTier switch
        {
            "Free" => 1f,
            "Premium" => 2f,
            "PremiumPlus" => 3f,
            _ => 1f
        };
    }

    /// <summary>
    /// Get cosmetic shop discount
    /// </summary>
    public float GetShopDiscount()
    {
        return currentTier switch
        {
            "Free" => 0f,
            "Premium" => 0.1f,
            "PremiumPlus" => 0.2f,
            _ => 0f
        };
    }

    /// <summary>
    /// Add experience to Battle Pass
    /// </summary>
    public void AddBattlePassXP(int amount)
    {
        battlePassXP += (int)(amount * GetProgressionMultiplier());
        
        // Level up if XP threshold reached (e.g., 1000 XP per level)
        while (battlePassXP >= 1000)
        {
            battlePassXP -= 1000;
            battlePassLevel++;
            OnBattlePassLevelUp();
        }
    }

    /// <summary>
    /// Called when Battle Pass levels up
    /// </summary>
    private void OnBattlePassLevelUp()
    {
        Debug.Log($"Battle Pass Level Up! Current Level: {battlePassLevel}");
        // Award rewards based on tier
    }

    /// <summary>
    /// Purchase cosmetic item with gems
    /// </summary>
    public bool PurchaseCosmeticItem(int gemCost)
    {
        if (gemBalance >= gemCost)
        {
            gemBalance -= gemCost;
            Debug.Log($"Purchased cosmetic! Gems remaining: {gemBalance}");
            return true;
        }
        
        Debug.Log("Insufficient gems!");
        return false;
    }

    /// <summary>
    /// Upgrade Game Pass tier
    /// </summary>
    public void UpgradeGamePass(string newTier)
    {
        currentTier = newTier;
        Debug.Log($"Game Pass upgraded to: {newTier}");
    }

    /// <summary>
    /// Add currency (from battles, quests, etc.)
    /// </summary>
    public void AddCurrency(int goldAmount, int gemAmount = 0)
    {
        goldBalance += goldAmount;
        gemBalance += gemAmount;
        Debug.Log($"Currency added. Gold: {goldBalance}, Gems: {gemBalance}");
    }

    // Getters
    public int GetGoldBalance() => goldBalance;
    public int GetGemBalance() => gemBalance;
    public string GetCurrentTier() => currentTier;
    public int GetBattlePassLevel() => battlePassLevel;
}
