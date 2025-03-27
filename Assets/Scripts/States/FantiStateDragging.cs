using UnityEngine;

public class FantiStateDragging : FantiState
{
    private Camera _mainCamera;
    private Vector2 _offset;
    private bool _isDragging = false;
    private Vector3 _targetPosition;
    private Vector2 _currentInputPosition;

    public override void Enter()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.LogError("[FantiStateDragging] Main camera not found!");
            AIBehavior.ChangeToMoodState();
            return;
        }

        _isDragging = true;
        _currentInputPosition = InputManager.Instance.TouchPositionAction.ReadValue<Vector2>();
        
        // Calculate initial offset
        Vector2 pointerWorldPos = _mainCamera.ScreenToWorldPoint(_currentInputPosition);
        _offset = (Vector2)AIBehavior.transform.position - pointerWorldPos;
        _targetPosition = AIBehavior.transform.position;

        // Stop any ongoing animations
        AIBehavior.GetComponent<FantiAnimationController>().PlayIdle();
    }

    public override void Update()
    {
        if (!_isDragging) return;

        // Read input and update position
        _currentInputPosition = InputManager.Instance.TouchPositionAction.ReadValue<Vector2>();
        Vector2 pointerWorldPos = _mainCamera.ScreenToWorldPoint(_currentInputPosition);
        Vector2 newTargetPos = pointerWorldPos + _offset;

        // Clamp to movement bounds
        float clampedX = Mathf.Clamp(newTargetPos.x, -3.2f, 3.2f);
        _targetPosition = new Vector3(clampedX, newTargetPos.y, AIBehavior.transform.position.z);
        
        // Move directly to position
        AIBehavior.transform.position = _targetPosition;
    }

    public override void Exit()
    {
        _isDragging = false;
    }
} 