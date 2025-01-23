using TMPro;
using UnityEngine;

public class FantiMenu : MonoBehaviour
{
    [SerializeField] TMP_Text _fantiNameText;

    public void LoadSelectedFanti()
    {
        StartCoroutine(Utilities.WaitForAFrameThen(() => {
            Debug.Log("LoadSelectedFanti: " + GameStateManager.Instance.SelectedFanti._name);
            UpdateText(GameStateManager.Instance.SelectedFanti);
        }));
    }

    void UpdateText(FantiModel fanti)
    {
        _fantiNameText.SetText(fanti._name);
    }
}
