using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CheckingForBuffs : MonoBehaviour
{
    public Text TextTimer;

    private float _currentTimer, _timeToStart = 1;
    private Object[] _allItems;

    [HideInInspector] public ItemSO[] item;

    SaveDataBase DataBase;
    [SerializeField] int TimeActiveBuff, CountAcriveBuff;
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
        TimeActiveBuff = item[DataBase.DataItemIsActivated].ItemTimeAction;
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
            CountAcriveBuff--;
        }
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        TimeActiveBuff = DataBase.DataTimeActiveBuff;
        CountAcriveBuff = DataBase.HowManyActiveBuffs;
    }

    public void SaveDataToJSON()
    {
        DataBase.DataTimeActiveBuff = TimeActiveBuff;

        string volumeStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, volumeStr);
    }
}
