using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [Header("Display Elements")]
    [SerializeField] Image _itemImage;
    [SerializeField] TextDisplay _priceText;

    [Header("Data")]
    [SerializeField] ShopItemData _itemData;

    void Start()
    {
        if (_itemData != null)
        {
            LoadItemData(_itemData);
        }
    }

    public void LoadItemData(ShopItemData itemData)
    {
        _itemData = itemData;
        _itemImage.sprite = itemData.ShopSprite;
        _itemImage.SetNativeSize();
        _priceText.UpdateText(itemData.Price.ToString());
    }
}