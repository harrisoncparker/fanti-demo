using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Configuration/Dialogue Data")]
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
        public GameEvent eventToFire;
    }

    public DialogueLine[] lines;
} 