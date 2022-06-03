using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    public GameObject StartNewGameButton, MainMenuPanel, StartNewGamePanel, SettingsPanel, DarkPanelObj;

    private int _whatScreen;

    SaveDataBase DataBase;

    private const string PATH = @"Assets\Resources\DataBase.txt";
    [SerializeField] int NewGameLeveSlotMachine = 1, NewGameTicket = 0, NewGameGettingTicket = 1, NewGameTimeSecond = 0, NewGameTimeMinute = 5;
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
            if (DataBase.DataGameStart == true)
            {
                StartNewGameButton.SetActive(true);
            }
        }
    }
    public void PlayGame()
    {
        if (DataBase.DataGameFirstStart == true)
            _whatScreen = 5;
        else _whatScreen = 1;
        SaveNewDataToJSON();
        SceneTransition();
    }
    public void StartNewGame()
    {
        StartNewGamePanel.SetActive(true);
    }
    public void NoStartNewGame()
    {
        StartNewGamePanel.SetActive(false);
    }
    public void YesStartNewGame()
    {
        _whatScreen = 1;
        SaveNewDataToJSON();
        SceneTransition();
    }
    public void Settings()
    {
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
        _whatScreen = 4;
        SceneTransition();
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
        //audioSetting.MusicVolum = SliderMusic.value;
        //audioSetting.FVXVolum = SliderVFX.value;

        //audioSetting.ToggleMusic = ToggleMusic.isOn;
        //audioSetting.ToggleVFX = ToggleVFX.isOn;

        DataBase.DataGameStart = true;
        DataBase.DataLeveSlotMachine = NewGameLeveSlotMachine;
        DataBase.DataTicket = NewGameTicket;
        DataBase.DataGettingTicket = NewGameGettingTicket;
        DataBase.DataGaemTimeSecond = NewGameTimeSecond;
        DataBase.DataGameTimeMinute = NewGameTimeMinute;

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
