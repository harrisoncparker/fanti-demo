using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerModel Model { get; set; }

    [SerializeField] GameEvent _goldUpdatedEvent;

    public int EarnGoldForCards(int cardsPlayed)
    {
        int goldToEarn = cardsPlayed * 10;
        Model.gold += goldToEarn;
        _goldUpdatedEvent.Raise();
        return goldToEarn;
    }
}