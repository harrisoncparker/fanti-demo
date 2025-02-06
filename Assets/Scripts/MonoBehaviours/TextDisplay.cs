using TMPro;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text _tmpText;

    void Awake() 
    {
        _tmpText = (_tmpText == null) ? GetComponent<TMP_Text>() : _tmpText;
    }

    public void UpdateText(string text, bool uppercase = true)
    {
        if (uppercase) {
            text = text.ToUpper();
        }
        _tmpText.SetText(text);
    }
}
