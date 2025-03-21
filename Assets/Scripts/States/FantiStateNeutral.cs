using UnityEngine;

public class FantiStateNeutral : FantiState
{
    private float _timeSinceLastDecision = 0f;
    private float _secondsBetweenDecision;

    public FantiStateNeutral(float secondsBetweenDecision)
    {
        _secondsBetweenDecision = secondsBetweenDecision;
    }

    public override void Enter()
    {
        _timeSinceLastDecision = 0f;
        AIBehavior.GetComponent<FantiAnimationController>().PlayIdle();
    }

    public override void Update()
    {
        if (!AIBehavior.IsOnGround)
        {
            AIBehavior.ChangeState(new FantiStateFalling());
            return;
        }

        _timeSinceLastDecision += Time.deltaTime;
        
        if (_timeSinceLastDecision >= _secondsBetweenDecision)
        {
            _timeSinceLastDecision = 0f;
            DecideNextAction();
        }
    }

    private void DecideNextAction()
    {
        // Neutral Fantis have a balanced chance to move (80% chance, same as original idle)
        if (Random.value < 0.8f)
        {
            AIBehavior.ChangeState(new FantiStateMoving());
        }
    }
} 