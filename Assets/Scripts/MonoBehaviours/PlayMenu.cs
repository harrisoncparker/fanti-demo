using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayMenu : MonoBehaviour
{

    [Header("Text Displays")]
    [SerializeField] TextDisplay _questionTextDisplay;
    [SerializeField] TextDisplay _answerTextDisplay;
    [SerializeField] TextDisplay _cardedPlayedTextDisplay;
    [SerializeField] TextDisplay _goldEarnedTextDisplay;
    [SerializeField] TextDisplay _expGainedTextDisplay;
    [SerializeField] TextDisplay _streakTextDisplay;

    [Header("Elements")]
    [SerializeField] GameObject _selfAssessmentGameObject;
    [SerializeField] Button _answerRevealButton;
    [SerializeField] GameObject _overlayGameObject;
    [SerializeField] Button _exitButton;
    [SerializeField] RectTransform _fantiCloseupRectTransform;

    [Header("Events")]
    [SerializeField] GameEvent _playMenuLoadedEvent;

    List<CardModel> _cardsToPlay;
    int _playIndex = 0;
    int _score = 0;

    /**
     * Fanti animation fields
     */
    readonly float _animationSeconds = .2f;
    Vector3 _fantiCloseupOriginalScale;
    float _fantiCloseupOriginalY;

    /**
     * Awake
     */
    void Awake() {
        _fantiCloseupOriginalScale = _fantiCloseupRectTransform.localScale;
        _fantiCloseupOriginalY = _fantiCloseupRectTransform.anchoredPosition.y;
    }

    /**
     * Load the play menu
     */
    public void Load()
    {
        StartCoroutine(Utilities.WaitForAFrameThen(() => {
            LoadDecks();
        }));
    }

    /**
     * Reveal the answer
     */
    public void RevealAnswer()
    {
        _selfAssessmentGameObject.SetActive(true);
        _answerRevealButton.gameObject.SetActive(false);
    }

    /**
     * @param score - The score of the self-assessment
     */
    public void SubmitSelfAssesment(int score)
    {
        _playIndex++;
        _score += score;

        Debug.Log("Score: " + _score);

        if (_playIndex >= _cardsToPlay.Count) {
            FinishPlaySession();
            return;
        }

        LoadCard(_cardsToPlay[_playIndex]);
    }

    /**
     * Finish the play session
     */
    void FinishPlaySession()
    {
        _overlayGameObject.SetActive(true);
        _exitButton.gameObject.SetActive(false);

        PlayFantiAnimation();

        int cardsPlayed = _playIndex + 1;
        int goldEarned  = cardsPlayed * 10; // @TODO: GameStateManager.Instance.CurrentPlayer.EarnGoldForCards(cardsPlayed);
        int expGained   = GameStateManager.Instance.SelectedFanti.EarnExp(100);
        int streak      = GameStateManager.Instance.SelectedFanti.IncrementStreak();

        _cardedPlayedTextDisplay.UpdateText(cardsPlayed.ToString());
        _goldEarnedTextDisplay.UpdateText(goldEarned.ToString());
        _expGainedTextDisplay.UpdateText(expGained.ToString());
        _streakTextDisplay.UpdateText(streak.ToString());

        // @TODO: Notify the player if the Fanti leveled up
        // @TODO: Save the player and fanti data
    }

    /**
     * @param reset - Reset the animation to the original state
     */
    void PlayFantiAnimation(bool reset = false)
    {
        if(!_fantiCloseupRectTransform) {
            Debug.LogWarning("No RectTransform set for the Fanti closeup");
            return;
        }

        Vector3 targetScale = new(2, 2, 2);
        float targetY = 80;

        if(reset) {
            targetScale = _fantiCloseupOriginalScale;
            targetY = _fantiCloseupOriginalY;
        } 

        LeanTween.scale(_fantiCloseupRectTransform, targetScale, _animationSeconds);
        LeanTween.moveY(_fantiCloseupRectTransform, targetY, _animationSeconds);

        // @TODO Play sprite animation ...
    }

    /**
     * Load the decks
     */
    void LoadDecks()
    {
        Reset();

        _cardsToPlay = SelectCards(GameStateManager.Instance.SelectedDecks);

        LoadCard(_cardsToPlay[_playIndex]);

        StartCoroutine(Utilities.WaitForAFrameThen(() => { 
            _playMenuLoadedEvent.Raise(gameObject);
        }));
    }

    /**
     * Reset the play session
     */
    void Reset() {
        _playIndex = 0;
        _score = 0;
        _overlayGameObject.SetActive(false);
        _exitButton.gameObject.SetActive(true);
        PlayFantiAnimation(true);
        HideAnswer();
    }

    /**
     * Hide the answer
     */
    void HideAnswer()
    {
        _selfAssessmentGameObject.SetActive(false);
        _answerRevealButton.gameObject.SetActive(true);
    }

    /**
     * @param decks - The decks to select cards from
     * @return List<CardModel> - The selected cards
     */
    List<CardModel> SelectCards(List<DeckModel> decks) {
        // @TODO only select the cards that need to be played

        DeckModel combinedDeck = new("combined");

        decks.ForEach(deck => {
            combinedDeck.cards.AddRange(deck.cards);
        });

        return combinedDeck.GetShuffledCards();
    }

    /**
     * @param card - The card to load
     */
    void LoadCard(CardModel card)
    {
        if (card.doubleSided && Random.value > 0.5f) {
            _questionTextDisplay.UpdateText(card.back);
            _answerTextDisplay.UpdateText(card.front);
        } else {
            _questionTextDisplay.UpdateText(card.front);
            _answerTextDisplay.UpdateText(card.back);
        }

        HideAnswer();
    }
}
