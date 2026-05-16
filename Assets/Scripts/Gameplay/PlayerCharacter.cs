using UnityEngine;

/// <summary>
/// Player character controller handling movement and combat
/// </summary>
public class PlayerCharacter : MonoBehaviour, IDamageable
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Combat")]
    [SerializeField] private int baseDamage = 50;
    [SerializeField] private int attackPower = 10;
    [SerializeField] private int defense = 5;

    [Header("Abilities")]
    [SerializeField] private float dodgeDistance = 3f;
    [SerializeField] private float dodgeDuration = 0.5f;

    private CharacterController characterController;
    private HealthSystem healthSystem;
    private InputManager inputManager;
    private Animator animator;
    private bool isDodging = false;
    private Vector3 currentVelocity;
    private float gravity = -9.81f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        healthSystem = GetComponent<HealthSystem>();
        inputManager = InputManager.Instance;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
        HandleCombat();
        HandleAbilities();
    }

    /// <summary>
    /// Handles player movement
    /// </summary>
    private void HandleMovement()
    {
        Vector2 moveInput = inputManager.GetMovementInput();
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        
        // Apply gravity
        currentVelocity.y += gravity * Time.deltaTime;
        
        // Move character
        if (characterController.isGrounded && currentVelocity.y < 0)
        {
            currentVelocity.y = 0;
        }
        
        Vector3 finalVelocity = (moveDirection * currentSpeed) + (Vector3.up * currentVelocity.y);
        characterController.Move(finalVelocity * Time.deltaTime);
        
        // Rotate towards movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        // Update animation
        if (animator != null)
        {
            animator.SetBool("isMoving", moveInput != Vector2.zero);
            animator.SetBool("isSprinting", isSprinting);
        }
    }

    /// <summary>
    /// Handles combat actions
    /// </summary>
    private void HandleCombat()
    {
        if (inputManager.IsAttackPressed() && !isDodging)
        {
            PerformBasicAttack();
        }
    }

    /// <summary>
    /// Handles special abilities
    /// </summary>
    private void HandleAbilities()
    {
        if (inputManager.IsDodgePressed() && !isDodging)
        {
            StartCoroutine(PerformDodge());
        }
    }

    /// <summary>
    /// Performs basic attack
    /// </summary>
    private void PerformBasicAttack()
    {
        bool isCritical = CombatSystem.Instance.CheckCriticalStrike();
        int damage = CombatSystem.Instance.CalculateDamage(baseDamage, attackPower, 0, isCritical);
        
        Debug.Log($"Player attacks for {damage} damage! {(isCritical ? "CRITICAL!" : "")}");
        
        // TODO: Cast raycast or sphere cast to hit enemies
    }

    /// <summary>
    /// Performs dodge roll
    /// </summary>
    private System.Collections.IEnumerator PerformDodge()
    {
        isDodging = true;
        Vector3 dodgeDirection = (characterController.velocity.normalized != Vector3.zero) 
            ? characterController.velocity.normalized 
            : transform.forward;
        
        float dodgeTimer = 0f;
        
        while (dodgeTimer < dodgeDuration)
        {
            characterController.Move(dodgeDirection * dodgeDistance / dodgeDuration * Time.deltaTime);
            dodgeTimer += Time.deltaTime;
            yield return null;
        }
        
        isDodging = false;
    }

    /// <summary>
    /// Takes damage from any source
    /// </summary>
    public void TakeDamage(int damage, string damageType)
    {
        if (isDodging) return; // Invincible while dodging
        
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damage);
        }
    }

    // Getters
    public int GetBaseDamage() => baseDamage;
    public int GetAttackPower() => attackPower;
    public int GetDefense() => defense;
}
