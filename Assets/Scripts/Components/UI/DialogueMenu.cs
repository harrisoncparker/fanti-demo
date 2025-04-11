using UnityEngine;
using UnityEngine.UI;

public class DialogueMenu : MonoBehaviour
{
    [Header("Text Displays")]
    [SerializeField] private TextDisplay _speakerTextDisplay;
    [SerializeField] private Button _choiceButtonPrefab;
    
    [Header("Containers")]
    [SerializeField] private VisibilityToggler _dialogueContainer;
    [SerializeField] private TextDisplay _speakerBox;
    [SerializeField] private DialogueChoiceDisplay _choicesBox;

    private DialogueData _currentDialogue;
    private int _currentLineIndex;

    private void OnEnable()
    {
        EventManager.Instance.Dialogue.OnDialogueRequested += HandleDialogueRequested;
    }

    private void OnDisable()
    {
        EventManager.Instance.Dialogue.OnDialogueRequested -= HandleDialogueRequested;
    }

    private void HandleDialogueRequested(DialogueData dialogue)
    {
        StartCoroutine(Utilities.WaitForAFrameThen(() => {
            _currentDialogue = dialogue;
            _currentLineIndex = 0;
            _dialogueContainer.Open();
            DisplayCurrentLine();
        }));
    }

    private void DisplayCurrentLine()
    {
        var line = _currentDialogue.lines[_currentLineIndex];
        _speakerTextDisplay.UpdateText(line.text);
        SetupChoices(line.choices);
    }

    private void SetupChoices(DialogueChoice[] choices)
    {
        _choicesBox.ClearChoices();
        
        foreach (var choice in choices)
        {
            var choiceButton = Instantiate(_choiceButtonPrefab);
            var textDisplay = choiceButton.GetComponent<TextDisplay>();
            
            textDisplay.UpdateText(choice.choiceText);
            choiceButton.onClick.AddListener(() => HandleChoice(choice));
            
            _choicesBox.AddChoice(choiceButton);
        }
    }

    private void HandleChoice(DialogueChoice choice)
    {
        if (choice.menuToOpen != null)
            choice.menuToOpen.Open();

        if (choice.nextDialogue != null)
            EventManager.Instance.Dialogue.TriggerDialogueRequested(choice.nextDialogue);
        else
            Close();
    }

    private void Close()
    {
        _dialogueContainer.Close();
        EventManager.Instance.Dialogue.TriggerDialogueClosed();
    }
} 