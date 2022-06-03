using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ShopUI : MonoBehaviour
{
    public AudioSource Audio;
    public GameObject PauseButton, ContinueButton, ShopPanel, PausePanel, DarkPanelObj;
    public Text AllTicketText;
    public GameTimer GameTimer;

    private int _whatScreen;

    SaveDataBase DataBase;
    [SerializeField] int Ticket = 0;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        DataBase = new SaveDataBase();
        LoadDataFromJSON();
        UpdateText();
    }
    void Update()
    {
        
    }
    public void PauseGame()
    {
        GameTimer.Pause = true;
        PausePanel.SetActive(true);
        ShopPanel.SetActive(false);
        PauseButton.SetActive(false);
    }
    public void ContinueGame()
    {
        GameTimer.Pause = false;
        PausePanel.SetActive(false);
        ShopPanel.SetActive(true);
        PauseButton.SetActive(true);
    }
    public void MovingToSlotMachine()
    {
        _whatScreen = 2;
        SceneTransition();
    }
    public void MovingToHall()
    {
        _whatScreen = 1;
        SceneTransition();
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
        DataBase.DataTicket = Ticket;
        //DataBase.DataGettingTicket = GettingTickets;
        //DataBase.DataGaemTimeSecond = NewGaemTimeSecond;
        //DataBase.DataGameTimeMinute = NewGameTimeMinute;

        string volumeStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, volumeStr);
    }
    private void UpdateText()
    {
        AllTicketText.text = $"Ticket: {Ticket}";
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
