using System.Collections.Generic;

[System.Serializable]
public class FantiModel : Model
{
    // General
    public string name;
    public ColourName colour;
    public List<string> deckIds;
    public int exp;
    public int streak;

    // Wearable IDs
    public int headWearableID;
    public int faceWearableID;

    // Timestamps
    public string lastPlaySessionSerialized;
    public System.DateTime LastPlaySession
    {
        get => DeserializeDateTime(lastPlaySessionSerialized);
        set => lastPlaySessionSerialized = value.ToString("o");
    }

    public string lastWearableUpdateSerialized;
    public System.DateTime LastWearableUpdate
    {
        get => DeserializeDateTime(lastWearableUpdateSerialized);
        set => lastWearableUpdateSerialized = value.ToString("o");
    }

    public FantiModel(
        string name,
        ColourName colour = ColourName.Pink,
        List<string> deckIds = null,
        int exp = 0,
        int streak = 0,
        int headWearableID = 0,
        int faceWearableID = 0,
        System.DateTime? lastPlaySession = null,
        System.DateTime? lastWearableUpdate = null
    )
    {
        this.name = name;
        this.colour = colour;
        this.deckIds = deckIds ?? new List<string>();
        this.exp = exp;
        this.streak = streak;
        this.headWearableID = headWearableID;
        this.faceWearableID = faceWearableID;

        LastPlaySession = lastPlaySession ?? System.DateTime.Now;
        LastWearableUpdate = lastWearableUpdate ?? System.DateTime.Now;
    }

    public List<DeckModel> GetDecks() {
        List<DeckModel> decks = new();

        deckIds.ForEach(deckID => {
            SaveDataManager.Instance.CurrentPlayerModel.decks.ForEach(deck => {

                if (deck.id == deckID) {
                    decks.Add(deck);
                }
            });
        });

        return decks;
    }

    public int Level()
    {
        return GetLevelFromExp(exp);
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