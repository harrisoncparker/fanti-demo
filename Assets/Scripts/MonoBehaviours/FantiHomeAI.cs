using UnityEngine;
using System.Collections;

public class FantiHomeAI : MonoBehaviour
{
    [SerializeField] Animator _animator;
    
    float _secondsBetweenDecision = 3f;
    float _movementSpeed = 1f;
    Vector3 _targetPosition;
    bool _targetPositionActive = false;
    float boundsX = 3.2f;

    void Start()
    {
        StartCoroutine(WaitAndChooseAction());
    }

    void Update() 
    {
        MoveToTargetPosition();
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
