using UnityEngine;

/// <summary>
/// Centralized input handling for cross-platform support
/// Supports keyboard, mouse, and gamepad inputs
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

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
    /// Gets movement input from keyboard or gamepad
    /// </summary>
    public Vector2 GetMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        return new Vector2(horizontal, vertical).normalized;
    }

    /// <summary>
    /// Checks if attack button is pressed
    /// </summary>
    public bool IsAttackPressed()
    {
        return Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1");
    }

    /// <summary>
    /// Checks if dodge/roll button is pressed
    /// </summary>
    public bool IsDodgePressed()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump");
    }

    /// <summary>
    /// Gets elemental power input (1-8 keys or gamepad buttons)
    /// </summary>
    public int GetElementalPowerInput()
    {
        for (int i = 1; i <= 8; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
            {
                return i;
            }
        }
        return 0;
    }

    /// <summary>
    /// Checks if interact button is pressed
    /// </summary>
    public bool IsInteractPressed()
    {
        return Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire2");
    }

    /// <summary>
    /// Gets mouse position in world space
    /// </summary>
    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, 0);
        plane.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }
}
