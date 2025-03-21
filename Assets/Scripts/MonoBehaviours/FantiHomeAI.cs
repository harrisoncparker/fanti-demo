using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Tilemaps;

public class FantiHomeAI : MonoBehaviour
{
    [SerializeField] Animator _animator;
    
    float _secondsBetweenDecision = 3f;
    float _movementSpeed = 1f;
    Vector3 _targetPosition;
    bool _targetPositionActive = false;
    float boundsX = 3.2f;
    Vector3 _raycastOrigin = Vector3.zero;
    float _raycastOriginOffset = 2f;
    bool _onGround = true;

    enum State {
        MoodGood,
        MoodBad,
        MoodNeutral,
        BeingMoved,
        Falling,
    }

    State _currentMood = State.MoodNeutral;
    State _currentState = State.MoodNeutral;

    void Start()
    {
        _currentState = _currentMood;
        StartCoroutine(WaitAndChooseAction());
    }

    void Update() 
    {
        Debug.Log("On ground: " + _onGround);
        UpdateState();
        _raycastOrigin = transform.position + Vector3.up * _raycastOriginOffset;
    }

    void UpdateState()
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
                Debug.Log("Being moved");
                _animator.Play("Walking");
                if (_onGround) {
                    _currentState = _currentMood;
                }
                break;      
            case State.Falling:
                Debug.Log("Falling");
                _animator.Play("Walking");
                if (_onGround) {
                    _currentState = _currentMood;
                }
                break;
        }
    }

    void UpdateBasedOnMood(State mood)
    {
        Debug.Log("Mood: " + mood.ToString());
        MoveToTargetPosition();
        if (!_onGround) {
            _currentState = State.Falling;
        }
    }

    void MoveToTargetPosition()
    {
        if (!_targetPositionActive) return;

        if (Mathf.Approximately(_targetPosition.x, transform.position.x)) {
            _targetPositionActive = false;
        } else {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movementSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_raycastOrigin, 0.5f);
        Gizmos.DrawLine(_raycastOrigin, FindBounds(Vector2.up));
        Gizmos.DrawLine(_raycastOrigin, FindBounds(Vector2.right));
        Gizmos.DrawLine(_raycastOrigin, FindBounds(Vector2.left));

        var _boundsDown = FindBounds(Vector2.down);
        float distance =  Mathf.Abs(
                Vector2.Distance(_boundsDown, _raycastOrigin)
            );
        _onGround = distance <= _raycastOriginOffset;
        if (_onGround) Gizmos.color = Color.blue;

        Gizmos.DrawLine(_raycastOrigin, _boundsDown);
    }

    private Vector2 FindBounds(Vector2 direction)
    {
        var hits = Physics2D.RaycastAll(_raycastOrigin, direction, 10f)
            .Where(hit => hit.collider.GetComponent<TilemapCollider2D>() != null);

        foreach (var hit in hits) {
            Gizmos.color = Color.green;
            return hit.point;
        }

        Gizmos.color = Color.red;
        return (Vector2) _raycastOrigin + direction * 10f;
    }

    IEnumerator WaitAndChooseAction()
    {
        yield return new WaitForSeconds(_secondsBetweenDecision);
        StartCoroutine(MoveToRandom());
    }

    IEnumerator MoveToRandom()
    {
        StopCoroutine(WaitAndChooseAction());

        // Choose a random direction (-1 or 1)
        float direction = Random.Range(0,2)*2-1;
        // Check if I can move at lease 1 in that direction
        if (transform.position.x + direction > boundsX || transform.position.x + direction < -boundsX) {
            // if not reverse direction
            direction *= -1;
        }
        // Set target position to current position
        _targetPosition = transform.position;
        // Choose a valid target X value
        _targetPosition.x = Random.Range(transform.position.x + direction, boundsX * direction);
        // activate target position
        _targetPositionActive = true;

        // Horizonal flip if needed
        Vector3 scale = transform.localScale;
        scale.x = direction * -1;
        transform.localScale = scale;

        // Play walking animation in correct direction
        _animator.speed = 4;
        _animator.Play("Walking");

        yield return new WaitUntil(() => !_targetPositionActive);

        // Play idle animation
        _animator.speed = 1;
        _animator.Play("Idle");

        StartCoroutine(WaitAndChooseAction());
    }
}
