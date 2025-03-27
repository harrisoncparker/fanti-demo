using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent[] _gameEvents;
    [SerializeField] UnityEvent _response;
    [SerializeField] UnityEvent<object> _dataResponse;

    public GameObject _eventSource; 
    // @TODO reconsider the way _eventSource is being used
    // on writing this it was only used for clicking on Fantis
    // It may end up being used for furniture

    // public GameEvent[] GameEvents { get { return _gameEvents; } private set; }

    public GameEvent[] GameEvents {
        get {
            return _gameEvents;
        }
        private set {}
    }

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

    public void OnEventRaised(GameObject source = null)
    {
        _eventSource = source;
        _response.Invoke();
    }

    public void OnEventRaised(GameObject source, object data)
    {
        _eventSource = source;
        _dataResponse?.Invoke(data);
    }

    public static GameObject FindEventSourceInListeners(string eventName, GameEventListener[] eventListeners)
    {
        GameObject eventSource = null;

        foreach (GameEventListener eventListener in eventListeners)
        {
            foreach (GameEvent gameEvent in eventListener.GameEvents)
            {
                if(eventName == gameEvent.name) {
                    eventSource = eventListener._eventSource;
                }
            }
        }

         if (eventSource == null) {
            Debug.LogError(eventName + " is not called by this objects listeners.");
        }

        return eventSource;
    }
}
