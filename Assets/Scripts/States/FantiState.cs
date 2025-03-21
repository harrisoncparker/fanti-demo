using UnityEngine;

public abstract class FantiState
{
    protected Fanti Fanti { get; private set; }
    protected FantiModel Model => Fanti.Model;
    protected FantiAIBehavior AIBehavior { get; private set; }

    public void Initialize(Fanti fanti, FantiAIBehavior aiBehavior)
    {
        Fanti = fanti;
        AIBehavior = aiBehavior;
    }

    public virtual void Enter() {}
    public virtual void Exit() {}
    public virtual void Update() {}
    public virtual void FixedUpdate() {}
    public virtual void HandleInput() {}
} 