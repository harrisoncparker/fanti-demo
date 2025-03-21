using UnityEngine;

public class FantiStateFalling : FantiState
{
    private FallingBehavior _fallingBehavior;
    private FantiAnimationController _animationController;

    public override void Enter()
    {
        // Cache component references
        _fallingBehavior = AIBehavior.GetComponent<FallingBehavior>();
        _animationController = AIBehavior.GetComponent<FantiAnimationController>();

        if (_fallingBehavior == null || _animationController == null)
        {
            Debug.LogError("Missing required components for falling state!");
            return;
        }

        // Start falling animation and physics
        _animationController.PlayFalling();
        _fallingBehavior.StartFalling();
    }

    public override void Update()
    {
        if (_fallingBehavior.IsOnGround)
        {
            HandleLanding();
        }
    }

    private void HandleLanding()
    {
        _animationController.PlayIdle();
        
        // Transition back to idle state (will be mood state later)
        AIBehavior.ChangeState(new FantiStateIdle(3f));
    }

    public override void Exit()
    {
        // Ensure falling is stopped when leaving state
        if (_fallingBehavior != null)
        {
            _fallingBehavior.StopFalling();
        }
    }
} 