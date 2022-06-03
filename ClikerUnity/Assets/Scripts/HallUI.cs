using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class HallUI : MonoBehaviour
{
    public GameObject PauseButton, ContinueButton, PausePanel, DarkPanelObj, TravelAreaToShop, TravelAreaToSlotMachine, DarkPanel;
    public Text AllTicketText;
    public GameTimer GameTimer;

    //private float _currentTimer = 1, _timeToStart = 1;
    [SerializeField] int Ticket = 0;

    private int _whatScreen;

    SaveDataBase DataBase;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        DataBase = new SaveDataBase();
        LoadDataFromJSON();
        UpdateText();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseGame()
    {
        GameTimer.Pause = true;
        PausePanel.SetActive(true);
        PauseButton.SetActive(false);
        TravelAreaToShop.GetComponent<BoxCollider>().enabled = false;
        TravelAreaToSlotMachine.GetComponent<BoxCollider>().enabled = false;
    }
    public void ContinueGame()
    {
        GameTimer.Pause = false;
        TravelAreaToShop.GetComponent<BoxCollider>().enabled = true;
        TravelAreaToSlotMachine.GetComponent<BoxCollider>().enabled = true;
        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
    }
    public void MovingToMainMenu()
    {
        _whatScreen = 0;
        SceneTransition();
    }
    public void MovingToGalary()
    {
        _whatScreen = 4;
        SceneTransition();
    }
    private void SceneTransition()
    {
        GameTimer.SaveDataToJSON();
        DarkPanelObj.SetActive(true);
        DarkPanelObj.GetComponent<CanvasGroup>().DOFade(endValue: 1, 1f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(_whatScreen);
                DarkPanelObj.GetComponent<CanvasGroup>().DOKill();
            });
    }
    private void UpdateText()
    {
        AllTicketText.text = $"Ticket: {Ticket}";
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        Ticket = DataBase.DataTicket;

        //AudioMixer.SetFloat("MainMusic", audioSetting.MusicVolum);
        //AudioMixer.SetFloat("VFX", audioSetting.FVXVolum);

        //SliderMusic.value = audioSetting.MusicVolum;
        //SliderVFX.value = audioSetting.FVXVolum;

        //ToggleMusic.isOn = audioSetting.ToggleMusic;
        //ToggleVFX.isOn = audioSetting.ToggleVFX;
    }

    public void SaveDataToJSON()
    {
        //audioSetting.MusicVolum = SliderMusic.value;
        //audioSetting.FVXVolum = SliderVFX.value;

        //audioSetting.ToggleMusic = ToggleMusic.isOn;
        //audioSetting.ToggleVFX = ToggleVFX.isOn;

        //DataBase.DataLeveSlotMachine = NewLeveSlotMachine;
        //DataBase.DataTicket = Ticket;
        //DataBase.DataGettingTicket = GettingTickets;
        //DataBase.DataGaemTimeSecond = NewGaemTimeSecond;
        //DataBase.DataGameTimeMinute = NewGameTimeMinute;

        string volumeStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, volumeStr);
    }
}