using UnityEngine;

public class ShopItemData : ScriptableObject
{
    [Header("Shop Properties")]
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private ShopCategoryData _category;
    
    // Convert filename to kebab-case for ID
    public string Id => StringUtilities.ToKebabCase(name);
    public int Price => _price;
    public Sprite ShopSprite => _sprite;
    public ShopCategoryData Category => _category;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // We don't need to validate characters anymore since ToKebabCase handles it
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogWarning($"ShopItem has no name. Please provide a name for the asset.");
        }
    }
#endif
}