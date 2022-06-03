using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.IO;

public class GameTimer : MonoBehaviour
{
    public GameObject DarkPanelObj;
    public Text TextTimer;
    public bool Pause;
    
    private float currentTimer, timeToStart = 1;
    private int _whatScreen;

    SaveDataBase DataBase;
    [SerializeField] int Minute = 5, Second;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        DataBase = new SaveDataBase();
        LoadDataFromJSON();
    }
    void Update()
    {
        if (Pause == false)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer < 0)
            {
                currentTimer = timeToStart;
                Second--;
                if (Second < 0)
                {
                    Minute--;
                    Second = 59;
                }
                TextTimer.text = $"Time: {Minute}:{Second}";
            }
            if (Minute < 0)
            {
                _whatScreen = 4;
                SceneTransition();
            }
        }
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        Minute = DataBase.DataGameTimeMinute;
        Second = DataBase.DataGaemTimeSecond;

    }

    public void SaveDataToJSON()
    {
        DataBase.DataGameTimeMinute = Minute;
        DataBase.DataGaemTimeSecond = Second;

        string volumeStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, volumeStr);
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