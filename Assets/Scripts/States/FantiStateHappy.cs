using UnityEngine;

public class FantiStateHappy : FantiState
{
    private float _timeSinceLastDecision = 0f;
    private float _secondsBetweenDecision;

    public FantiStateHappy(float secondsBetweenDecision)
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
        // Happy Fantis are more likely to move around (90% chance)
        if (Random.value < 0.9f)
        {
            AIBehavior.ChangeState(new FantiStateMoving());
        }
    }
} 