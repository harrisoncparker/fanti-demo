using System.Collections.Generic;

public enum FantiMood
{
    Happy,
    Neutral,
    Sad
}

[System.Serializable]
public class FantiModel : Model
{
    // General
    public string name;
    public ColourName colour;
    public List<string> deckIds;
    public int exp;
    public int streak;

    public FantiMood Mood
    {
        get
        {
            // TODO: Calculate mood based on various factors like:
            // - Time since last play session
            // - Study streak
            // - Experience gain rate
            // - Deck completion rates
            // - Interaction frequency
            // For now, default to neutral
            return FantiMood.Neutral;
        }
    }

    public int Level
    {
        get => GetLevelFromExp(exp);
    }

    // Wearable IDs
    public int headWearableID;
    public int faceWearableID;

    // Serialized Timestamps
    public string lastPlaySession;
    public System.DateTime LastPlaySessionDateTime
    {
        get => DeserializeDateTime(lastPlaySession);
        set => lastPlaySession = value.ToString("o");
    }

    public string lastWearableUpdate;
    public System.DateTime LastWearableUpdateDateTime
    {
        get => DeserializeDateTime(lastWearableUpdate);
        set => lastWearableUpdate = value.ToString("o");
    }

    public List<DeckModel> Decks
    {
        get {
            List<DeckModel> decks = new();

            deckIds.ForEach(deckID => {
                GameStateManager.Instance.CurrentPlayer.Model.decks.ForEach(deck =>
                {
                    if (deck.id == deckID) decks.Add(deck);
                });
            });

            return decks;
        }
    }

    public List<CardModel> ScheduledCards
    {
        get { 
            List<CardModel> combinedCards = new();

            Decks.ForEach(deck => {
                combinedCards.AddRange(deck.ScheduledCards);
            });

            return DeckModel.ShuffleCards(combinedCards);
        }
    }

    public FantiModel(
        string name,
        ColourName colour = ColourName.Pink,
        List<string> deckIds = null,
        int exp = 0,
        int streak = 0,
        int headWearableID = 0,
        int faceWearableID = 0,
        System.DateTime? lastPlaySessionDateTime = null,
        System.DateTime? lastWearableUpdateDateTime = null
    )
    {
        this.name = name;
        this.colour = colour;
        this.deckIds = deckIds ?? new List<string>();
        this.exp = exp;
        this.streak = streak;
        this.headWearableID = headWearableID;
        this.faceWearableID = faceWearableID;

        LastPlaySessionDateTime = lastPlaySessionDateTime ?? System.DateTime.Now;
        LastWearableUpdateDateTime = lastWearableUpdateDateTime ?? System.DateTime.Now;
    }

    int GetLevelFromExp(int experience)
    {
        // if (baseExp = 50) then curve looks something like this 
        // lvl1 = 0, lvl2 = 50, lvl3 = 200, lvl4 = 450, lvl5 = 800
        int baseExp = 50; 
        int level = 1;

        while (experience >= baseExp * level * level)
        {
            level++;
        }

        return level;
    }
}