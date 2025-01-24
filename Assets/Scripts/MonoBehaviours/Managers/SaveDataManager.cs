using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{   
    public static SaveDataManager Instance { get; set; }
    public PlayerModel CurrentPlayerModel { get; private set; }

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
            Instance.CurrentPlayerModel = GetTestPlayerModel();
            SaveData();
        }

        // Main awake
        LoadData();
    }

    public void SaveData()
    {
        // Save locally
        LocalSaveSystem.SaveData(Instance.CurrentPlayerModel, _useTestData);
    }

    public void LoadData()
    {
        // Load locally first
        Instance.CurrentPlayerModel = LocalSaveSystem.LoadData();
        Instance._saveDataLoadedEvent.Raise(gameObject);
    }

    PlayerModel GetTestPlayerModel()
    {
        FantiModel fantalita = new("Fantalita");
        FantiModel hugo = new("Hugo", ColourName.Blue);

        fantalita.level = 2;
        fantalita.exp = 120;

        hugo.streak = 4;
        hugo.exp = 40;

        PlayerModel testPlayer = new(
            "Harrison",
            "test@gmail.com",
            new List<FantiModel> { 
                fantalita, 
                hugo
            }
        );

        return testPlayer;
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