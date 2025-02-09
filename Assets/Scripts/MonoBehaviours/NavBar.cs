using UnityEngine;

public class NavBar : MonoBehaviour
{
    [SerializeField] TextDisplay _goldDisplay;
    [SerializeField] TextDisplay _sheduledCardsDisplay;

    public void UpdateTextDisplays()
    {
        PlayerModel playerModel = GameStateManager.Instance.CurrentPlayer.Model;
        _goldDisplay.UpdateText(playerModel.gold.ToString());
        _sheduledCardsDisplay.UpdateText(playerModel.ScheduledCards.Count.ToString());
    }
}
