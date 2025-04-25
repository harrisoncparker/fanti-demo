using UnityEngine;

public class MainMenuPanel : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] float _animationSeconds = .3f;

    float _closedPositionY;
    bool _isPanelOpen = false;
    RectTransform _rectTransform;

    void Start() 
    {
        _rectTransform = GetComponent<RectTransform>();
        _closedPositionY = _rectTransform.anchoredPosition.y;
    }

    public void OpenPanel()
    {
        LeanTween.moveY(_rectTransform, 0, _animationSeconds);
        _isPanelOpen = true;
    }

    public void ClosePanel()
    {
        LeanTween.moveY(_rectTransform, _closedPositionY, _animationSeconds);
        _isPanelOpen = false;
    }

    public void TogglePanelState()
    {
        if (_isPanelOpen) ClosePanel();
        else OpenPanel();
    }
}
