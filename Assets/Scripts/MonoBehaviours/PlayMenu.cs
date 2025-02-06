using UnityEngine;
using System.Collections.Generic;

public class PlayMenu : MonoBehaviour
{

    [Header("Text Displays")]
    [SerializeField] TextDisplay _questionTextDisplay;
    [SerializeField] TextDisplay _answerTextDisplay;

    [Header("Events")]
    [SerializeField] GameEvent _playMenuLoadedEvent;

    List<CardModel> _cardsToPlay;
    int _playIndex = 0;

    public void Load()
    {
        StartCoroutine(Utilities.WaitForAFrameThen(() => {
            LoadDeck();
        }));
    }

    void LoadDeck()
    {
        Debug.Log("the deck model --- " + GameStateManager.Instance.SelectedDeckModel.title);

        _cardsToPlay = SelectCards(GameStateManager.Instance.SelectedDeckModel);

        LoadCard(_cardsToPlay[_playIndex]);

        StartCoroutine(Utilities.WaitForAFrameThen(() => { 
            _playMenuLoadedEvent.Raise(gameObject);
        }));
    }

    List<CardModel> SelectCards(DeckModel deck) {
        // @todo only select the cards that need to be played
        return deck.cards;
    }

    void LoadCard(CardModel card)
    {
        if (card.doubleSided && Random.Range(0,1) > .5f) {
            _questionTextDisplay.UpdateText(card.front);
            _questionTextDisplay.UpdateText(card.back);
        } else {
            _questionTextDisplay.UpdateText(card.back);
            _answerTextDisplay.UpdateText(card.front);
        }
    }
}
