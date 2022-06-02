using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ShopUI : MonoBehaviour
{
    public GameObject PauseButton, ContinueButton, ShopPanel, PausePanel, DarkPanelObj;
    public Text AllTicketText;
    private int _whatScreen;
    [SerializeField] int Ticket = 0, GettingTickets = 1;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void PauseGame()
    {
        //Time.timeScale = 0;
        PausePanel.SetActive(true);
        ShopPanel.SetActive(false);
        PauseButton.SetActive(false);
    }
    public void ContinueGame()
    {
        PausePanel.SetActive(false);
        ShopPanel.SetActive(true);
        PauseButton.SetActive(true);
        //Time.timeScale = 1;
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
