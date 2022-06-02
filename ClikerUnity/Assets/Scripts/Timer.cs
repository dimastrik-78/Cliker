using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    public GameObject DarkPanelObj;
    private float currentTimer, timeToStart = 1;
    private int _minute = 5, _second, time = 300, _whatScreen;
    public Text textTimer, textTest;
    void Start()
    {
        currentTimer = timeToStart;
    }
    void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer < 0)
        {
            currentTimer = timeToStart;
            time--;
            _second--;
            if (_second < 0)
            {
                _minute--;
                _second = 59;
            }
            textTimer.text = $"Time: {_minute}:{_second}";
            textTest.text = $"Time: {time}";
        }
        if (_minute < 0)
        {
            _whatScreen = 4;
            SceneTransition();
        }
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