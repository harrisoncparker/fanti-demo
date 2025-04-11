using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [Header("Display Elements")]
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextDisplay _priceText;
    [SerializeField] private TextDisplay _quantityText;

    [Header("References")]
    [SerializeField] private ShopItemData _itemData;

    private void OnEnable()
    {
        EventManager.Instance.Save.OnSaveDataLoaded += UpdateDisplay;
        EventManager.Instance.Player.OnInventoryUpdated += UpdateDisplay;
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.Save.OnSaveDataLoaded -= UpdateDisplay;
            EventManager.Instance.Player.OnInventoryUpdated -= UpdateDisplay;
        }
    }

    public void LoadItemData(ShopItemData itemData)
    {
        _itemData = itemData;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (_itemData == null) return;

        Player currentPlayer = GameStateManager.Instance.CurrentPlayer;

        _itemImage.sprite = _itemData.ShopSprite;
        _itemImage.SetNativeSize();
        _priceText.UpdateText(_itemData.Price.ToString());
        _quantityText.UpdateText(currentPlayer.GetItemQuantity(_itemData).ToString());
    }

    public void OnPurchaseButtonClicked()
    {
        Player currentPlayer = GameStateManager.Instance.CurrentPlayer;

        if (_itemData == null || currentPlayer == null) return;

        if (!currentPlayer.CanAfford(_itemData.Price))
        {
            // TODO: Show insufficient funds feedback
            return;
        }

        // Purchase the item
        currentPlayer.AddToInventory(_itemData);
        UpdateDisplay();
    }
}