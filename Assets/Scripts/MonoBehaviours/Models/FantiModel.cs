using System;
using System.Collections.Generic;
using UnityEngine;


public class Model : MonoBehaviour 
{
    int _id;
}

public class CardModel : Model
{
    string _front;
    string _back;
    bool _doubleSided;
}

public class DeckModel : Model
{
    string _title;
    List<CardModel> _cards;
}

public class FantiModel : Model
{
    [Header("base")]
    public string _name;
    [SerializeField] string _colour;
    [SerializeField] List<DeckModel> _decks;
    [SerializeField] int _level;
    [SerializeField] int _exp;
    [SerializeField] int _streak;

    [Header("timestamps")]
    [SerializeField] DateTime _lastPlaySession;
    [SerializeField] DateTime   _lastWearableUpdate;

    [Header("wearables")]
    [SerializeField] int _headWearable;
    [SerializeField] int _faceWearable;

    public void SelectFanti()
    {
        GameObject eventSource = GetComponent<GameEventListener>()._eventSource;

        if (!eventSource) {
            Debug.LogError("No event source set");
        }

        if (this != eventSource.GetComponent<FantiModel>()) return;

        GameStateManager.Instance.SelectedFanti = this;
    }
}