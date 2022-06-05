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
    public GameObject PauseButton, ContinueButton, PausePanel, DarkPanelObj, TravelAreaToShop, TravelAreaToSlotMachine, DarkPanel, ActiveItem;
    public Text AllTicketText;
    public GameTimer GameTimer;
    public TimerActiveBuff TimerActiveBuff;
    
    private int _whatScreen;

    SaveDataBase DataBase;
    [SerializeField] int Ticket = 0;
    [SerializeField] AudioMixer AudioMixer;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        if (PlayerPrefs.HasKey("Ticket"))
            Ticket = PlayerPrefs.GetInt("Ticket");

        DataBase = new SaveDataBase();
        LoadDataFromJSON();
        UpdateText();

        if (DataBase.HowManyActiveBuffs > 0)
        {
            ActiveItem.SetActive(true);
        }
    }
    public void PauseGame()
    {
        GameTimer.Pause = true;
        TimerActiveBuff.Pause = true;
        PausePanel.SetActive(true);
        PauseButton.SetActive(false);
        TravelAreaToShop.GetComponent<BoxCollider>().enabled = false;
        TravelAreaToSlotMachine.GetComponent<BoxCollider>().enabled = false;
    }
    public void ContinueGame()
    {
        GameTimer.Pause = false;
        TimerActiveBuff.Pause = false;
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
    private void UpdateText()
    {
        AllTicketText.text = $"Ticket: {Ticket}";
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        AudioMixer.SetFloat("Music", DataBase.DataVolumMusic);
        AudioMixer.SetFloat("Effect", DataBase.DataVolumEffect);
    }
    private void SceneTransition()
    {
        PlayerPrefs.Save();
        GameTimer.SaveDataToJSON();
        DarkPanelObj.SetActive(true);
        DarkPanelObj.GetComponent<CanvasGroup>().DOFade(endValue: 1, 1f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(_whatScreen);
                DarkPanelObj.GetComponent<CanvasGroup>().DOKill();
            });
    }
}