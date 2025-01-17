using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    [SerializeField] List<GameEventListener> listeners = new();

    public void Raise(GameObject source) 
    {
        if (!source) Debug.LogWarning("GameEvent.source not set");

        foreach (GameEventListener listener in listeners) 
            listener.OnEventRaised(source);
    }

    public void RegisterListener(GameEventListener listener)
    {
        if(!listeners.Contains(listener))
            listeners.Add(listener);   
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if(listeners.Contains(listener))
            listeners.Remove(listener);   
    }
}
