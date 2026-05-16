using UnityEngine;

/// <summary>
/// Core combat system handling damage calculations and combat interactions
/// </summary>
public class CombatSystem : MonoBehaviour
{
    public static CombatSystem Instance { get; private set; }

    [SerializeField] private float criticalStrikeChance = 0.15f;
    [SerializeField] private float criticalDamageMultiplier = 1.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Calculates damage based on attack power and defense
    /// </summary>
    public int CalculateDamage(int baseDamage, int attackPower, int defense, bool isCritical = false)
    {
        float damageMultiplier = 1f + (attackPower / 100f);
        float defenseReduction = 1f - (defense / (defense + 100f));
        
        int damage = (int)(baseDamage * damageMultiplier * defenseReduction);
        
        if (isCritical)
        {
            damage = (int)(damage * criticalDamageMultiplier);
        }
        
        return Mathf.Max(1, damage); // Ensure minimum 1 damage
    }

    /// <summary>
    /// Checks if attack is a critical strike
    /// </summary>
    public bool CheckCriticalStrike()
    {
        return Random.value < criticalStrikeChance;
    }

    /// <summary>
    /// Calculates elemental advantage damage bonus
    /// </summary>
    public float GetElementalAdvantage(string attackerElement, string defenderElement)
    {
        // Element advantage matrix
        if (attackerElement == "Fire" && defenderElement == "Nature") return 1.2f;
        if (attackerElement == "Water" && defenderElement == "Fire") return 1.2f;
        if (attackerElement == "Lightning" && defenderElement == "Water") return 1.2f;
        if (attackerElement == "Earth" && defenderElement == "Lightning") return 1.2f;
        if (attackerElement == "Wind" && defenderElement == "Earth") return 1.2f;
        if (attackerElement == "Ice" && defenderElement == "Wind") return 1.2f;
        if (attackerElement == "Nature" && defenderElement == "Ice") return 1.2f;
        if (attackerElement == "Light" && defenderElement == "Void") return 1.5f;
        
        return 1f; // No advantage
    }

    /// <summary>
    /// Applies damage to a target
    /// </summary>
    public void ApplyDamage(IDamageable target, int damage, string damageType)
    {
        if (target != null)
        {
            target.TakeDamage(damage, damageType);
            Debug.Log($"Damage applied: {damage} ({damageType})");
        }
    }
}

/// <summary>
/// Interface for objects that can take damage
/// </summary>
public interface IDamageable
{
    void TakeDamage(int damage, string damageType);
}
