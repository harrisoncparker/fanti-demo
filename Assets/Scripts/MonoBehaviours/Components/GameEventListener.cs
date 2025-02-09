
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent[] _gameEvents;
    [SerializeField] UnityEvent _response;

    public GameObject _eventSource; 
    // @TODO reconsider the way _eventSource is being used
    // on writing this it was only used for clicking on Fantis
    // It may end up being used for furniture

    void OnEnable() 
    {
        foreach (GameEvent gameEvent in _gameEvents)
        {
            gameEvent.RegisterListener(this);
        }
    }

    void OnDisable() 
    {
        foreach (GameEvent gameEvent in _gameEvents)
        {
            gameEvent.UnregisterListener(this);
        }
    }

    public void OnEventRaised(GameObject source)
    {
        _eventSource = source;
        _response.Invoke();
    }
}
