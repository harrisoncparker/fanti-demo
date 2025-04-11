using UnityEngine;

public class FurnitureShopSceneManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private DialogueData _initialDialogue;

    void Start()
    {
        if (_initialDialogue == null)
        {
            Debug.LogError("Initial dialogue not set in FurnitureShopSceneManager");
            return;
        }

        EventManager.Instance.Dialogue.TriggerDialogueRequested(_initialDialogue);
    }

    void OnDestroy()
    {
        EventManager.Instance.Dialogue.TriggerDialogueCloseRequested();
    }
}