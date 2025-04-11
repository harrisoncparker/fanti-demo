using UnityEngine;
using System;

public class DialogueEventManager : MonoBehaviour
{
    public event Action<DialogueData> OnDialogueRequested;
    public event Action OnDialogueClosed;

    public void TriggerDialogueRequested(DialogueData dialogue) => OnDialogueRequested?.Invoke(dialogue);
    public void TriggerDialogueClosed() => OnDialogueClosed?.Invoke();
} 