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

    //private float _currentTimer = 1, _timeToStart = 1;
    [SerializeField] int Ticket = 0, GettingTickets = 1;

    SaveDataBase DataBase;
    private int _whatScreen;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        DataBase = new SaveDataBase();
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
    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseGame()
    {
        //Time.timeScale = 0;
        PausePanel.SetActive(true);
        PauseButton.SetActive(false);
        TravelAreaToShop.GetComponent<BoxCollider>().enabled = false;
        TravelAreaToSlotMachine.GetComponent<BoxCollider>().enabled = false;
    }
    public void ContinueGame()
    {
        TravelAreaToShop.GetComponent<BoxCollider>().enabled = true;
        TravelAreaToSlotMachine.GetComponent<BoxCollider>().enabled = true;
        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
        //Time.timeScale = 1;
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

        Ticket = DataBase.SaveDataTicket;
        GettingTickets = DataBase.SaveDataGettingTicket;

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

        DataBase.SaveDataTicket = Ticket;
        DataBase.SaveDataGettingTicket = GettingTickets;

        string volumeStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, volumeStr);
    }
}