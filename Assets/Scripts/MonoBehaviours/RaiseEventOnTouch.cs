using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RaiseEventOnTouch : MonoBehaviour
{
    [SerializeField] GameEvent _onPointerUpEvent;
    private bool _isPointerDown = false;

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

    void HandlePointerDown(Vector2 position)
    {
        if (!_isPointerDown && CheckPointerHit(position))
            _isPointerDown = true;
    }

    void HandlePointerUp(Vector2 position)
    {
        if (_isPointerDown && CheckPointerHit(position))
        {
            _onPointerUpEvent?.Raise(gameObject);
            _isPointerDown = false;
        }
    }

    bool CheckPointerHit(Vector2 position)
    {
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
