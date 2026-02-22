using UnityEngine;

[RequireComponent(requiredComponent: typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health;
    public float moveSpeed = 5f;
    public float jumpForce = 6f;
    public float gravity = -30f;
    public Transform cameraTransform;

    private CharacterController _controller;
    private Vector3 _velocity;

    void Start()
    {
        health = maxHealth;
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHealth(current: health,  max: maxHealth);

        _controller = GetComponent<CharacterController>();
        _velocity = Vector3.zero;
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // input
        float moveZ = Input.GetKey(key: KeyCode.W) ? 1f : Input.GetKey(key: KeyCode.S) ? -1f : 0f;
        float moveX = Input.GetKey(key: KeyCode.D) ? 1f : Input.GetKey(key: KeyCode.A) ? -1f : 0f;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        
        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * moveZ + camRight * moveX;

        if (_controller.isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }

        if (_controller.isGrounded && Input.GetKeyDown(key: KeyCode.Space))
        {
            _velocity.y = jumpForce;
        }

        _velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = (move * moveSpeed + Vector3.up * _velocity.y) * Time.deltaTime;
        _controller.Move(motion: finalMove);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHealth(current: health, max: maxHealth);
        
        // Debug.Log(message: "Health: " + health);

        if (health <= 0f)
        {
            health = 0f;
            UIManager.Instance.ShowGameOver();
            _controller.enabled = false;
            
            cameraTransform.GetComponent<CameraLook>().enabled = false;
            FindObjectOfType<PlayerRaycast>().canShoot  = false;
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    public void Heal(float amount)
    {
        if (health <= 0)
            return;
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
        
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHealth(current: health, max: maxHealth);
        
        // Debug.Log(message: "Health: " + health);
    }
}
