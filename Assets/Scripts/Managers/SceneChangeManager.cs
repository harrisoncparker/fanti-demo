using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    string _currentContentScene = "";

    public GameEvent _loadSceneEvent;

    void Start()
    {
        // If second scene is already loaded set it as content scene
        if (SceneManager.sceneCount >= 2) 
        {
            _currentContentScene = SceneManager.GetSceneAt(1).name;
            return;
        }

        // Set starting scene based on save file state
        bool isFirstTimeVisiting = false; // @TODO check save data to determine
        if (isFirstTimeVisiting)
            LoadTownScene();
        else
            LoadHomeScene();
    }

    void LoadScene(string sceneName = "")
    {
        _loadSceneEvent.Raise(); // @TODO decide if this should be after the next if statment

        if (_currentContentScene == sceneName) return;

        UnloadCurrentContentScene();

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        _currentContentScene = sceneName;
    }

    void UnloadCurrentContentScene()
    {
        if (!string.IsNullOrEmpty(_currentContentScene))
            SceneManager.UnloadSceneAsync(_currentContentScene);
    }

    public void LoadHomeScene()
    {
        LoadScene("Home");
    }

    public void LoadShelterScene()
    {
        LoadScene("Shelter");
    }

    public void LoadFurnitureShopScene()
    {
        LoadScene("Furniture Shop");
    }


    public void LoadTownScene()
    {
        // @TODO Implement town scene
        // LoadScene("Town");
    }
}
