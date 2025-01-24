using System.Collections.Generic;

[System.Serializable]
public class DeckModel : Model
{
    public string title;
    public List<CardModel> cards;

    public DeckModel(string title, List<CardModel> cards = null)
    {
        this.title = title;
        this.cards = cards ?? new List<CardModel>();
    }
}