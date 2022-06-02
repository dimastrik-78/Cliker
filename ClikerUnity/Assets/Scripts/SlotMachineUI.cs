using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SlotMachineUI : MonoBehaviour
{
    public GameObject PauseButton, ContinueButton, UpgradeButton, GamePanel, PausePanel, DarkPanelObj;
    public Text AllTicketText, UpgradeText;
    private int _whatScreen;
    [SerializeField] int Ticket = 0, GettingTickets = 1, Level = 1;
    void Start()
    {
        UpdateText();
    }
    void Update()
    {
        
    }
    public void GiveMeTicket()
    {
        Ticket += GettingTickets;
        UpdateText();
    }
    public void UpgradeClicks()
    {
        switch (Level)
        {
            case 1:
                if (Ticket >= 45)
                {
                    Ticket -= 45;
                    GettingTickets++;
                    Level++;
                    UpgradeText.text = $"Upgrade ({Level} -> {Level + 1}) \n 450 Ticket";
                }
                break;
            case 2:
                if (Ticket >= 450)
                {
                    Ticket -= 450;
                    GettingTickets++;
                    Level++;
                    UpgradeText.text = $"Upgrade ({Level} -> {Level + 1}) \n 1350 Ticket";
                }
                break;
            case 3:
                if (Ticket >= 1350)
                {
                    Ticket -= 1350;
                    GettingTickets++;
                    Level++;
                    UpgradeText.text = $"Upgrade ({Level} -> {Level + 1}) \n 1800 Ticket";
                }
                break;
            case 4:
                if (Ticket >= 1800)
                {
                    Ticket -= 1800;
                    GettingTickets++;
                    Level++;
                    UpgradeText.text = $"Upgrade (Max)";
                }
                break;
        }
        UpdateText();
    }
    public void PauseGame()
    {
        //Time.timeScale = 0;
        PausePanel.SetActive(true);
        GamePanel.SetActive(false);
        PauseButton.SetActive(false);
    }
    public void ContinueGame()
    {
        PausePanel.SetActive(false);
        GamePanel.SetActive(true);
        PauseButton.SetActive(true);
        //Time.timeScale = 1;
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
