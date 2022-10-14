using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{

    public string playerName;
    public float playerCapital;

    public Dictionary<DateTime, int> mangoLeft;
    public int grahamLeft;
    public int milkLeft;
    public int iceCubesLeft;
    public int cupsLeft;

    public int currentTemperature;
    public float currentPopularity;
    public float currentSatisfaction;

    public void NewPlayer(string _playerName)
    {

        playerName = _playerName;
        mangoLeft = new Dictionary<DateTime, int>();
        playerCapital = 2000.00f;
        grahamLeft = 0;
        milkLeft = 0;
        iceCubesLeft = 0;
        cupsLeft = 0;
        currentTemperature = 0;
        currentPopularity = 0.1f;
        currentSatisfaction = 1f;
        currentTemperature = UnityEngine.Random.Range(20, 45);

        SavePlayer();

    }

    public void SavePlayer()
    {

        Database.SavePlayer(this);

    }

    public void LoadPlayer()
    {

        PlayerModel player = Database.LoadPlayer();

        playerName = player.playerName;
        playerCapital = player.playerCapital;

        mangoLeft = player.resourceMango;
        grahamLeft = player.resourceGraham;
        milkLeft = player.resourceMilk;
        iceCubesLeft = player.resourceIceCubes;
        cupsLeft = player.resourceCups;

        currentTemperature = player.currentTemperature;
        currentPopularity = player.currentPopularity;
        currentSatisfaction = player.currentSatisfaction;

    }

}
