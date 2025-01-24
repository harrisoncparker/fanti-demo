using TMPro;
using UnityEngine;

public class FantiMenu : MonoBehaviour
{
    [Header("Text Displays")]
    [SerializeField] TextDisplay _fantiNameText;
    [SerializeField] TextDisplay _levelTextDisplay;
    [SerializeField] TextDisplay _streakTextDisplay;
    [SerializeField] TextDisplay _expTextDisplay;

    [Header("Events")]
    [SerializeField] GameEvent _fantiMenuLoadedEvent;

    public void Load()
    {
        StartCoroutine(Utilities.WaitForAFrameThen(() => {
            LoadFanti();
        }));
    }

    void LoadFanti()
    {
        UpdateText(GameStateManager.Instance.SelectedFanti);

        // @TODO Currently just waiting a frame to insure everythings 
        // loaded before displaying the fanti menu. Eventually this should 
        // be a more robust loading buffer.
        StartCoroutine(Utilities.WaitForAFrameThen(() => { 
            _fantiMenuLoadedEvent.Raise(gameObject);
        }));
    }

    void UpdateText(Fanti fanti)
    {
        _fantiNameText.UpdateText(fanti.Model.name);
        _levelTextDisplay.UpdateText(fanti.Model.level.ToString());
        _streakTextDisplay.UpdateText(fanti.Model.streak.ToString());
        _expTextDisplay.UpdateText(fanti.Model.exp.ToString());
    }
}
