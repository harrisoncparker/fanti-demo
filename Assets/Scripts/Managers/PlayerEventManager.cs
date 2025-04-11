using UnityEngine;
using System;

public class PlayerEventManager : MonoBehaviour
{
    public event Action OnGoldUpdated;
    public event Action OnInventoryUpdated;

    public void TriggerGoldUpdated() => OnGoldUpdated?.Invoke();
    public void TriggerInventoryUpdated() => OnInventoryUpdated?.Invoke();
} 