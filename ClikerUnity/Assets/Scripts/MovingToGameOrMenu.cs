using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MovingToGameOrMenu : MonoBehaviour
{
    public GameObject DarkPanelObj;

    private int _whatScreen;

    SaveDataBase DataBase;

    [SerializeField] bool PlayerPlayed;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        DataBase = new SaveDataBase();
        LoadDataFromJSON();
        if (DataBase.DataPlayerPlayed == false)
        {
            _whatScreen = 1;
            DataBase.DataPlayerPlayed = true;
            DataBase.DataStartStoryReceived = true;
        }
        else
        {
            _whatScreen = 0;
            DataBase.DataPlayerPlayed = false;
        }
    }
    void Update()
    {
        
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);
    }
    public void SaveDataToJSON()
    {
        string volumeStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, volumeStr);
    }
    private void OnMouseUp()
    {
        SaveDataToJSON();
        DarkPanelObj.SetActive(true);
        DarkPanelObj.GetComponent<CanvasGroup>().DOFade(endValue: 1, 1f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(_whatScreen);
                DarkPanelObj.GetComponent<CanvasGroup>().DOKill();
            });
    }
}
