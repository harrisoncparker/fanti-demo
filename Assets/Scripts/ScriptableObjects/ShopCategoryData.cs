using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Category", menuName = "Configuration/Shop Category")]
public class ShopCategoryData : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    public Sprite Icon => _icon;
} 