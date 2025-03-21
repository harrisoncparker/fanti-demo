using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
public class FallingBehavior : MonoBehaviour
{
    [SerializeField] private float _fallAcceleration = 2.6f;
    
    private BoxCollider2D _collider;
    private float _currentFallSpeed;
    private bool _isFalling;
    private bool _isOnGround = true;
    private const float RAYCAST_HEIGHT_OFFSET = 2f;

    public bool IsOnGround => _isOnGround;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        CheckGround();
    }

    private void FixedUpdate()
    {
        if (_isFalling)
        {
            ApplyGravity();
        }
    }

    private void CheckGround()
    {
        Vector2 colliderBottom = GetColliderBottomPosition();
        Vector2 groundPosition = FindBoundsFromPoint(colliderBottom, Vector2.down);
        float distanceToGround = Mathf.Abs(Vector2.Distance(groundPosition, colliderBottom));
        
        bool wasOnGround = _isOnGround;
        _isOnGround = distanceToGround <= 0.1f;
        
        if (!wasOnGround && _isOnGround)
        {
            Debug.LogError($"[FallingBehavior] Ground contact detected! Distance: {distanceToGround}");
            SnapToGround(groundPosition);
            StopFalling();
        }
        else if (!_isOnGround && !_isFalling)
        {
            Debug.LogError("[FallingBehavior] Lost ground contact, starting to fall");
            StartFalling();
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

    private Vector2 GetColliderBottomPosition()
    {
        return (Vector2)transform.position + _collider.offset - new Vector2(0, _collider.size.y / 2);
    }

    private void SnapToGround(Vector2 groundPosition)
    {
        Vector3 position = transform.position;
        position.y = groundPosition.y + (_collider.size.y / 2) - _collider.offset.y;
        transform.position = position;
    }

    private Vector2 FindBoundsFromPoint(Vector2 origin, Vector2 direction)
    {
        var hits = Physics2D.RaycastAll(origin, direction, 10f)
            .Where(hit => hit.collider.GetComponent<TilemapCollider2D>() != null);

        var firstHit = hits.FirstOrDefault();
        return firstHit.collider != null ? firstHit.point : origin + direction * 10f;
    }

    private void OnDrawGizmos()
    {
        if (_collider == null) return;
        
        DrawColliderBounds();
        DrawGroundCheck();
    }

    private void DrawColliderBounds()
    {
        Gizmos.color = Color.cyan;
        Vector2 colliderCenter = (Vector2)transform.position + _collider.offset;
        Gizmos.DrawWireCube(colliderCenter, _collider.size);
    }

    private void DrawGroundCheck()
    {
        Vector2 colliderBottom = GetColliderBottomPosition();
        Vector2 groundPosition = FindBoundsFromPoint(colliderBottom, Vector2.down);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(colliderBottom, 0.1f);
        Gizmos.DrawLine(colliderBottom, groundPosition);
        
        Gizmos.color = _isOnGround ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundPosition, 0.1f);
    }
} 