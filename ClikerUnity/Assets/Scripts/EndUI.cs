using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndUI : MonoBehaviour
{
    public GameObject StartGameImg, BadEndImg, GoodEndImg;

    private int Ticket;

    SaveDataBase DataBase;

    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        if (PlayerPrefs.HasKey("Ticket"))
            Ticket = PlayerPrefs.GetInt("Ticket");

        DataBase = new SaveDataBase();
        LoadDataFromJSON();

        if (DataBase.DataPlayerPlayed == false)
        {
            StartGameImg.SetActive(true);
        }
        if (Ticket < 1500 && DataBase.DataGameTimeMinute <= 0)
        {
            BadEndImg.SetActive(true);
        }
        else if (Ticket >= 1500 && DataBase.DataGameTimeMinute <= 0)
            GoodEndImg.SetActive(true);
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);
    }
}
