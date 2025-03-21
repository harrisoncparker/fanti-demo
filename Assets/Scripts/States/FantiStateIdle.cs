using UnityEngine;

public class FantiStateIdle : FantiState
{
    private float _timeSinceLastDecision = 0f;
    private float _secondsBetweenDecision;

    public FantiStateIdle(float secondsBetweenDecision)
    {
        _secondsBetweenDecision = secondsBetweenDecision;
    }

    public override void Enter()
    {
        _timeSinceLastDecision = 0f;
    }

    public override void Update()
    {
        _timeSinceLastDecision += Time.deltaTime;
        
        if (_timeSinceLastDecision >= _secondsBetweenDecision)
        {
            _timeSinceLastDecision = 0f;
            DecideNextAction();
        }
    }

    private void DecideNextAction()
    {
        // 80% chance to move, 20% chance to stay idle
        if (Random.value < 0.8f)
        {
            var movingState = new FantiStateMoving();
            AIBehavior.ChangeState(movingState);
        }
    }
} 