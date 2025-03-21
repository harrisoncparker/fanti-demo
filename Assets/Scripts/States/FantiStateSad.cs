using UnityEngine;

public class FantiStateSad : FantiState
{
    private float _timeSinceLastDecision = 0f;
    private float _secondsBetweenDecision;

    public FantiStateSad(float secondsBetweenDecision)
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
        // Sad Fantis are less likely to move around (50% chance)
        if (Random.value < 0.5f)
        {
            AIBehavior.ChangeState(new FantiStateMoving());
        }
    }
} 