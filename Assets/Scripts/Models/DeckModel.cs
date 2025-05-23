using System;
using System.Collections.Generic;

[Serializable]
public class DeckModel : Model
{
    public string id;
    public string title;
    public List<CardModel> cards;

    public List<CardModel> ScheduledCards
    {
        get => cards.FindAll(card => card.NextReviewDateTime <= DateTime.Now);
    }

    public DeckModel(string title, List<CardModel> cards = null, string id = null)
    {
        this.id = id ?? Guid.NewGuid().ToString();
        this.title = title;
        this.cards = cards ?? new List<CardModel>();
    }

    public static List<CardModel> ShuffleCards(List<CardModel> cards) {
		int count = cards.Count;
		int last = count - 1;

        List<CardModel> shuffledCards = new(cards);

		for (var i = 0; i < last; ++i) {
			int r = UnityEngine.Random.Range(i, count);
            (shuffledCards[r], shuffledCards[i]) = (shuffledCards[i], shuffledCards[r]);
        }

        return shuffledCards;
	}

    public static DeckModel FakeDoubleSided()
    {
        return new(
            "Spanish Revision",
            new List<CardModel> {
                new("tree", "árbol (spanish)", true),
                new("game", "juego (spanish)", true),
                new("computer", "computadora (spanish)", true),
                new("river", "río (spanish)", true),
            }
        );
    }

    public static DeckModel FakeOneSided()
    {
        return new(
            "Maths Revision",
            new List<CardModel> {
                new("1 + 1", "2", false),
                new("7 x 3", "21", false),
                new("Pythagorean theorem", "a2 + b2 = c2", false),
            }
        );
    }

    public static DeckModel FakeMixedSided()
    {
        return new(
            "Animal Quiz",
            new List<CardModel> {
                new("Prey: Rabbit", "Preditor: Fox", true),
                new("Prey: Zebra", "Preditor: Lion", true),
                new("Largest Animal", "Blue Whale", false),
                new("Tallest Animal", "Giraffe", true),
            }
        );
    }
}