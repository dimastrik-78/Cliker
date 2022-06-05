using System;
using System.Collections.Generic;

[Serializable]
public class SaveDataBase
{
    public float DataVolumMusic;
    public float DataVolumEffect;

    public bool DataGameFirstStart;

    public bool DataPlayerPlayed;
    public bool DataStartStoryReceived;
    public bool DataGoodEndReceived;
    public bool DataBadEndReceived;

    public int DataLeveSlotMachine;
    public int DataTicket;
    public int DataGettingTicket;

    public int DataGameTimeMinute;
    public int DataGameTimeSecond;

    public int DataItemIsActivated;
    public int DataTimeActiveBuff;
    public int HowManyActiveBuffs;
    public int[] ArrayActivatedItems;
}