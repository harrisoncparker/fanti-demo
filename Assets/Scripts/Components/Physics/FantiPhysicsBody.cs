using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FantiPhysicsBody : MonoBehaviour
{
    [SerializeField] private float _fallAcceleration = 2.6f;
    
    private float _currentFallSpeed;
    private bool _isFalling;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (_isFalling)
        {
            ApplyGravity();
        }
    }

    public void StartFalling()
    {
        if (_isFalling) return;
        _isFalling = true;
        _currentFallSpeed = 0f;
    }

    public void StopFalling()
    {
        _isFalling = false;
        _currentFallSpeed = 0f;
    }

    private void ApplyGravity()
    {
        _currentFallSpeed += _fallAcceleration * Time.fixedDeltaTime;
        float verticalMovement = _currentFallSpeed * Time.fixedDeltaTime;
        transform.position += Vector3.down * verticalMovement;
    }

    public BoxCollider2D Collider => _collider;
} 