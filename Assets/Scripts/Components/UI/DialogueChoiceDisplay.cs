using UnityEngine;
using UnityEngine.UI;

public class DialogueChoiceDisplay : MonoBehaviour
{
    [SerializeField] private Transform _choicesParent;

    public void AddChoice(Button choiceButton)
    {
        choiceButton.transform.SetParent(_choicesParent, false);
    }

    public void ClearChoices()
    {
        foreach (Transform child in _choicesParent)
        {
            Destroy(child.gameObject);
        }
    }
} 