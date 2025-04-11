using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Fanti/Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        [TextArea(3, 5)]
        public string text;
        public DialogueChoice[] choices;
    }

    [System.Serializable]
    public class DialogueChoice
    {
        public string choiceText;
        public DialogueData nextDialogue;
        public VisibilityToggler menuToOpen;
    }

    public DialogueLine[] lines;
} 