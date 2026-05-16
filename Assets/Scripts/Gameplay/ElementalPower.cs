using UnityEngine;

/// <summary>
/// Base class for all elemental powers
/// </summary>
public abstract class ElementalPower : MonoBehaviour
{
    [SerializeField] protected string elementName;
    [SerializeField] protected int baseDamage = 100;
    [SerializeField] protected float cooldown = 5f;
    [SerializeField] protected int manaCost = 50;
    [SerializeField] protected float castTime = 1f;

    protected float lastCastTime = -999f;
    protected bool isCasting = false;

    /// <summary>
    /// Casts the elemental power
    /// </summary>
    public virtual void Cast(Vector3 targetPosition)
    {
        if (!CanCast())
        {
            Debug.Log($"Cannot cast {elementName} - on cooldown or already casting");
            return;
        }
        
        StartCoroutine(CastRoutine(targetPosition));
    }

    /// <summary>
    /// Check if power can be cast
    /// </summary>
    public virtual bool CanCast()
    {
        return Time.time >= lastCastTime + cooldown && !isCasting;
    }

    /// <summary>
    /// Casting routine with delay
    /// </summary>
    protected virtual System.Collections.IEnumerator CastRoutine(Vector3 targetPosition)
    {
        isCasting = true;
        yield return new WaitForSeconds(castTime);
        
        ExecutePower(targetPosition);
        lastCastTime = Time.time;
        isCasting = false;
    }

    /// <summary>
    /// Execute the power effect (override in derived classes)
    /// </summary>
    protected abstract void ExecutePower(Vector3 targetPosition);

    /// <summary>
    /// Get remaining cooldown time
    /// </summary>
    public float GetRemainingCooldown()
    {
        float remaining = cooldown - (Time.time - lastCastTime);
        return Mathf.Max(0, remaining);
    }

    // Getters
    public string GetElementName() => elementName;
    public int GetBaseDamage() => baseDamage;
    public int GetManaCost() => manaCost;
    public float GetCooldown() => cooldown;
}
