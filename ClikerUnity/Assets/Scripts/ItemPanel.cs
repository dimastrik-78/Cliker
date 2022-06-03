using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemPanel : MonoBehaviour
{
    public GameObject PanelInfoItemObj;
    public GameObject[] ArrayImageItem, ArrayActiveBuff;
    public ItemInfo[] ItemInfo;
    public TimerActiveBuff[] TimerActiveBuff;

    private Object[] _allItems;

    [HideInInspector] public ItemSO[] itemType;
    [HideInInspector] public int selectedItem;

    SaveDataBase DataBase;
    [SerializeField] public int Ticket, CountAcriveBuff, WhichItemIsActivated;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    private void Start()
    {
        DataBase = new SaveDataBase();
        _allItems = Resources.LoadAll("Items", typeof(ItemSO));
        itemType = new ItemSO[_allItems.Length];
        for (int i = 0; i < _allItems.Length; i++)
        {
            ArrayImageItem[i].SetActive(true);
            ItemInfo[i].itemNumber = i;
            itemType[i] = (ItemSO)_allItems[i];
            ItemInfo[i].GetComponent<Image>().sprite = itemType[i].Icon;
        }
    }
    public void BuyBuff()
    {
        Debug.Log(ArrayActiveBuff.Length);
        for (int i = 0; i < ArrayActiveBuff.Length && ArrayActiveBuff.Length != CountAcriveBuff; i++)
        {
            if (ArrayActiveBuff[i].activeInHierarchy == false)
            {
                Ticket -= itemType[selectedItem].ItemCost;
                ArrayImageItem[selectedItem].SetActive(false);
                ArrayActiveBuff[i].SetActive(true);
                TimerActiveBuff[i].StartAcrive();
                ArrayActiveBuff[i].transform.GetChild(0).GetComponent<Image>().sprite = itemType[selectedItem].Icon;
                CountAcriveBuff++;
                PanelInfoItemObj.SetActive(false);
                SaveNewDataToJSON();
                break;
            }
        }
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        //Ticket = DataBase.SaveDataTicket;
        //GettingTickets = DataBase.SaveDataGettingTicket;

        //AudioMixer.SetFloat("MainMusic", audioSetting.MusicVolum);
        //AudioMixer.SetFloat("VFX", audioSetting.FVXVolum);

        //SliderMusic.value = audioSetting.MusicVolum;
        //SliderVFX.value = audioSetting.FVXVolum;

        //ToggleMusic.isOn = audioSetting.ToggleMusic;
        //ToggleVFX.isOn = audioSetting.ToggleVFX;
    }

    public void SaveNewDataToJSON()
    {
        DataBase.DataItemIsActivated = selectedItem;
        DataBase.DataItemIsActivated = WhichItemIsActivated;
        DataBase.HowManyActiveBuffs = CountAcriveBuff;
        DataBase.DataTicket = Ticket;

        string DataStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, DataStr);
    }
}
