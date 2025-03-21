using UnityEngine;

public class FantiStateMoving : FantiState
{
    private Vector3 _targetPosition;
    private const float MOVEMENT_BOUNDS_X = 3.2f;

    public override void Enter()
    {
        SetNewTargetPosition();
        AIBehavior.GetComponent<FantiAnimationController>().PlayWalking();
    }

    public override void Update()
    {
        if (HasReachedTargetX())
        {
            CompleteMovement();
            return;
        }
        
        UpdatePosition();
    }

    private void SetNewTargetPosition()
    {
        float direction = ChooseValidDirection();
        _targetPosition = AIBehavior.transform.position;
        _targetPosition.x = CalculateTargetX(direction);
        UpdateSpriteDirection(direction);
    }

    private float ChooseValidDirection()
    {
        float direction = Random.Range(0, 2) * 2 - 1;
        bool wouldExceedBounds = AIBehavior.transform.position.x + direction > MOVEMENT_BOUNDS_X || 
                                AIBehavior.transform.position.x + direction < -MOVEMENT_BOUNDS_X;
        
        return wouldExceedBounds ? -direction : direction;
    }

    private float CalculateTargetX(float direction)
    {
        return Random.Range(AIBehavior.transform.position.x + direction, MOVEMENT_BOUNDS_X * direction);
    }

    private void UpdateSpriteDirection(float direction)
    {
        Vector3 scale = AIBehavior.transform.localScale;
        scale.x = direction * -1;
        AIBehavior.transform.localScale = scale;
    }

    private bool HasReachedTargetX()
    {
        return Mathf.Approximately(_targetPosition.x, AIBehavior.transform.position.x);
    }

    private void CompleteMovement()
    {
        AIBehavior.GetComponent<FantiAnimationController>().PlayIdle();
        AIBehavior.ChangeState(new FantiStateIdle(3f));
    }

    private void UpdatePosition()
    {
        AIBehavior.transform.position = Vector3.MoveTowards(
            AIBehavior.transform.position, 
            _targetPosition, 
            ((FantiAIBehavior)AIBehavior).MovementSpeed * Time.deltaTime
        );
    }
} 