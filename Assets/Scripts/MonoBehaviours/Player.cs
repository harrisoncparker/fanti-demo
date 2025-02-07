using UnityEngine;

class Player : MonoBehaviour
{
    public PlayerModel Model { get; set; }

    [SerializeField] GameEvent _goldUpdatedEvent;

    public void Initialize(PlayerModel model)
    {
        Model = model;
        gameObject.name = model.userName;
    }

    public int EarnGoldForCards(int cardsPlayed)
    {
        int goldToEarn = cardsPlayed * 10;
        Model.gold += goldToEarn;
        _goldUpdatedEvent.Raise(gameObject);
        return goldToEarn;
    }
}