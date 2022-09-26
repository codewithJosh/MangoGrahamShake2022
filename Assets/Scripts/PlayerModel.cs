using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerModel
{

    public string playerName;
    public float playerCapital;

    public Dictionary<DateTime, int> resourceMango;
    public int resourceGraham;
    public int resourceMilk;
    public int resourceIceCubes;
    public int resourceCups;

    public int currentTemperature;
    public float currentPopularity;
    public float currentSatisfaction;

    public PlayerModel(Player _player)
    {

        playerName = _player.playerName;
        playerCapital = _player.playerCapital;

        resourceMango = _player.resourceMango;
        resourceGraham = _player.resourceGraham;
        resourceMilk = _player.resourceMilk;
        resourceIceCubes = _player.resourceIceCubes;
        resourceCups = _player.resourceCups;

        currentTemperature = _player.currentTemperature;
        currentPopularity = _player.currentPopularity;
        currentSatisfaction = _player.currentSatisfaction;

    }

}
