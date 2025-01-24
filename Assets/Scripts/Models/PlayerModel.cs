using System.Collections.Generic;

[System.Serializable]
public class PlayerModel : Model
{
    public string userName;
    public string email;
    public List<FantiModel> fantis;

    public PlayerModel(
        string userName,
        string email,
        List<FantiModel> fantis = null
    )
    {
        this.userName = userName;
        this.email = email;
        this.fantis = fantis ?? new List<FantiModel>();
    }

    public static PlayerModel Fake()
    {
        FantiModel fantalita = new("Fantalita");
        FantiModel hugo = new("Hugo", ColourName.Blue);

        fantalita.level = 2;
        fantalita.exp = 120;

        hugo.streak = 4;
        hugo.exp = 40;

        PlayerModel fakePlayer = new(
            "Harrison",
            "test@gmail.com",
            new List<FantiModel> { 
                fantalita, 
                hugo
            }
        );

        return fakePlayer;
    }
}
