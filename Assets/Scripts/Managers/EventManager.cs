using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    
    public PlayerEventManager Player { get; private set; }
    public SaveEventManager Save { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManagers();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeManagers()
    {
        Player = gameObject.AddComponent<PlayerEventManager>();
        Save = gameObject.AddComponent<SaveEventManager>();
    }
} 