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
}
