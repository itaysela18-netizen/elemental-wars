using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Health system for characters and enemies
/// </summary>
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private UnityEvent onHealthChanged;
    [SerializeField] private UnityEvent onDeath;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Takes damage
    /// </summary>
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        
        currentHealth -= damage;
        onHealthChanged?.Invoke();
        
        Debug.Log($"{gameObject.name} took {damage} damage. Health: {currentHealth}/{maxHealth}");
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Heals the character
    /// </summary>
    public void Heal(int amount)
    {
        if (isDead) return;
        
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        onHealthChanged?.Invoke();
        
        Debug.Log($"{gameObject.name} healed for {amount}. Health: {currentHealth}/{maxHealth}");
    }

    /// <summary>
    /// Character dies
    /// </summary>
    private void Die()
    {
        isDead = true;
        onDeath?.Invoke();
        
        Debug.Log($"{gameObject.name} died!");
        
        // Destroy character or trigger death animation
        Destroy(gameObject, 2f);
    }

    // Getters
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
    public float GetHealthPercent() => (float)currentHealth / maxHealth;
    public bool IsDead => isDead;
}
