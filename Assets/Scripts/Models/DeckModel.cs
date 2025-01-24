using System;
using System.Collections.Generic;

[Serializable]
public class DeckModel : Model
{
    public string id;
    public string title;
    public List<CardModel> cards;

    public DeckModel(string title, List<CardModel> cards = null, string id = null)
    {
        this.id = id ?? Guid.NewGuid().ToString();
        this.title = title;
        this.cards = cards ?? new List<CardModel>();
    }

    public static DeckModel Fake()
    {
        return new(
            "Spanish Revision",
            new List<CardModel> {
                new("tree", "árbol", true),
                new("game", "juego", true),
                new("computer", "computadora", true),
                new("river", "río", true),
            }
        );
    }
}