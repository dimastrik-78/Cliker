using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volum : MonoBehaviour
{
    SaveDataBase DataBase;
    [SerializeField] Slider SliderMusic, SliderEffect;
    [SerializeField] AudioMixer AudioMixer;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        DataBase = new SaveDataBase();
        LoadDataFromJSON();
    }
    public void ChangeMusic()
    {
        AudioMixer.SetFloat("Music", SliderMusic.value);
        SaveNewDataToJSON();
    }
    public void ChangeEffect()
    {
        AudioMixer.SetFloat("Effect", SliderEffect.value);
        SaveNewDataToJSON();
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        AudioMixer.SetFloat("Music", DataBase.DataVolumMusic);
        AudioMixer.SetFloat("Effect", DataBase.DataVolumEffect);

        SliderMusic.value = DataBase.DataVolumMusic;
        SliderEffect.value = DataBase.DataVolumEffect;
    }

    public void SaveNewDataToJSON()
    {
        DataBase.DataVolumMusic = SliderMusic.value;
        DataBase.DataVolumEffect = SliderEffect.value;

        string DataStr = JsonUtility.ToJson(DataBase);
        File.WriteAllText(PATH, DataStr);
    }
}
