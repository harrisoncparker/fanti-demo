using UnityEngine;

public class VisibilityToggler : MonoBehaviour
{
    [SerializeField] GameObject _objectToToggle;
    [SerializeField] bool _initialVisibilty = false;

    void Start()
    {
        if (IsMissingObject()) return;
        _objectToToggle.SetActive(_initialVisibilty);
    }

    bool IsMissingObject()
    {
        if(_objectToToggle == null) {
            Debug.LogWarning("_objectToToggle not set");
            return true;
        }
        return false;
    }

    public void Toggle()
    {
        if (IsMissingObject()) return;
        _objectToToggle.SetActive(!_objectToToggle.activeInHierarchy);
    }

    public void Open()
    {
        if (IsMissingObject()) return;
        _objectToToggle.SetActive(true);
    }

    public void Close()
    {
        if (IsMissingObject()) return;
        _objectToToggle.SetActive(false);
    }
}
