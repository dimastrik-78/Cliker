using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Audio;

public class MainMenuUI : MonoBehaviour
{
    public GameObject StartNewGameButton, MainMenuPanel, StartNewGamePanel, SettingsPanel, DarkPanelObj;

    private int _whatScreen;
    private UnityEngine.Object[] _allItems;

    [HideInInspector] public ItemSO[] allItem;
    [HideInInspector] public int itemNumber;

    SaveDataBase DataBase;
    [SerializeField] bool FirstStartGame;
    [SerializeField] AudioMixer AudioMixer;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        DataBase = new SaveDataBase();
        if (File.Exists(PATH) == false)
        {
            File.Create(PATH);
        }
        else
        {
            LoadDataFromJSON();
            if (DataBase.DataPlayerPlayed == true)
            {
                StartNewGameButton.SetActive(true);
            }
        }
    }
    public void PlayGame()
    {
        if (FirstStartGame == true)
            CreateNewDataToJSON();
        if (DataBase.DataPlayerPlayed == false)
        {
            _whatScreen = 5;
            SaveNewDataToJSON();

        }
        else _whatScreen = 1;
        SceneTransition();
    }
    public void StartNewGame()
    {
        if (FirstStartGame == true)
            CreateNewDataToJSON();
        StartNewGamePanel.SetActive(true);
    }
    public void NoStartNewGame()
    {
        StartNewGamePanel.SetActive(false);
    }
    public void YesStartNewGame()
    {
        _whatScreen = 5;
        SaveNewDataToJSON();
        SceneTransition();
    }
    public void Settings()
    {
        if (FirstStartGame == true)
            CreateNewDataToJSON();
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    public void ReturnMainMenu()
    {
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }
    public void MovingToGalary()
    {
        if (FirstStartGame == true)
            CreateNewDataToJSON();
        _whatScreen = 4;
        SceneTransition();
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        FirstStartGame = DataBase.DataGameFirstStart;

        AudioMixer.SetFloat("Music", DataBase.DataVolumMusic);
        AudioMixer.SetFloat("Effect", DataBase.DataVolumEffect);
    }

    public void SaveNewDataToJSON()
    {
        _allItems = Resources.LoadAll("items", typeof(ItemSO));

        DataBase.DataPlayerPlayed = false;

        PlayerPrefs.SetInt("Ticket", 0);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("GettingTickets", 1);

        DataBase.DataGameTimeMinute = 5;
        DataBase.DataGameTimeSecond = 0;

        DataBase.DataItemIsActivated = 0;
        DataBase.DataTimeActiveBuff = 0;
        DataBase.HowManyActiveBuffs = 0;
        for (int i = 0; i < _allItems.Length; i++)
        {
            DataBase.ArrayActivatedItems[i] = -1;
        }

        string DataStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, DataStr);
    }
    public void CreateNewDataToJSON()
    {
        _allItems = Resources.LoadAll("items", typeof(ItemSO));

        PlayerPrefs.SetInt("Ticket", 0);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("GettingTickets", 1);

        DataBase.DataVolumMusic = 0f;
        DataBase.DataVolumEffect = 0f;

        DataBase.DataStartStoryReceived = false;
        DataBase.DataPlayerPlayed = false;
        DataBase.DataGoodEndReceived = false;
        DataBase.DataBadEndReceived = false;

        DataBase.DataGameTimeMinute = 5;
        DataBase.DataGameTimeSecond = 0;

        DataBase.DataItemIsActivated = 0;
        DataBase.DataTimeActiveBuff = 0;
        DataBase.HowManyActiveBuffs = 0;
        DataBase.ArrayActivatedItems = new int[_allItems.Length];
        for (int i = 0; i < _allItems.Length; i++)
        {
            DataBase.ArrayActivatedItems[i] = -1;
        }

        string DataStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, DataStr);
    }
    private void SceneTransition()
    {
        DarkPanelObj.SetActive(true);
        DarkPanelObj.GetComponent<CanvasGroup>().DOFade(endValue: 1, 1f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(_whatScreen);
                DarkPanelObj.GetComponent<CanvasGroup>().DOKill();
            });
    }
}
