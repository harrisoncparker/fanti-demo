using UnityEngine;
using System;

public class SaveEventManager : MonoBehaviour
{
    public event Action OnSaveDataLoaded;
    
    public void TriggerSaveDataLoaded() => OnSaveDataLoaded?.Invoke();
} 