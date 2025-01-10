
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent _gameEvent;
    [SerializeField] UnityEvent _response;

    void OnEnable() 
    {
        _gameEvent.RegisterListener(this);
    }

    void OnDisable() 
    {
        _gameEvent.UnregisterListener(this);
    }

    public void OnEvenRaised() 
    {
        _response.Invoke();
    }
}
