using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextDisplay))]
public class ChoiceButton : Button
{
    private TextDisplay _textDisplay;

    protected override void Awake()
    {
        base.Awake();
        _textDisplay = GetComponent<TextDisplay>();
    }

    public void UpdateChoiceText(string text)
    {
        _textDisplay.UpdateText(text);
    }
} 