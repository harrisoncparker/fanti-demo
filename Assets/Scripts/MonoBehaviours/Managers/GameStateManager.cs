using UnityEngine;

public class GameStateManager : MonoBehaviour
{
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
    }

    public Fanti SelectedFanti { get; set; }
    public DeckModel SelectedDeckModel { get; set; }

    public void ResetGame()
    {
        SelectedFanti = null;
        SelectedDeckModel = null;
    }
}