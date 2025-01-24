using System;
using Unity.VisualScripting;
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
}