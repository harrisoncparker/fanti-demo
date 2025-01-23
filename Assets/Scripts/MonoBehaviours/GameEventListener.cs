
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent _gameEvent;
    [SerializeField] UnityEvent _response;

    public GameObject _eventSource;

    void OnEnable() 
    {
        _gameEvent.RegisterListener(this);
    }

    void OnDisable() 
    {
        _gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(GameObject source)
    {
        _eventSource = source;
        _response.Invoke();
    }
}
