using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] Player _playerManager;
    
    public static GameStateManager Instance { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object alive across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }

        if(_playerManager)
            Instance.CurrentPlayer = _playerManager;
        else
            Debug.LogWarning("GameStateManager cant find player");
    }

    public Fanti SelectedFanti { get; set; }
    public Player CurrentPlayer { get; set; }

    public void ResetGame()
    {
        SelectedFanti = null;
        CurrentPlayer = null;
    }
}