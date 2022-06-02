using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GaleryUI : MonoBehaviour
{
    public GameObject DarkPanelObj;
    private int _whatScreen;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Hall()
    {
        _whatScreen = 1;
        SceneTransition();
    }
    public void MainMenu()
    {
        _whatScreen = 0;
        SceneTransition();
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
