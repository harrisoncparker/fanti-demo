[System.Serializable]
public class CardModel : Model
{
    public string front;
    public string back;
    public bool doubleSided;

    public CardModel(string front, string back, bool doubleSided = false)
    {
        this.front = front;
        this.back = back;
        this.doubleSided = doubleSided;
    }
}
