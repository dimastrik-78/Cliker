using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GaleryUI : MonoBehaviour
{
    public GameObject DarkPanelObj;
    private int WhatScreen;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Hall()
    {
        WhatScreen = 1;
        AppearanceDarkScreen();
    }
    public void MainMenu()
    {
        WhatScreen = 0;
        AppearanceDarkScreen();
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
