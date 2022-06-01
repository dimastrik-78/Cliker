using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SlotMachineUI : MonoBehaviour
{
    public GameObject PauseButton, ContinueButton, GamePanel, PausePanel, DarkPanelObj;
    public Text AllTicketText;
    private int WhatScreen;
    [SerializeField] int Ticket = 0, GettingTickets = 1;
    void Start()
    {
        
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
        if (Ticket >= 25)
        {
            Ticket -= 25;
            GettingTickets++;
            UpdateText();
        }
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
        WhatScreen = 3;
        AppearanceDarkScreen();
    }
    public void MovingToHall()
    {
        WhatScreen = 1;
        AppearanceDarkScreen();
    }
    public void MovingToMainMenu()
    {
        WhatScreen = 0;
        AppearanceDarkScreen();
    }
    public void MovingToGalary()
    {
        WhatScreen = 4;
        AppearanceDarkScreen();
    }
    private void UpdateText()
    {
        AllTicketText.text = $"Ticket: {Ticket}";
    }
    private void AppearanceDarkScreen()
    {
        DarkPanelObj.SetActive(true);
        DarkPanelObj.GetComponent<CanvasGroup>().DOFade(endValue: 1, 2f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(WhatScreen);
                DarkPanelObj.GetComponent<CanvasGroup>().DOKill();
            });
    }
}
