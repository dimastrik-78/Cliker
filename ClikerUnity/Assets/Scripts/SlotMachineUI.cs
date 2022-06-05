using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SlotMachineUI : MonoBehaviour
{
    public AudioSource AudioMove, AudioClick;
    public GameObject PauseButton, ContinueButton, UpgradeButton, GamePanel, PausePanel, DarkPanelObj, ActiveItem;
    public Text AllTicketText, UpgradeText;
    public GameTimer GameTimer;
    public TimerActiveBuff TimerActiveBuff;

    [HideInInspector] public ItemSO[] item;

    private float _currentTimer, _timeToStart = 1;
    private Object[] _allItems;
    private int _whatScreen, _timerActiveEffect;

    SaveDataBase DataBase;
    [SerializeField] int Ticket, GettingTickets, Level;
    [SerializeField] AudioMixer AudioMixer;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        if (PlayerPrefs.HasKey("Ticket"))
            Ticket = PlayerPrefs.GetInt("Ticket");
        if (PlayerPrefs.HasKey("GettingTickets"))
            GettingTickets = PlayerPrefs.GetInt("GettingTickets");
        if (PlayerPrefs.HasKey("Level"))
            Level = PlayerPrefs.GetInt("Level");

        DataBase = new SaveDataBase();
        LoadDataFromJSON();
        UpdateText();
        if (DataBase.HowManyActiveBuffs > 0)
        {
            ActiveItem.SetActive(true);
        }

        _timerActiveEffect = DataBase.DataTimeActiveBuff;

        _allItems = Resources.LoadAll("items", typeof(ItemSO));
        item = new ItemSO[_allItems.Length];
        for (int i = 0; i < _allItems.Length; i++)
        {
            item[i] = (ItemSO)_allItems[i];
        }
    }
    void Update()
    {
        _currentTimer -= Time.deltaTime;
        if (_currentTimer < 0)
        {
            _currentTimer = _timeToStart;
            _timerActiveEffect--;
        }
    }
    public void GiveMeTicket()
    {
        AudioClick.Play();
        if (_timerActiveEffect >= 0)
        {
            Ticket += GettingTickets * item[DataBase.DataItemIsActivated].ItemEffect;
        }
        else Ticket += GettingTickets;
        PlayerPrefs.SetInt("Ticket", Ticket);
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
        PlayerPrefs.SetInt("GettingTickets", GettingTickets);
        PlayerPrefs.SetInt("Level", Level);
        UpdateText();
    }
    public void PauseGame()
    {
        GameTimer.Pause = true;
        TimerActiveBuff.Pause = true;
        PausePanel.SetActive(true);
        GamePanel.SetActive(false);
        PauseButton.SetActive(false);
    }
    public void ContinueGame()
    {
        GameTimer.Pause = false;
        TimerActiveBuff.Pause = false;
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
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        AudioMixer.SetFloat("Music", DataBase.DataVolumMusic);
        AudioMixer.SetFloat("Effect", DataBase.DataVolumEffect);
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
        PlayerPrefs.Save();
        GameTimer.SaveDataToJSON();
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
