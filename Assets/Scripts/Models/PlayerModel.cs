using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerModel : Model
{
    public string userName;
    public string email;
    public int gold;
    public List<FantiModel> fantis;
    public List<DeckModel> decks;
    public InventoryModel inventory;

    public List<CardModel> ScheduledCards
    {
        get {
            List<CardModel> scheduledCards = new();
            foreach (FantiModel fanti in fantis)
            {
                scheduledCards.AddRange(fanti.ScheduledCards);
            }
            return scheduledCards;
        }
    }

    public PlayerModel(
        string userName,
        string email,
        int gold = 120,
        List<FantiModel> fantis = null,
        List<DeckModel> decks = null,
        InventoryModel inventory = null
    )
    {
        this.userName = userName;
        this.email = email;
        this.gold = gold;
        this.fantis = fantis ?? new List<FantiModel>();
        this.decks = decks ?? new List<DeckModel>();
        this.inventory = inventory ?? new InventoryModel();
    }

    public static PlayerModel Fake()
    {
        // Fake Decks
        DeckModel fakeDeckDoubleSided = DeckModel.FakeDoubleSided();
        DeckModel fakeDeckOneSided = DeckModel.FakeOneSided();
        DeckModel fakeDeckMixedSided = DeckModel.FakeMixedSided();

        // Fake Fantis
        FantiModel fantalita = new("Fantalita");
        FantiModel hugo = new("Hugo", ColourName.Blue);

        fantalita.streak = 3;
        fantalita.exp = 315;
        fantalita.deckIds = new List<string> {
            fakeDeckDoubleSided.id,
            fakeDeckOneSided.id
        };

        hugo.deckIds = new List<string> {
            fakeDeckMixedSided.id
        };

        // Fake Inventory
        var inventory = new InventoryModel();
        
        // Try to find and add furniture items if they exist
        var furniture = Resources.FindObjectsOfTypeAll<FurnitureData>();
        if (furniture != null && furniture.Length > 0)
        {
            foreach (var item in furniture[..2])  // Take first two items using range
            {
                inventory.AddItem(item.Id, 1);
            }
        }
        
        // Fake Player
        PlayerModel fakePlayer = new(
            "Harrison",
            "test@gmail.com",
            0,
            new() { fantalita, hugo },
            new() { 
                fakeDeckDoubleSided,
                fakeDeckOneSided,
                fakeDeckMixedSided
            },
            inventory
        );

        return fakePlayer;
    }
}
