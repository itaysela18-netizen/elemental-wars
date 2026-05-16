using UnityEngine;

/// <summary>
/// Fire elemental power implementation
/// </summary>
public class FirePower : ElementalPower
{
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private float meterorStormDuration = 5f;
    [SerializeField] private int meteorCount = 10;

    private void Start()
    {
        elementName = "Fire";
        baseDamage = 150;
        cooldown = 4f;
        manaCost = 75;
    }

    /// <summary>
    /// Flame Burst - Single projectile attack
    /// </summary>
    public void FlammBurst(Vector3 targetPosition)
    {
        if (!CanCast()) return;
        
        Vector3 direction = (targetPosition - transform.position).normalized;
        
        // Create fireball
        GameObject fireball = Instantiate(fireBallPrefab, transform.position + direction, Quaternion.identity);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * 20f;
        }
        
        lastCastTime = Time.time;
        Debug.Log("Flame Burst cast!");
    }

    /// <summary>
    /// Meteor Storm - Area of effect attack
    /// </summary>
    public void MeteorStorm(Vector3 targetPosition)
    {
        if (!CanCast()) return;
        
        StartCoroutine(MeteorStormRoutine(targetPosition));
    }

    private System.Collections.IEnumerator MeteorStormRoutine(Vector3 targetPosition)
    {
        float elapsed = 0f;
        while (elapsed < meterorStormDuration)
        {
            for (int i = 0; i < meteorCount / 5; i++)
            {
                Vector3 randomPos = targetPosition + new Vector3(
                    Random.Range(-5f, 5f),
                    10f,
                    Random.Range(-5f, 5f)
                );
                
                // Create meteor
                GameObject meteor = Instantiate(fireBallPrefab, randomPos, Quaternion.identity);
                Rigidbody rb = meteor.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.down * 15f;
                }
            }
            
            elapsed += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        
        lastCastTime = Time.time;
        Debug.Log("Meteor Storm complete!");
    }

    /// <summary>
    /// Fire Breath - Cone attack
    /// </summary>
    public void FireBreath(Vector3 targetPosition)
    {
        if (!CanCast()) return;
        
        // Raycast for enemies in cone
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position + transform.forward * 2f,
            3f,
            transform.forward,
            15f
        );
        
        foreach (RaycastHit hit in hits)
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                CombatSystem.Instance.ApplyDamage(damageable, baseDamage, "Fire");
            }
        }
        
        lastCastTime = Time.time;
        Debug.Log("Fire Breath cast!");
    }

    protected override void ExecutePower(Vector3 targetPosition)
    {
        FlammBurst(targetPosition);
    }
}
