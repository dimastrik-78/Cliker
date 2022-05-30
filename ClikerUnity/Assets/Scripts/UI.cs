using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
        //Добавить переменные, которые будут хранить начальное значение всех переменных чтобы обнулять игру, сделать логику сброса прогресса
    }
    public void Settings()
    {

    }
    public void Galary()
    {

    }
}
