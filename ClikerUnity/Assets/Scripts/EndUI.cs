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

    SaveDataBase DataBase;
    //[SerializeField]
    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        DataBase = new SaveDataBase();

    }
    void Update() //DataGameFirstStart
    {
        
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        //Ticket = DataBase.SaveDataTicket;
        //GettingTickets = DataBase.SaveDataGettingTicket;

        //AudioMixer.SetFloat("MainMusic", audioSetting.MusicVolum);
        //AudioMixer.SetFloat("VFX", audioSetting.FVXVolum);

        //SliderMusic.value = audioSetting.MusicVolum;
        //SliderVFX.value = audioSetting.FVXVolum;

        //ToggleMusic.isOn = audioSetting.ToggleMusic;
        //ToggleVFX.isOn = audioSetting.ToggleVFX;
    }

    public void SaveNewDataToJSON()
    {
        //audioSetting.MusicVolum = SliderMusic.value;
        //audioSetting.FVXVolum = SliderVFX.value;

        //audioSetting.ToggleMusic = ToggleMusic.isOn;
        //audioSetting.ToggleVFX = ToggleVFX.isOn;

        string DataStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, DataStr);
    }
}
