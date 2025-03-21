using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Fanti))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(FallingBehavior))]
public class FantiAIBehavior : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _secondsBetweenDecision = 3f;
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private bool _showDebug = true;
    
    private Fanti _fanti;
    private FallingBehavior _fallingBehavior;
    private FantiAnimationController _animationController;
    private FantiState _currentState;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _fanti = GetComponent<Fanti>();
        _fallingBehavior = GetComponent<FallingBehavior>();
        _animationController = GetComponent<FantiAnimationController>();

        if (_fanti == null || _fallingBehavior == null || _animationController == null)
        {
            Debug.LogError("Missing required components on Fanti GameObject!");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        ChangeState(new FantiStateIdle(_secondsBetweenDecision));
    }

    private void Update()
    {
        _currentState?.Update();
    }

    public void ChangeState(FantiState newState)
    {
        _currentState?.Exit();

        _currentState = newState;
        _currentState.Initialize(_fanti, this);
        _currentState.Enter();
    }

    private void OnDrawGizmos()
    {
        if (!_showDebug) return;

        // Draw debug text
        string stateName = _currentState?.GetType().Name ?? "No State";
        string groundState = _fallingBehavior?.IsOnGround ?? false ? "Grounded" : "In Air";
        
        GUIStyle style = new();
        style.normal.textColor = GetStateColor();
        style.fontSize = 16;
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleLeft;

        Vector3 textPosition = transform.position + Vector3.right * 1.2f + Vector3.up * 1.75f;
        Handles.Label(textPosition, $"State: {stateName}\n{groundState}", style);
    }

    private Color GetStateColor()
    {
        if (_currentState == null) return Color.black;

        return _currentState switch
        {
            FantiStateIdle _ => Color.green,
            FantiStateMoving _ => Color.blue,
            FantiStateFalling _ => Color.red,
            _ => Color.black
        };
    }

    public float MovementSpeed => _movementSpeed;
    public bool IsOnGround => _fallingBehavior.IsOnGround;
} 