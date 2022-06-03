using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TemporaryEnding : MonoBehaviour
{
    public Text Text;

    SaveDataBase DataBase;
    [SerializeField] int Ticket, GettingTickets, Level;
    private const string PATH = @"Assets\Resources\DataBase.txt";
    void Start()
    {
        DataBase = new SaveDataBase();
        LoadDataFromJSON();
        if (DataBase.DataTicket < 1500)
        {
            Text.text = $"You have collected tickets {Ticket} \n You lost to your friends";
        }
        else Text.text = $"You have collected tickets {Ticket} \n You won your friends";
    }
    void Update()
    {
        
    }
    public void LoadDataFromJSON()
    {
        string jsonStr = File.ReadAllText(PATH);
        DataBase = JsonUtility.FromJson<SaveDataBase>(jsonStr);

        Ticket = DataBase.DataTicket;
    }
}
