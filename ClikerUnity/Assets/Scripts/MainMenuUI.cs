using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    public GameObject MainMenuPanel, StartNewGamePanel, SettingsPanel, DarkPanelObj;
    private int _whatScreen;
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
        _whatScreen = 1;
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
        //Добавить переменные, которые будут хранить начальное значение всех переменных чтобы обнулять игру, сделать логику сброса прогресса
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
        SaveSoundSettings();
    }
    public void SaveSoundSettings()
    {

    }
    public void MovingToGalary()
    {
        _whatScreen = 4;
        SceneTransition();
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
