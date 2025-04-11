using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VisibilityToggler))]
public class DialogueMenu : MonoBehaviour
{
    [Header("Displays")]
    [SerializeField] private TextDisplay _speakerTextDisplay;
    [SerializeField] private DialogueChoiceDisplay _choicesDisplay;

    [Header("Prefabs")]
    [SerializeField] private ChoiceButton _choiceButtonPrefab;

    private DialogueData _currentDialogue;
    private int _currentLineIndex;
    private VisibilityToggler _visibilityToggler;

    private void Awake()
    {
        _visibilityToggler = GetComponent<VisibilityToggler>();
    }

    private void OnEnable()
    {
        StartCoroutine(Utilities.WaitForManagerThen<EventManager>(manager => {
            manager.Dialogue.OnDialogueRequested += HandleDialogueRequested;
            manager.Dialogue.OnDialogueCloseRequested += Close;
            manager.Dialogue.OnDialogueOpenRequested += Open;
        }));
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.Dialogue.OnDialogueRequested -= HandleDialogueRequested;
            EventManager.Instance.Dialogue.OnDialogueCloseRequested -= Close;
            EventManager.Instance.Dialogue.OnDialogueOpenRequested -= Open;
        }
    }

    private void HandleDialogueRequested(DialogueData dialogue)
    {
        StartCoroutine(Utilities.WaitForAFrameThen(() => {
            _currentDialogue = dialogue;
            _currentLineIndex = 0;
            _visibilityToggler.Open();
            DisplayCurrentLine();
        }));
    }

    private void DisplayCurrentLine()
    {
        var line = _currentDialogue.lines[_currentLineIndex];
        _speakerTextDisplay.UpdateText(line.text);
        SetupChoices(line.choices);
    }

    private void SetupChoices(DialogueData.DialogueChoice[] choices)
    {
        _choicesDisplay.ClearChoices();
        
        foreach (var choice in choices)
        {
            var choiceButton = Instantiate(_choiceButtonPrefab);
            choiceButton.UpdateChoiceText(choice.choiceText);
            choiceButton.onClick.AddListener(() => HandleChoice(choice));
            
            _choicesDisplay.AddChoice(choiceButton);
        }
    }

    private void HandleChoice(DialogueData.DialogueChoice choice)
    {
        if (choice.eventToFire != null)
            choice.eventToFire.Raise();
        
        if (choice.nextDialogue != null)
            EventManager.Instance.Dialogue.TriggerDialogueRequested(choice.nextDialogue);
        else 
            Close();
    }

    private void Close()
    {
        _visibilityToggler.Close();
        EventManager.Instance.Dialogue.TriggerDialogueClosed();
    }

    private void Open()
    {
        _visibilityToggler.Open();
    }
} 