using TMPro;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text _tmpText;

    void Awake() 
    {
        if (_tmpText == null)
        {
            Debug.LogError($"TextDisplay on {gameObject.name} requires TMP_Text to be assigned in the inspector!", this);
            enabled = false;
        }
    }

    public void UpdateText(string text, bool uppercase = true)
    {
        if (_tmpText == null)
        {
            Debug.LogError($"TextDisplay on {gameObject.name} is missing its TMP_Text reference! Did you forget to assign it in the inspector?", this);
            return;
        }

        if (uppercase)
        {
            text = text.ToUpper();
        }
        _tmpText.SetText(text);
    }
}
