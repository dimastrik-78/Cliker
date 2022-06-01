using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    public GameObject MainMenuPanel, StartNewGamePanel, SettingsPanel, DarkPanelObj;
    private int WhatScreen;
    void Start()
    {
        //if (File.Exists(PATH) == false)
        //{
        //    File.Create(PATH);
        //    SaveDataToJSON();
        //}
        //else
        //{
        //    LoadDataFromJSON();
        //}
    }
    public void PlayGame()
    {
        WhatScreen = 1;
        AppearanceDarkScreen();
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
        WhatScreen = 1;
        //Добавить переменные, которые будут хранить начальное значение всех переменных чтобы обнулять игру, сделать логику сброса прогресса
        AppearanceDarkScreen();
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
        SaveSoundSettings();
    }
    public void SaveSoundSettings()
    {

    }
    public void MovingToGalary()
    {
        WhatScreen = 4;
        AppearanceDarkScreen();
    }
    private void AppearanceDarkScreen()
    {
        DarkPanelObj.SetActive(true);
        DarkPanelObj.GetComponent<CanvasGroup>().DOFade(endValue: 1, 2f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(WhatScreen);
                DarkPanelObj.GetComponent<CanvasGroup>().DOKill();
            });
    }
}
