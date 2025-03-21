using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] Camera mainCamera;

    public delegate void PointerDown(Vector2 position);
    public event PointerDown OnPointerDown;

    public delegate void PointerUp(Vector2 position);
    public event PointerUp OnPointerUp;

    PlayerInput _playerInput;
    InputAction _touchAction;
    public InputAction TouchPositionAction => _touchPositionAction;
    InputAction _touchPositionAction;
    Vector2 _lastPointerPositon;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _touchAction = _playerInput.actions.FindAction("Touch");
        _touchPositionAction = _playerInput.actions.FindAction("TouchPosition");

        if (_touchAction != null)
        {
            _touchAction.performed += OnTouchPerformed;
            _touchAction.canceled += OnTouchCanceled;
        }
    }

    public void OnTouchPerformed(InputAction.CallbackContext context)
    {
        StartCoroutine(UpdatePointerPosition(() => {
            OnPointerDown?.Invoke(_lastPointerPositon);
        }));
    }

    public void OnTouchCanceled(InputAction.CallbackContext context)
    {
        StartCoroutine(UpdatePointerPosition(() => {
            OnPointerUp?.Invoke(_lastPointerPositon);
        }));
    }

    IEnumerator UpdatePointerPosition(Action callback)
    {
        // Wait until the next frame
        yield return null;

        _lastPointerPositon = _touchPositionAction?.ReadValue<Vector2>() ?? Vector2.zero;

        if (_lastPointerPositon == Vector2.zero)
        {
            Debug.LogWarning("'_lastPointerPositon' is still Vector2.zero after waiting. Ensure the input system is configured correctly.");
            yield break;
        }

        callback();
    }
}