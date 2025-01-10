using UnityEngine;

public class UICanvas : MonoBehaviour
{
    UICanvas _instance;

    private void Awake() 
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }
}
