using UnityEngine;

[RequireComponent(typeof(Fanti))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class FantiAIBehavior : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _secondsBetweenDecision = 3f;
    [SerializeField] private float _movementSpeed = 1f;
    
    private Fanti _fanti;
    private FantiPhysicsBody _physicsBody;
    private FantiGroundDetector _groundDetector;
    private FantiAnimationController _animationController;
    private FantiState _currentState;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _fanti = GetComponent<Fanti>();
        _physicsBody = GetComponent<FantiPhysicsBody>();
        _groundDetector = GetComponent<FantiGroundDetector>();
        _animationController = GetComponent<FantiAnimationController>();

        if (_fanti == null || _physicsBody == null || _groundDetector == null || _animationController == null)
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

    public float MovementSpeed => _movementSpeed;
} 