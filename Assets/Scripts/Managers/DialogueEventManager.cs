using UnityEngine;
using System;

public class DialogueEventManager : MonoBehaviour
{
    public event Action<DialogueData> OnDialogueRequested;
    public event Action OnDialogueCloseRequested;
    public event Action OnDialogueOpenRequested;
    public event Action OnDialogueClosed;

    public void TriggerDialogueRequested(DialogueData dialogue) => OnDialogueRequested?.Invoke(dialogue);
    public void TriggerDialogueCloseRequested() => OnDialogueCloseRequested?.Invoke();
    public void TriggerDialogueOpenRequested() => OnDialogueOpenRequested?.Invoke();
    public void TriggerDialogueClosed() => OnDialogueClosed?.Invoke();
} 