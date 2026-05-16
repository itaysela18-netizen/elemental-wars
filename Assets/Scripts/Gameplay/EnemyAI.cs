using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Enemy AI with state machine (Idle, Patrol, Chase, Attack)
/// </summary>
public class EnemyAI : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int baseDamage = 25;
    [SerializeField] private int attackPower = 5;
    [SerializeField] private int defense = 2;

    [Header("AI")]
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float patrolSpeed = 3.5f;
    [SerializeField] private float chaseSpeed = 5f;

    private enum State { Idle, Patrol, Chase, Attack }
    private State currentState = State.Idle;
    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    private HealthSystem healthSystem;
    private float lastAttackTime = -999f;
    private int currentHealth;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthSystem = GetComponent<HealthSystem>();
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        UpdateState();
        ExecuteState();
    }

    /// <summary>
    /// Updates AI state based on conditions
    /// </summary>
    private void UpdateState()
    {
        if (playerTransform == null) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        if (distanceToPlayer <= attackRange)
        {
            currentState = State.Attack;
        }
        else if (distanceToPlayer <= detectionRange)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Patrol;
        }
    }

    /// <summary>
    /// Executes current AI state
    /// </summary>
    private void ExecuteState()
    {
        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Attack();
                break;
        }
    }

    private void Idle()
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(transform.position);
        }
    }

    private void Patrol()
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.speed = patrolSpeed;
            if (!navMeshAgent.hasPath)
            {
                Vector3 randomDirection = Random.insideUnitSphere * 10f;
                randomDirection += transform.position;
                NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 5f, NavMesh.AllAreas);
                navMeshAgent.SetDestination(hit.position);
            }
        }
    }

    private void Chase()
    {
        if (navMeshAgent.isActiveAndEnabled && playerTransform != null)
        {
            navMeshAgent.speed = chaseSpeed;
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }

    private void Attack()
    {
        if (playerTransform == null) return;
        
        // Face player
        transform.LookAt(playerTransform);
        
        // Attack if cooldown is ready
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PerformAttack();
            lastAttackTime = Time.time;
        }
    }

    /// <summary>
    /// Performs attack on player
    /// </summary>
    private void PerformAttack()
    {
        if (playerTransform == null) return;
        
        bool isCritical = CombatSystem.Instance.CheckCriticalStrike();
        int damage = CombatSystem.Instance.CalculateDamage(baseDamage, attackPower, 0, isCritical);
        
        IDamageable playerHealth = playerTransform.GetComponent<IDamageable>();
        if (playerHealth != null)
        {
            CombatSystem.Instance.ApplyDamage(playerHealth, damage, "Physical");
        }
    }

    /// <summary>
    /// Takes damage
    /// </summary>
    public void TakeDamage(int damage, string damageType)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Health: {currentHealth}/{maxHealth}");
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddExperience(100);
        }
        Destroy(gameObject);
    }
}
