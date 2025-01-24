using System.Collections.Generic;

[System.Serializable]
public class PlayerModel : Model
{
    public string userName;
    public string email;
    public List<FantiModel> fantis;
    public List<DeckModel> decks;

    public PlayerModel(
        string userName,
        string email,
        List<FantiModel> fantis = null,
        List<DeckModel> decks = null
    )
    {
        this.userName = userName;
        this.email = email;
        this.fantis = fantis ?? new List<FantiModel>();
        this.decks = decks ?? new List<DeckModel>();
    }

    public static PlayerModel Fake()
    {
        // Fake Decks
        DeckModel fakeDeck = DeckModel.Fake();

        // Fake Fantis
        FantiModel fantalita = new("Fantalita");
        FantiModel hugo = new("Hugo", ColourName.Blue);

        fantalita.level = 2;
        fantalita.exp = 120;
        fantalita.deckIds = new List<string> {
            fakeDeck.id
        };

        hugo.streak = 4;
        hugo.exp = 40;

        // Fake Player
        PlayerModel fakePlayer = new(
            "Harrison",
            "test@gmail.com",
            new() { 
                fantalita, 
                hugo
            },
            new() { 
                fakeDeck 
            }
        );

        return fakePlayer;
    }
}
