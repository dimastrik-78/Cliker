using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsTypes", menuName = "New item", order = 51)]
public class ItemSO : ScriptableObject
{
    public enum ItemsTypes { item };
    [SerializeField] private ItemsTypes _itemsTypes;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _itemName;
    [SerializeField] private string _itemDescription;
    [SerializeField] private int _itemCost;
    [SerializeField] private int _itemEffect;
    [SerializeField] private int _itemTimeAction;
    public ItemsTypes ItemType { get => _itemsTypes; }
    public Sprite Icon { get => _icon; }
    public string ItemName { get => _itemName; }
    public string ItemDescription { get => _itemDescription; }
    public int ItemCost { get => _itemCost; }
    public int ItemEffect { get => _itemEffect; }
    public int ItemTimeAction { get => _itemTimeAction; }
}