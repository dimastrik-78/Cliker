using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TimerActiveBuff : MonoBehaviour
{
    public ItemPanel ItemInShop;
    public Text TextTimer;

    [HideInInspector] public ItemSO[] item;

    private Object[] _allItems;
    private float _currentTimer, _timeToStart = 1;

    SaveDataBase DataBase;
    [SerializeField] int TimeActiveBuff;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    private void Start()
    {
        DataBase = new SaveDataBase();
        LoadDataFromJSON();
    }
    public void StartAcrive()
    {
        _allItems = Resources.LoadAll("items", typeof(ItemSO));
        item = new ItemSO[_allItems.Length];
        for (int i = 0; i < _allItems.Length; i++)
        {
            item[i] = (ItemSO)_allItems[i];
        }
        TimeActiveBuff = item[ItemInShop.selectedItem].ItemTimeAction;
        TextTimer.text = $"{TimeActiveBuff}";
    }
    void Update()
    {
        _currentTimer -= Time.deltaTime;
        if (_currentTimer < 0)
        {
            _currentTimer = _timeToStart;
            TimeActiveBuff--;
            TextTimer.text = $"{TimeActiveBuff}";
            SaveDataToJSON();
        }
        if (TimeActiveBuff < 0)
        {
            gameObject.SetActive(false);
            ItemInShop.CountAcriveBuff--;
            SaveDataToJSON();
        }
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        TimeActiveBuff = DataBase.DataTimeActiveBuff;
    }

    public void SaveDataToJSON()
    {
        DataBase.DataTimeActiveBuff = TimeActiveBuff;
        DataBase.HowManyActiveBuffs = ItemInShop.CountAcriveBuff;

        string volumeStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, volumeStr);
    }
}
