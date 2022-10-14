using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{

    [HideInInspector] public string playerName;
    [HideInInspector] public float playerCapital;
    [HideInInspector] public int mangoLeft;
    [HideInInspector] public int grahamLeft;
    [HideInInspector] public int milkLeft;
    [HideInInspector] public int iceCubesLeft;
    [HideInInspector] public int cupsLeft;
    [HideInInspector] public int currentTemperature;
    [HideInInspector] public float currentPopularity;
    [HideInInspector] public float currentSatisfaction;

    public void NewPlayer(string _playerName)
    {

        playerName = _playerName;
        playerCapital = 2000.00f;
        mangoLeft = 0;
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

        mangoLeft = player.mangoLeft;
        grahamLeft = player.grahamLeft;
        milkLeft = player.milkLeft;
        iceCubesLeft = player.iceCubesLeft;
        cupsLeft = player.cupsLeft;

        currentTemperature = player.currentTemperature;
        currentPopularity = player.currentPopularity;
        currentSatisfaction = player.currentSatisfaction;

    }

}
