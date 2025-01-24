using TMPro;
using UnityEngine;

public class FantiMenu : MonoBehaviour
{
    [SerializeField] TMP_Text _fantiNameText;

    public void LoadSelectedFanti()
    {
        StartCoroutine(Utilities.WaitForAFrameThen(() => {
            UpdateText(GameStateManager.Instance.SelectedFanti);
        }));
    }

    void UpdateText(Fanti fanti)
    {
        _fantiNameText.SetText(fanti.Model.name);
    }
}
