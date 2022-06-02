using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsTypes", menuName = "New item", order = 51)]
public class BuffItemSO : ScriptableObject
{
    public enum ItemsTypes { item };
    [SerializeField] private ItemsTypes _itemsTypes;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _itemName;
    [SerializeField] private string _itemDescriptionBuff;
    [SerializeField] private int _itemCost;
    public ItemsTypes ItemType { get => _itemsTypes; }
    public Sprite Icon { get => _icon; }
    public string ItemName { get => _itemName; }
    public string ItemDescriptionBuff { get => _itemDescriptionBuff; }
    public int ItemCost { get => _itemCost; }
}