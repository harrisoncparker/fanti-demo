using UnityEngine;
using UnityEngine.UI;

public class UIBurgerButton : MonoBehaviour
{
    
    [SerializeField] Sprite _closedIcon;
    [SerializeField] Sprite _openIcon;

    Image _image;
    
    void Start()
    {
        _image = GetComponent<Image>();
    }

    public void ToggleIcon()
    {
        if (_image.sprite == _closedIcon)
            _image.sprite = _openIcon;
        else
            _image.sprite = _closedIcon;
    }
}
