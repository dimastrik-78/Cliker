using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemPanel : MonoBehaviour
{
    public GameObject PanelInfoItemObj, ActiveItem;
    public GameObject[] ArrayImageItem;
    public ItemInfo[] ItemInfo;
    public TimerActiveBuff TimerActiveBuff;

    private Object[] _allItems;

    public ShopUI Shop;

    [HideInInspector] public ItemSO[] itemType;

    SaveDataBase DataBase;
    [SerializeField] public int Ticket, CountAcriveBuff, selectedItem;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    private void Start()
    {
        DataBase = new SaveDataBase();
        LoadDataFromJSON();

        _allItems = Resources.LoadAll("Items", typeof(ItemSO));
        itemType = new ItemSO[_allItems.Length];
        for (int i = 0; i < _allItems.Length; i++)
        {
            if (i != DataBase.ArrayActivatedItems[i])
            {
                ArrayImageItem[i].SetActive(true);
                ItemInfo[i].itemNumber = i;
                itemType[i] = (ItemSO)_allItems[i];
                ItemInfo[i].GetComponent<Image>().sprite = itemType[i].Icon;
            }
        }
    }
    public void BuyBuff()
    {
        LoadDataFromJSON();
        if (DataBase.HowManyActiveBuffs != 1 && Shop.Ticket >= itemType[selectedItem].ItemCost)
        {
            DataBase.DataTimeActiveBuff = itemType[selectedItem].ItemTimeAction;
            Shop.Ticket -= itemType[selectedItem].ItemCost;
            ArrayImageItem[selectedItem].SetActive(false);
            ActiveItem.SetActive(true);
            ActiveItem.transform.GetChild(0).GetComponent<Image>().sprite = itemType[selectedItem].Icon;
            DataBase.HowManyActiveBuffs++;
            PanelInfoItemObj.SetActive(false);
            Shop.UpdateText();
            SaveDataToJSON();

            TimerActiveBuff.Start();
        }
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);
    }

    public void SaveDataToJSON()
    {
        DataBase.DataItemIsActivated = selectedItem;
        DataBase.ArrayActivatedItems[selectedItem] = selectedItem;
        
        string DataStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, DataStr);
    }
}
