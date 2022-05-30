using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject PauseButton, ContinueButton, PausePanel;
    public Text AllTicketText, TicketOfSecondText;

    private float currentTimer, timeToStart;
    SaveDataBase DataBase;
    [SerializeField] int Ticket = 0, TicketOfSecond = 0, GettingTickets = 1;

    private const string PATH = @"Assets\Resources\DataBase.txt";
    // Минимально жизнеспособный продукт (англ. minimum viable product, MVP) — продукт, обладающий минимальными, но достаточными для удовлетворения первых потребителей функциями.
    void Start()
    {
        DataBase = new SaveDataBase();

        if (File.Exists(PATH) == false)
        {
            File.Create(PATH);
            SaveVolToJSON();
        }
        else
        {
            LoadVolFromJSON();
        }
    }
    // Update is called once per frame
    void Update()
    {
        SaveVolToJSON();
        if (TicketOfSecond > 0)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer < 0)
            {
                currentTimer = timeToStart;
                Ticket += TicketOfSecond;
                UpdateText();
            }
        }
    }
    public void GiveMeTicket()
    {
        Ticket += GettingTickets;
        UpdateText();
    }
    public void UpgradeClicks()
    {
        if (Ticket >= 25)
        {
            Ticket -= 25;
            GettingTickets++;
            UpdateText();
        }
    }
    public void AutomaticClicksX1()
    {
        if (Ticket >= 50)
        {
            Ticket -= 50;
            TicketOfSecond++;
            UpdateText();
        }
    }
    public void AutomaticClicksX2()
    {
        if (Ticket >= 150)
        {
            Ticket -= 150;
            TicketOfSecond += 2;
            UpdateText();
        }
    }
    public void AutomaticClicksX3()
    {
        if (Ticket >= 300)
        {
            Ticket -= 300;
            TicketOfSecond += 3;
            UpdateText();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        PauseButton.SetActive(false);
        TicketOfSecondText.text = $"T/S: 0";
    }
    private void UpdateText()
    {
        TicketOfSecondText.text = $"T/S: {TicketOfSecond}";
        AllTicketText.text = $"Ticket: {Ticket}";
    }
    public void LoadVolFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        Ticket = DataBase.Tickets;
        TicketOfSecond = DataBase.TicketsOfSecond;
        GettingTickets = DataBase.GettingTickets;

        //AudioMixer.SetFloat("MainMusic", audioSetting.MusicVolum);
        //AudioMixer.SetFloat("VFX", audioSetting.FVXVolum);

        //SliderMusic.value = audioSetting.MusicVolum;
        //SliderVFX.value = audioSetting.FVXVolum;

        //ToggleMusic.isOn = audioSetting.ToggleMusic;
        //ToggleVFX.isOn = audioSetting.ToggleVFX;
    }

    public void SaveVolToJSON()
    {
        //audioSetting.MusicVolum = SliderMusic.value;
        //audioSetting.FVXVolum = SliderVFX.value;

        //audioSetting.ToggleMusic = ToggleMusic.isOn;
        //audioSetting.ToggleVFX = ToggleVFX.isOn;

        DataBase.Tickets = Ticket;
        DataBase.TicketsOfSecond = TicketOfSecond;
        DataBase.GettingTickets = GettingTickets;

        string volumeStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, volumeStr);
    }
}
//currentTimer -= Time.deltaTime;
//line.fillAmount = 1 - (currentTimer / timeToStart);
//if (currentTimer < 0)
//{
//    currentTimer = timeToStart;
//}