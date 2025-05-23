using UnityEngine;
using UnityEngine.UI;

public class FantiMenu : MonoBehaviour
{
    [Header("Text Displays")]
    [SerializeField] TextDisplay _fantiNameText;
    [SerializeField] TextDisplay _levelTextDisplay;
    [SerializeField] TextDisplay _streakTextDisplay;
    [SerializeField] TextDisplay _expTextDisplay;

    [Header("Elements")]
    [SerializeField] Button _playButton;
    [SerializeField] Button _addDecksButton;

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
        UpdateText(GameStateManager.Instance.SelectedFanti.Model);

        UpdateButtons(GameStateManager.Instance.SelectedFanti.Model);

        // @TODO Currently just waiting a frame to insure everything's 
        // loaded before displaying the fanti menu. Eventually this should 
        // be a more robust loading buffer.
        StartCoroutine(Utilities.WaitForAFrameThen(() => { 
            _fantiMenuLoadedEvent.Raise();
        }));
    }

    void UpdateText(FantiModel fanti)
    {
        _fantiNameText.UpdateText(fanti.name, false);
        _levelTextDisplay.UpdateText(fanti.Level.ToString());
        _streakTextDisplay.UpdateText(fanti.streak.ToString());
        _expTextDisplay.UpdateText(fanti.exp.ToString());
    }

    void UpdateButtons(FantiModel fanti)
    {
        // check if button exists
        _playButton.gameObject.SetActive(fanti.deckIds.Count >= 1);
        _playButton.interactable = fanti.ScheduledCards.Count >= 1;
        
        _addDecksButton.gameObject.SetActive(fanti.deckIds.Count < 1);
    }
}
