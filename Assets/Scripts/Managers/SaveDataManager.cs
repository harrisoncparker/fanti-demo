using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{   
    public static SaveDataManager Instance { get; set; }

    [Header("Events")]
    [SerializeField] GameEvent _saveDataLoadedEvent;

    [Header("Testing")]
    [SerializeField] bool _useTestData = true;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Test data
        if (_useTestData) {
            GameStateManager.Instance.CurrentPlayer.Model = PlayerModel.Fake();
            SaveData();
        }

        // Main awake
        LoadData();
    }

    public void SaveData()
    {
        LocalSaveSystem.SaveData(GameStateManager.Instance.CurrentPlayer.Model, _useTestData);
    }

    public void LoadData()
    {
        GameStateManager.Instance.CurrentPlayer.Model = LocalSaveSystem.LoadData();
        Instance._saveDataLoadedEvent.Raise();
    }
}

public class LocalSaveSystem
{
    private static string SavePath => Path.Combine(Application.dataPath, "Data/testPlayerData.json");

    public static void SaveData(PlayerModel playerModel, bool isUsingTestData = false)
    {
        File.WriteAllText(
            SavePath, 
            playerModel.ToJson(isUsingTestData)
        );
    }

    public static PlayerModel LoadData()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<PlayerModel>(json);
        }
        return new PlayerModel(
            "[no username]",
            "[no email address]"
        );
    }
}