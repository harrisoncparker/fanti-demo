using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerModel Model { get; set; }

    [Header("Events")]
    [SerializeField] GameEvent _goldUpdatedEvent;
    [SerializeField] GameEvent _inventoryUpdatedEvent;

    private void ValidateModel()
    {
        if (Model == null)
        {
            Debug.LogError("Player Model is null. Ensure it's initialized before performing operations.");
            return;
        }
        if (Model.inventory == null)
        {
            Debug.LogWarning("Player inventory is null. Initializing new inventory.");
            Model.inventory = new InventoryModel();
        }
    }

    public int EarnGoldForCards(int cardsPlayed)
    {
        ValidateModel();
        
        int goldToEarn = cardsPlayed * 10;
        int randomBonus = Random.Range(0, goldToEarn);
        Model.gold += goldToEarn + randomBonus;
        _goldUpdatedEvent.Raise();
        return goldToEarn;
    }

    public bool CanAfford(int cost)
    {
        ValidateModel();
        
        if (cost < 0)
        {
            Debug.LogError($"Invalid cost: {cost}. Cost cannot be negative.");
            return false;
        }
        return Model.gold >= cost;
    }

    public bool PurchaseItem(ShopItemData item)
    {
        ValidateModel();

        if (item == null)
        {
            Debug.LogError("Cannot purchase null item.");
            return false;
        }

        if (!CanAfford(item.Price))
        {
            Debug.LogWarning($"Cannot afford {item.name} for {item.Price} gold.");
            return false;
        }

        Model.gold -= item.Price;
        AddToInventory(item);
        _goldUpdatedEvent.Raise();
        return true;
    }

    public void AddToInventory(ShopItemData item, int amount = 1)
    {
        ValidateModel();
        
        if (item == null)
        {
            Debug.LogError("Cannot add null item to inventory.");
            return;
        }
        
        if (amount <= 0)
        {
            Debug.LogError($"Cannot add {amount} items. Amount must be positive.");
            return;
        }

        Model.inventory.AddItem(item.Id, amount);
        _inventoryUpdatedEvent.Raise();
    }

    public bool RemoveFromInventory(ShopItemData item, int amount = 1)
    {
        ValidateModel();
        
        if (item == null)
        {
            Debug.LogError("Cannot remove null item from inventory.");
            return false;
        }
        
        if (amount <= 0)
        {
            Debug.LogError($"Cannot remove {amount} items. Amount must be positive.");
            return false;
        }

        int currentAmount = GetItemQuantity(item);
        if (currentAmount < amount)
        {
            Debug.LogWarning($"Cannot remove {amount}x {item.name}, only have {currentAmount}.");
            return false;
        }

        bool removed = Model.inventory.RemoveItem(item.Id, amount);
        if (removed)
        {
            _inventoryUpdatedEvent.Raise();
        }
        return removed;
    }

    public int GetItemQuantity(ShopItemData item)
    {
        ValidateModel();
        return Model.inventory.GetQuantity(item.Id);
    }

    public bool HasItem(ShopItemData item)
    {
        return GetItemQuantity(item) > 0;
    }
}