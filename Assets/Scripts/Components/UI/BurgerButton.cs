using UnityEngine;
using UnityEngine.UI;

public class BurgerButton : MonoBehaviour
{
    
    [Header("Icons")]
    [SerializeField] Sprite _closedIcon;
    [SerializeField] Sprite _openIcon;

    Image _image;
    
    void Start()
    {
        _image = GetComponent<Image>();
    }

    public void SetIconClosed() 
    {
        _image.sprite = _closedIcon;
    }

    public void SetIconOpen() 
    {
        _image.sprite = _openIcon;
    }

    public void ToggleIcon()
    {
        if (_image.sprite == _closedIcon)
            SetIconOpen();
        else
            SetIconClosed();      
    }
}
