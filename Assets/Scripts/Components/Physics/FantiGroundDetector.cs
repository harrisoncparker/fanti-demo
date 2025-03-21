using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(FantiPhysicsBody))]
public class FantiGroundDetector : MonoBehaviour
{
    private bool _onGround = true;
    private FantiPhysicsBody _physicsBody;

    private void Awake()
    {
        _physicsBody = GetComponent<FantiPhysicsBody>();
    }

    private void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        Vector2 colliderBottom = GetColliderBottomPosition();
        Vector2 groundPosition = FindBoundsFromPoint(colliderBottom, Vector2.down);
        float distanceToGround = Mathf.Abs(Vector2.Distance(groundPosition, colliderBottom));
        
        bool wasOnGround = _onGround;
        _onGround = distanceToGround <= 0.1f;
        
        if (!wasOnGround && _onGround)
        {
            SnapToGround(groundPosition);
            _physicsBody.StopFalling();
        }
        else if (!_onGround)
        {
            _physicsBody.StartFalling();
        }
    }

    private Vector2 GetColliderBottomPosition()
    {
        return (Vector2)transform.position + _physicsBody.Collider.offset - new Vector2(0, _physicsBody.Collider.size.y / 2);
    }

    private void SnapToGround(Vector2 groundPosition)
    {
        Vector3 position = transform.position;
        position.y = groundPosition.y + (_physicsBody.Collider.size.y / 2) - _physicsBody.Collider.offset.y;
        transform.position = position;
    }

    private Vector2 FindBoundsFromPoint(Vector2 origin, Vector2 direction)
    {
        var hits = Physics2D.RaycastAll(origin, direction, 10f)
            .Where(hit => hit.collider.GetComponent<TilemapCollider2D>() != null);

        var firstHit = hits.FirstOrDefault();
        return firstHit.collider != null ? firstHit.point : origin + direction * 10f;
    }

    public bool IsOnGround => _onGround;

    private void OnDrawGizmos()
    {
        if (_physicsBody == null || _physicsBody.Collider == null) return;
        
        DrawColliderBounds();
        DrawGroundCheck();
    }

    private void DrawColliderBounds()
    {
        Gizmos.color = Color.cyan;
        Vector2 colliderCenter = (Vector2)transform.position + _physicsBody.Collider.offset;
        Gizmos.DrawWireCube(colliderCenter, _physicsBody.Collider.size);
    }

    private void DrawGroundCheck()
    {
        Vector2 colliderBottom = GetColliderBottomPosition();
        Vector2 groundPosition = FindBoundsFromPoint(colliderBottom, Vector2.down);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(colliderBottom, 0.1f);
        Gizmos.DrawLine(colliderBottom, groundPosition);
        
        Gizmos.color = _onGround ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundPosition, 0.1f);
    }
} 