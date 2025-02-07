using System.Collections.Generic;

[System.Serializable]
public class PlayerModel : Model
{
    public string userName;
    public string email;
    public int gold;
    public List<FantiModel> fantis;
    public List<DeckModel> decks;

    public PlayerModel(
        string userName,
        string email,
        int gold = 0 ,
        List<FantiModel> fantis = null,
        List<DeckModel> decks = null
    )
    {
        this.userName = userName;
        this.email = email;
        this.gold = gold;
        this.fantis = fantis ?? new List<FantiModel>();
        this.decks = decks ?? new List<DeckModel>();
    }

    public static PlayerModel Fake()
    {
        // Fake Decks
        DeckModel fakeDeckDoubleSided = DeckModel.FakeDoubleSided();

        DeckModel fakeDeckOneSided = DeckModel.FakeOneSided();

        // Fake Fantis
        FantiModel fantalita = new("Fantalita");
        FantiModel hugo = new("Hugo", ColourName.Blue);

        fantalita.exp = 60;
        fantalita.deckIds = new List<string> {
            fakeDeckDoubleSided.id,
            fakeDeckOneSided.id
        };

        hugo.streak = 4;
        hugo.exp = 40;

        // Fake Player
        PlayerModel fakePlayer = new(
            "Harrison",
            "test@gmail.com",
            0,
            new() { 
                fantalita, 
                hugo
            },
            new() { 
                fakeDeckDoubleSided,
                fakeDeckOneSided
            }
        );

        return fakePlayer;
    }
}
