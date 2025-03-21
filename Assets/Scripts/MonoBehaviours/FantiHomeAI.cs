using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Tilemaps;

public class FantiHomeAI : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float _secondsBetweenDecision = 3f;
    [SerializeField] float _movementSpeed = 1f;
    [SerializeField] float _fallAcceleration = 1.6f;

    private const float RAYCAST_HEIGHT_OFFSET = 2f;
    private const float MOVEMENT_BOUNDS_X = 3.2f;
    
    private Vector3 _targetPosition;
    private bool _isMovingToTarget = false;
    private Vector3 _raycastOrigin = Vector3.zero;
    private bool _onGround = true;
    private float _currentFallSpeed;
    private BoxCollider2D _collider;
    private Animator _animator;

    enum State {
        MoodGood,
        MoodBad,
        MoodNeutral,
        BeingMoved,
        Falling,
    }

    private State _currentMood = State.MoodNeutral;
    private State _currentState = State.MoodNeutral;

    void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        
        if (_collider == null || _animator == null)
        {
            Debug.LogError("Missing required components on Fanti GameObject!");
            enabled = false;
            return;
        }

        // Log initial setup
        Debug.Log($"Initial Setup - Collider Size: {_collider.size}, " +
                 $"Offset: {_collider.offset}, " +
                 $"Bounds: {_collider.bounds.min} to {_collider.bounds.max}");
    }

    void Start()
    {
        _currentState = _currentMood;
        StartCoroutine(WaitAndChooseAction());
    }

    void Update() 
    {
        UpdateRaycastOrigin();
        CheckGround();
        UpdateState();
    }

    private void UpdateRaycastOrigin()
    {
        _raycastOrigin = transform.position + Vector3.up * RAYCAST_HEIGHT_OFFSET;
    }

    private void CheckGround()
    {
        Vector2 colliderBottom = GetColliderBottomPosition();
        Vector2 groundPosition = FindBoundsFromPoint(colliderBottom, Vector2.down);
        float distanceToGround = Mathf.Abs(Vector2.Distance(groundPosition, colliderBottom));
        
        bool wasOnGround = _onGround;
        _onGround = distanceToGround <= 0.1f;
        
        if (!wasOnGround && _onGround && _currentState == State.Falling)
        {
            SnapToGround(groundPosition);
        }
    }

    private Vector2 GetColliderBottomPosition()
    {
        return (Vector2)transform.position + _collider.offset - new Vector2(0, _collider.size.y / 2);
    }

    private void SnapToGround(Vector2 groundPosition)
    {
        _currentFallSpeed = 0f;
        Vector3 position = transform.position;
        position.y = groundPosition.y + (_collider.size.y / 2) - _collider.offset.y;
        transform.position = position;
    }

    private void UpdateState()
    {
        switch (_currentState)
        {
            case State.MoodGood:
                UpdateBasedOnMood(State.MoodGood);
                break;
            case State.MoodBad:
                UpdateBasedOnMood(State.MoodBad);
                break;
            case State.MoodNeutral:
                UpdateBasedOnMood(State.MoodNeutral);
                break;
            case State.BeingMoved:
                HandleBeingMoved();
                break;      
            case State.Falling:
                HandleFalling();
                break;
        }
    }

    private void HandleBeingMoved()
    {
        _animator.Play("Walking");
        if (_onGround)
        {
            _currentState = _currentMood;
        }
    }

    private void HandleFalling()
    {
        if (_onGround)
        {
            HandleLanding();
            return;
        }

        StartFallingIfNeeded();
        ApplyGravity();
    }

    private void HandleLanding()
    {
        if (_currentState != State.Falling) return;
        
        _currentState = _currentMood;
        _currentFallSpeed = 0f;
        _animator.speed = 1;
        _animator.Play("Idle");
    }

    private void StartFallingIfNeeded()
    {
        if (_currentState == State.Falling) return;

        _currentState = State.Falling;
        _animator.Play("Walking");
    }

    private void ApplyGravity()
    {
        _currentFallSpeed += _fallAcceleration * Time.deltaTime;
        float verticalMovement = _currentFallSpeed * Time.deltaTime;
        transform.position += Vector3.down * verticalMovement;
    }

    void UpdateBasedOnMood(State mood)
    {
        if (_currentState == State.Falling) return;
        
        MoveToTargetPosition();
        if (!_onGround) 
        {
            _currentState = State.Falling;
        }
    }

    void MoveToTargetPosition()
    {
        if (!_isMovingToTarget) return;

        if (HasReachedTargetX()) 
        {
            CompleteMovement();
            return;
        }
        
        UpdatePosition();
    }

    private bool HasReachedTargetX()
    {
        return Mathf.Approximately(_targetPosition.x, transform.position.x);
    }

    private void CompleteMovement()
    {
        _isMovingToTarget = false;
        ResetAnimation();
    }

    private void UpdatePosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movementSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (_collider == null) _collider = GetComponent<BoxCollider2D>();
        
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
        
        Gizmos.color = _onGround ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundPosition, 0.1f);
    }

    private Vector2 FindBoundsFromPoint(Vector2 origin, Vector2 direction)
    {
        var hits = Physics2D.RaycastAll(origin, direction, 10f)
            .Where(hit => hit.collider.GetComponent<TilemapCollider2D>() != null);

        var firstHit = hits.FirstOrDefault();
        return firstHit.collider != null ? firstHit.point : origin + direction * 10f;
    }

    private Vector2 FindBounds(Vector2 direction)
    {
        return FindBoundsFromPoint(_raycastOrigin, direction);
    }

    IEnumerator WaitAndChooseAction()
    {
        yield return new WaitForSeconds(_secondsBetweenDecision);
        StartCoroutine(MoveToRandom());
    }

    IEnumerator MoveToRandom()
    {
        StopCoroutine(WaitAndChooseAction());
        
        SetRandomTargetPosition();
        StartMovingAnimation();
        
        yield return new WaitUntil(() => !_isMovingToTarget);
        
        ResetAnimation();
        StartCoroutine(WaitAndChooseAction());
    }

    private void SetRandomTargetPosition()
    {
        float direction = ChooseValidDirection();
        _targetPosition = transform.position;
        _targetPosition.x = CalculateTargetX(direction);
        _isMovingToTarget = true;
        UpdateSpriteDirection(direction);
    }

    private float ChooseValidDirection()
    {
        float direction = Random.Range(0, 2) * 2 - 1;
        bool wouldExceedBounds = transform.position.x + direction > MOVEMENT_BOUNDS_X || 
                                transform.position.x + direction < -MOVEMENT_BOUNDS_X;
        
        return wouldExceedBounds ? -direction : direction;
    }

    private float CalculateTargetX(float direction)
    {
        return Random.Range(transform.position.x + direction, MOVEMENT_BOUNDS_X * direction);
    }

    private void UpdateSpriteDirection(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = direction * -1;
        transform.localScale = scale;
    }

    private void StartMovingAnimation()
    {
        _animator.speed = 4;
        _animator.Play("Walking");
    }

    private void ResetAnimation()
    {
        _animator.speed = 1;
        _animator.Play("Idle");
    }
}
