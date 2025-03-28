using UnityEngine;
using UnityEngine.UI;

public class FurnitureShopMenu : MonoBehaviour
{   
    [Header("Text Displays")]
    [SerializeField] TextDisplay _textDisplayName;

    [Header("Elements")]
    [SerializeField] Button _buttonName;

    [Header("Events")]
    [SerializeField] GameEvent _eventName;

    public void Load()
    {
        StartCoroutine(Utilities.WaitForAFrameThen(() => {
            Debug.Log("Loaded Furinture Shop Menu");
        }));
    }
}
