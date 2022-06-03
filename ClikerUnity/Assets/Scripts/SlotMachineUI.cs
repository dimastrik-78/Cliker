using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SlotMachineUI : MonoBehaviour
{
    public AudioSource AudioMove, AudioClick;
    public GameObject PauseButton, ContinueButton, UpgradeButton, GamePanel, PausePanel, DarkPanelObj;
    public Text AllTicketText, UpgradeText;
    public GameTimer GameTimer;

    private int _whatScreen;

    SaveDataBase DataBase;
    [SerializeField] int Ticket, GettingTickets, Level;
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
    public void GiveMeTicket()
    {
        AudioClick.Play();
        Ticket += GettingTickets;
        SaveDataToJSON();
        UpdateText();
    }
    public void UpgradeClicks()
    {
        switch (Level)
        {
            case 1:
                if (Ticket >= 30)
                {
                    Ticket -= 30;
                    GettingTickets++;
                    Level++;
                }
                break;
            case 2:
                if (Ticket >= 300)
                {
                    Ticket -= 300;
                    GettingTickets++;
                    Level++;
                }
                break;
            case 3:
                if (Ticket >= 900)
                {
                    Ticket -= 900;
                    GettingTickets++;
                    Level++;
                }
                break;
            case 4:
                if (Ticket >= 1200)
                {
                    Ticket -= 1200;
                    GettingTickets++;
                    Level++;
                }
                break;
        }
        UpdateText();
    }
    public void PauseGame()
    {
        GameTimer.Pause = true;
        PausePanel.SetActive(true);
        GamePanel.SetActive(false);
        PauseButton.SetActive(false);
    }
    public void ContinueGame()
    {
        GameTimer.Pause = false;
        PausePanel.SetActive(false);
        GamePanel.SetActive(true);
        PauseButton.SetActive(true);
    }
    public void MovingToTheShop()
    {
        _whatScreen = 3;
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
        GettingTickets = DataBase.DataGettingTicket;
        Level = DataBase.DataLeveSlotMachine;

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

        DataBase.DataLeveSlotMachine = Level;
        DataBase.DataTicket = Ticket;
        DataBase.DataGettingTicket = GettingTickets;
        //DataBase.DataGaemTimeSecond = NewGaemTimeSecond;
        //DataBase.DataGameTimeMinute = NewGameTimeMinute;

        string volumeStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, volumeStr);
    }
    private void UpdateText()
    {
        AllTicketText.text = $"Ticket: {Ticket}";
        if (Level == 1)
            UpgradeText.text = $"Upgrade ({Level} -> {Level + 1}) \n 30 Ticket";
        else if (Level == 2)
            UpgradeText.text = $"Upgrade ({Level} -> {Level + 1}) \n 300 Ticket";
        else if (Level == 3)
            UpgradeText.text = $"Upgrade ({Level} -> {Level + 1}) \n 900 Ticket";
        else if (Level == 4)
            UpgradeText.text = $"Upgrade ({Level} -> {Level + 1}) \n 900 Ticket";
        else if (Level == 5)
            UpgradeText.text = $"Upgrade (Max)";
    }
    private void SceneTransition()
    {
        AudioMove.Play();
        DarkPanelObj.SetActive(true);
        DarkPanelObj.GetComponent<CanvasGroup>().DOFade(endValue: 1, 1f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(_whatScreen);
                DarkPanelObj.GetComponent<CanvasGroup>().DOKill();
            });
    }
}
