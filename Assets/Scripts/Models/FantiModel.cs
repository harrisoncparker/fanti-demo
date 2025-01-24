using System;
using System.Collections.Generic;

[Serializable]
public class FantiModel : Model
{
    // General
    public string name;
    public ColourName colour;
    public List<string> deckIds;
    public int level;
    public int exp;
    public int streak;

    // Wearable IDs
    public int headWearableID;
    public int faceWearableID;

    // Timestamps
    public string lastPlaySessionSerialized;
    public DateTime LastPlaySession
    {
        get => DeserializeDateTime(lastPlaySessionSerialized);
        set => lastPlaySessionSerialized = value.ToString("o");
    }

    public string lastWearableUpdateSerialized;
    public DateTime LastWearableUpdate
    {
        get => DeserializeDateTime(lastWearableUpdateSerialized);
        set => lastWearableUpdateSerialized = value.ToString("o");
    }

    public FantiModel(
        string name,
        ColourName colour = ColourName.Pink,
        List<string> deckIds = null,
        int level = 1,
        int exp = 0,
        int streak = 0,
        int headWearableID = 0,
        int faceWearableID = 0,
        DateTime? lastPlaySession = null,
        DateTime? lastWearableUpdate = null
    )
    {
        this.name = name;
        this.colour = colour;
        this.deckIds = deckIds ?? new List<string>();
        this.level = level;
        this.exp = exp;
        this.streak = streak;
        this.headWearableID = headWearableID;
        this.faceWearableID = faceWearableID;

        LastPlaySession = lastPlaySession ?? DateTime.Now;
        LastWearableUpdate = lastWearableUpdate ?? DateTime.Now;
    }
}