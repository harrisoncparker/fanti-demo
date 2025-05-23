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
    [SerializeField] TextDisplay _levelUpTextDisplay;

    [Header("Elements")]
    [SerializeField] GameObject _selfAssessmentGameObject;
    [SerializeField] Button _answerRevealButton;
    [SerializeField] GameObject _overlayGameObject;
    [SerializeField] Button _exitButton;
    [SerializeField] RectTransform _fantiCloseupRectTransform;

    [Header("Events")]
    [SerializeField] GameEvent _playMenuLoadedEvent;
    [SerializeField] GameEvent _cardReviewedEvent;

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
            SetupPlaySession();
        }));
    }

    /**
     * @param score - The score of the self-assessment
     */
    public void SubmitSelfAssesment(int score)
    {
        _cardsToPlay[_playIndex].Review(score);
        _cardReviewedEvent.Raise();

        SaveDataManager.Instance.SaveData();

        _playIndex++;
        _score += score;

        if (_playIndex >= _cardsToPlay.Count) {
            FinishPlaySession();
            return;
        }

        LoadCard(_cardsToPlay[_playIndex]);
    }

    void SetupPlaySession()
    {
        ResetPlaySession();

        _cardsToPlay = new(GameStateManager.Instance.SelectedFanti.Model.ScheduledCards);

        LoadCard(_cardsToPlay[_playIndex]);

        StartCoroutine(Utilities.WaitForAFrameThen(() => { 
            _playMenuLoadedEvent.Raise();
        }));
    }

    void FinishPlaySession()
    {
        _overlayGameObject.SetActive(true);
        _exitButton.gameObject.SetActive(false);

        PlayFantiAnimation();

        int cardsPlayed = _playIndex;
        int streak = GameStateManager.Instance.SelectedFanti.Model.streak;
        int goldEarned = GameStateManager.Instance.CurrentPlayer.EarnGoldForCards(cardsPlayed + streak);
        streak = GameStateManager.Instance.SelectedFanti.IncrementStreak();
        int initLevel = GameStateManager.Instance.SelectedFanti.Model.Level;
        int expGained = GameStateManager.Instance.SelectedFanti.EarnExp();
    
        _cardedPlayedTextDisplay.UpdateText(cardsPlayed.ToString());
        _goldEarnedTextDisplay.UpdateText(goldEarned.ToString());
        _expGainedTextDisplay.UpdateText(expGained.ToString());
        _streakTextDisplay.UpdateText(streak.ToString());

        if(initLevel < GameStateManager.Instance.SelectedFanti.Model.Level) {
            _levelUpTextDisplay.UpdateText(GameStateManager.Instance.SelectedFanti.Model.name + " Leveled Up!");
            _levelUpTextDisplay.gameObject.SetActive(true);
        }

        SaveDataManager.Instance.SaveData();
    }

    void ResetPlaySession() 
    {
        _playIndex  = 0;
        _score      = 0;
        _overlayGameObject.SetActive(false);
        _exitButton.gameObject.SetActive(true);
        _levelUpTextDisplay.gameObject.SetActive(false);
        PlayFantiAnimation(true);
        HideAnswer();
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

    public void RevealAnswer()
    {
        _selfAssessmentGameObject.SetActive(true);
        _answerRevealButton.gameObject.SetActive(false);
    }

    void HideAnswer()
    {
        _selfAssessmentGameObject.SetActive(false);
        _answerRevealButton.gameObject.SetActive(true);
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
}
