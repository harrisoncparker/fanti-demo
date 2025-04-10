using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerModel Model { get; set; }

    [SerializeField] GameEvent _goldUpdatedEvent;

    public int EarnGoldForCards(int cardsPlayed)
    {
        int goldToEarn = cardsPlayed * 10;
        int randomBonus = Random.Range(0, goldToEarn);
        Model.gold += goldToEarn + randomBonus;
        _goldUpdatedEvent.Raise();
        return goldToEarn;
    }
}