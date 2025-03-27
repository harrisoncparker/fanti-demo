using UnityEngine;

public class Fanti : MonoBehaviour
{
    public FantiModel Model { get; private set; }

    private void Awake()
    {
        if (Model == null)
        {
            // Create a default model if none exists
            Model = new FantiModel(
                name: gameObject.name,
                colour: ColourName.Pink
            );
            Debug.Log($"[Fanti] Created default model for {gameObject.name}");
        }
    }

    public void Initialize(FantiModel model)
    {
        Model = model;
        gameObject.name = model.name;
    }

    public void SelectFanti()
    {
        // @TODO this is bad practice as is requires the called function to know which event called it
        // Eventually we need to find a better solution for this
        GameObject eventSource = GameEventListener.FindEventSourceInListeners(
            "HomeFantiClicked", 
            GetComponents<GameEventListener>()
        );

        if (this != eventSource.GetComponent<Fanti>()) return;

        GameStateManager.Instance.SelectedFanti = this;
    }

    public int EarnExp()
    {
        int expToEarn = 50 + (Model.streak * 5);
        Model.exp += expToEarn;
        return expToEarn;
    }

    public int IncrementStreak()
    {
        Model.streak++;
        return Model.streak;
    }
}