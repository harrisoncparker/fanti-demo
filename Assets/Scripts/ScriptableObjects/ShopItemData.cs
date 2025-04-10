using UnityEngine;

public class ShopItemData : ScriptableObject
{
    [Header("Shop Properties")]
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private ShopCategoryData _category;
    
    public int Price => _price;
    public Sprite ShopSprite => _sprite;
    public ShopCategoryData Category => _category;
}