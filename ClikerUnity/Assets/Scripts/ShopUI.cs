using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Audio;

public class ShopUI : MonoBehaviour
{
    public AudioSource Audio;
    public GameObject PauseButton, ContinueButton, ShopPanel, PausePanel, DarkPanelObj, ActiveItem;
    public Text AllTicketText;
    public GameTimer GameTimer;
    public TimerActiveBuff TimerActiveBuff;

    [HideInInspector] public ItemSO[] item;

    private int _whatScreen;

    SaveDataBase DataBase; 
    [SerializeField] AudioMixer AudioMixer;
    [SerializeField] public int Ticket;
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
        ShopPanel.SetActive(false);
        PauseButton.SetActive(false);
    }
    public void ContinueGame()
    {
        GameTimer.Pause = false;
        TimerActiveBuff.Pause = false;
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
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        AudioMixer.SetFloat("Music", DataBase.DataVolumMusic);
        AudioMixer.SetFloat("Effect", DataBase.DataVolumEffect);
    }
    public void UpdateText()
    {
        PlayerPrefs.SetInt("Ticket", Ticket);
        AllTicketText.text = $"Ticket: {Ticket}";
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
