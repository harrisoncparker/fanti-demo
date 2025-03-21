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
        if (_fallingBehavior == null)
        {
            Debug.LogError("[FantiStateFalling] _fallingBehavior is null!");
            return;
        }

        if (_fallingBehavior.IsOnGround)
        {
            Debug.LogError($"[FantiStateFalling] Detected ground contact, handling landing...");
            HandleLanding();
        }
    }

    private void HandleLanding()
    {
        if (_animationController == null)
        {
            Debug.LogError("[FantiStateFalling] _animationController is null!");
            return;
        }

        if (AIBehavior == null)
        {
            Debug.LogError("[FantiStateFalling] AIBehavior is null!");
            return;
        }

        Debug.LogError($"[FantiStateFalling] Playing idle animation and changing to mood state...");
        _animationController.PlayIdle();
        AIBehavior.ChangeToMoodState();
        Debug.LogError($"[FantiStateFalling] State change completed.");
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