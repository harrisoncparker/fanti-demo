using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class TouchEventHandler : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEvent _onTapEvent;
    [SerializeField] private GameEvent _onHoldStartEvent;
    [SerializeField] private GameEvent _onDragEvent;
    [SerializeField] private GameEvent _onHoldEndEvent;

    [Header("Settings")]
    [SerializeField] private float _holdThreshold = 0.3f;

    // @TODO: Add visual indicator (e.g. radial progress) for hold-to-drag threshold
    // This will require:
    // - UI prefab for the indicator
    // - Animation/shader for progress visualization
    // - Logic to show/hide and update progress during hold
    private bool _isPointerDown = false;
    private float _pointerDownTime = 0f;
    private bool _isHolding = false;
    private Vector2 _pointerStartPosition;
    private Vector2 _lastPointerPosition;

    void OnEnable()
    {
        InputManager.Instance.OnPointerDown += HandlePointerDown;
        InputManager.Instance.OnPointerUp += HandlePointerUp;
    }

    void OnDisable()
    {
        InputManager.Instance.OnPointerDown -= HandlePointerDown;
        InputManager.Instance.OnPointerUp -= HandlePointerUp;
    }

    void Update()
    {
        if (!_isPointerDown) return;

        Vector2 currentPosition = GetCurrentPointerPosition();
        
        if (!IsWithinScreenBounds(currentPosition))
        {
            EndHoldAndReset();
            return;
        }

        if (!_isHolding)
        {
            HandlePotentialHoldStart(currentPosition);
            return;
        }

        HandleDragUpdate(currentPosition);
    }

    private void HandlePotentialHoldStart(Vector2 position)
    {
        if (Time.time - _pointerDownTime < _holdThreshold) return;

        if (!CheckPointerHit(position))
        {
            ResetTouchState();
            return;
        }

        _isHolding = true;
        if (_onHoldStartEvent != null)
        {
            _onHoldStartEvent.RaiseWithSource(gameObject);
        }
    }

    private void HandleDragUpdate(Vector2 currentPosition)
    {
        if (currentPosition == _lastPointerPosition) return;

        Vector2 dragDelta = currentPosition - _lastPointerPosition;
        if (dragDelta.magnitude > 0)
        {
            Vector4 dragData = new Vector4(currentPosition.x, currentPosition.y, dragDelta.x, dragDelta.y);
            if (_onDragEvent != null)
            {
                _onDragEvent.Raise(gameObject, dragData);
            }
        }
        _lastPointerPosition = currentPosition;
    }

    private void EndHoldAndReset()
    {
        if (_onHoldEndEvent != null)
        {
            _onHoldEndEvent.RaiseWithSource(gameObject);
        }
        ResetTouchState();
    }

    void HandlePointerDown(Vector2 position)
    {
        if (!_isPointerDown && CheckPointerHit(position))
        {
            _isPointerDown = true;
            _pointerDownTime = Time.time;
            _pointerStartPosition = position;
            _lastPointerPosition = position;
        }
    }

    void HandlePointerUp(Vector2 position)
    {
        if (_isPointerDown)
        {
            if (CheckPointerHit(position))
            {
                if (_isHolding)
                {
                    if (_onHoldEndEvent != null)
                    {
                        _onHoldEndEvent.RaiseWithSource(gameObject);
                    }
                }
                else
                {
                    if (_onTapEvent != null)
                    {
                        _onTapEvent.RaiseWithSource(gameObject);
                    }
                }
            }
            ResetTouchState();
        }
    }

    private void ResetTouchState()
    {
        _isPointerDown = false;
        _isHolding = false;
        _pointerDownTime = 0f;
    }

    private Vector2 GetCurrentPointerPosition()
    {
        var action = InputManager.Instance.TouchPositionAction;
        return action != null ? action.ReadValue<Vector2>() : Vector2.zero;
    }

    private bool IsWithinScreenBounds(Vector2 position)
    {
        return position.x >= 0 && position.x <= Screen.width &&
               position.y >= 0 && position.y <= Screen.height;
    }

    private bool CheckPointerHit(Vector2 position)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return false;

        if (Camera.main == null)
        {
            Debug.LogError("Camera.main is null. Ensure there is a Camera tagged as MainCamera in the scene.");
            return false;
        }

        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        return hit.collider != null && hit.collider.gameObject == gameObject;
    }
} 