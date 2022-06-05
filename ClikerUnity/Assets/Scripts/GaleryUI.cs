using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GaleryUI : MonoBehaviour
{
    public GameObject DarkPanelObj, StartStoryObj, BadEnd, GoodEnd;

    private int _whatScreen;

    SaveDataBase DataBase;

    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        DataBase = new SaveDataBase();
        LoadDataFromJSON();
        if (DataBase.DataStartStoryReceived == true)
            StartStoryObj.SetActive(false);
        if (DataBase.DataBadEndReceived == true)
            BadEnd.SetActive(false);
        if (DataBase.DataGoodEndReceived == true)
            GoodEnd.SetActive(false);
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
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        
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
