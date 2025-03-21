using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FantiAnimationController : MonoBehaviour
{
    private Animator _animator;
    private readonly float _defaultSpeed = 1f;
    private readonly float _walkingSpeed = 4f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator component not found!");
            enabled = false;
            return;
        }
    }

    public void PlayIdle()
    {
        _animator.speed = _defaultSpeed;
        _animator.Play("Idle");
    }

    public void PlayWalking()
    {
        _animator.speed = _walkingSpeed;
        _animator.Play("Walking");
    }

    public void PlayFalling()
    {
        _animator.speed = _defaultSpeed;
        _animator.Play("Walking");
    }

    public void SetAnimationSpeed(float speed)
    {
        _animator.speed = speed;
    }
} 