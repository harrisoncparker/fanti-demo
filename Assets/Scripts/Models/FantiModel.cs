using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FantiModel : Model
{
    // General
    public string name;
    public ColourName colour;
    public List<DeckModel> decks;
    public int level;
    public int exp;
    public int streak;

    // Timestamps
    public DateTime lastPlaySession;
    public DateTime  lastWearableUpdate;

    // Wearables
    public int headWearableID;
    public int faceWearableID;

    public FantiModel(
        string name,
        ColourName colour = ColourName.Pink,
        List<DeckModel> decks = null,
        int level = 1,
        int exp = 0,
        int streak = 0,
        DateTime? lastPlaySession = null,
        DateTime? lastWearableUpdate = null,
        int headWearableID = 0,
        int faceWearableID = 0
    )
    {
        this.name = name;
        this.colour = colour;
        this.decks = decks ?? new List<DeckModel>();
        this.level = level;
        this.exp = exp;
        this.streak = streak;
        this.lastPlaySession = lastPlaySession ?? DateTime.Now;
        this.lastWearableUpdate = lastWearableUpdate ?? DateTime.Now;
        this.headWearableID = headWearableID;
        this.faceWearableID = faceWearableID;
    }

    public new string ToJson(bool prettyPrint = false)
    {

        
        return JsonUtility.ToJson(this, prettyPrint);
    }
}