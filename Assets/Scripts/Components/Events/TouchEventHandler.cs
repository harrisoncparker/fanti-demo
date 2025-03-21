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
        if (_isPointerDown)
        {
            Vector2 currentPosition = GetCurrentPointerPosition();
            
            if (!_isHolding)
            {
                if (Time.time - _pointerDownTime >= _holdThreshold)
                {
                    _isHolding = true;
                    _onHoldStartEvent?.RaiseWithSource(gameObject);
                }
            }
            else if (currentPosition != _lastPointerPosition)
            {
                Vector2 dragDelta = currentPosition - _lastPointerPosition;
                if (dragDelta.magnitude > 0)
                {
                    // Pack both current position and delta into a Vector4
                    Vector4 dragData = new Vector4(currentPosition.x, currentPosition.y, dragDelta.x, dragDelta.y);
                    _onDragEvent?.Raise(gameObject, dragData);
                }
                _lastPointerPosition = currentPosition;
            }
        }
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
                    _onHoldEndEvent?.RaiseWithSource(gameObject);
                }
                else
                {
                    _onTapEvent?.RaiseWithSource(gameObject);
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
        return InputManager.Instance.TouchPositionAction?.ReadValue<Vector2>() ?? Vector2.zero;
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