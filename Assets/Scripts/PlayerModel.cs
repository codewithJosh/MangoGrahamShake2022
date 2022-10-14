using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerModel
{

    public string playerName;
    public float playerCapital;

    public int mangoLeft;
    public int grahamLeft;
    public int milkLeft;
    public int iceCubesLeft;
    public int cupsLeft;

    public int currentTemperature;
    public float currentPopularity;
    public float currentSatisfaction;

    public PlayerModel(Player _player)
    {

        playerName = _player.playerName;
        playerCapital = _player.playerCapital;

        mangoLeft = _player.mangoLeft;
        grahamLeft = _player.grahamLeft;
        milkLeft = _player.milkLeft;
        iceCubesLeft = _player.iceCubesLeft;
        cupsLeft = _player.cupsLeft;

        currentTemperature = _player.currentTemperature;
        currentPopularity = _player.currentPopularity;
        currentSatisfaction = _player.currentSatisfaction;

    }

}
