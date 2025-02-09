using UnityEngine;

public class Fanti : MonoBehaviour
{
    public FantiModel Model { get; set; }

    public void Initialize(FantiModel model)
    {
        Model = model;
        gameObject.name = model.name;
    }

    public void SelectFanti()
    {
        GameObject eventSource = GetComponent<GameEventListener>()._eventSource;

        if (!eventSource) {
            Debug.LogError("No event source set");
        }

        if (this != eventSource.GetComponent<Fanti>()) return;

        GameStateManager.Instance.SelectedFanti = this;
    }

    public int EarnExp(int exp)
    {
        int expToEarn = exp + (Model.streak * 10);
        Model.exp += expToEarn;
        return expToEarn;
    }

    public int IncrementStreak()
    {
        Model.streak++;
        return Model.streak;
    }
}