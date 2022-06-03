using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public ItemPanel ItemInShop;
    public GameObject PanelInfoItemObj;

    [HideInInspector] public ItemSO[] allItem;
    [HideInInspector] public int itemNumber;

    private Object[] _allItems;
    private void Start()
    {
        _allItems = Resources.LoadAll("items", typeof(ItemSO));
        allItem = new ItemSO[_allItems.Length];
        for (int i = 0; i < _allItems.Length; i++)
        {
            allItem[i] = (ItemSO)_allItems[i];
        }
    }
    private void OnMouseUp()
    {
        PanelInfoItemObj.SetActive(true);
        PanelInfoItemObj.transform.GetChild(0).GetComponent<Text>().text = $"Name: {allItem[itemNumber].ItemName}";
        PanelInfoItemObj.transform.GetChild(1).GetComponent<Text>().text = $"Cost ticket: {allItem[itemNumber].ItemCost}";
        PanelInfoItemObj.transform.GetChild(2).GetComponent<Text>().text = $"Effect: {allItem[itemNumber].ItemDescription}";
        ItemInShop.selectedItem = itemNumber;
    }
}