using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture", menuName = "Items/Furniture")]
public class FurnitureData : ShopItemData
{
    [Header("Furniture Properties")]
    [SerializeField] private int _sortingLayer;
    [SerializeField] private bool _useGravity;
    public int SortingLayer => _sortingLayer;
    public bool UsesGravity => _useGravity;
}