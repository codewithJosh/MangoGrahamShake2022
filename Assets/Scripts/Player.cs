using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector] public float playerCapital;
    [HideInInspector] public float currentPopularity;
    [HideInInspector] public float currentSatisfaction;
    [HideInInspector] public int mangoLeft;
    [HideInInspector] public int grahamLeft;
    [HideInInspector] public int milkLeft;
    [HideInInspector] public int iceCubesLeft;
    [HideInInspector] public int cupsLeft;
    [HideInInspector] public int currentTemperature;
    [HideInInspector] public int mangoPerServe;
    [HideInInspector] public int grahamPerServe;
    [HideInInspector] public int milkPerServe;
    [HideInInspector] public int iceCubesPerServe;
    [HideInInspector] public string playerName;

    public void NewPlayer(string _playerName)
    {
 
        playerCapital = 2000.00f;
        currentPopularity = 0.1f;
        currentSatisfaction = 1f;
        mangoLeft = 0;
        grahamLeft = 0;
        milkLeft = 0;
        iceCubesLeft = 0;
        cupsLeft = 0;
        currentTemperature = UnityEngine.Random.Range(20, 45);
        mangoPerServe = 4;
        grahamPerServe = 2;
        milkPerServe = 2;
        iceCubesPerServe = 2;
        playerName = _playerName;

        SavePlayer();

    }

    public void SavePlayer()
    {

        Database.SavePlayer(this);

    }

    public void LoadPlayer()
    {

        PlayerModel player = Database.LoadPlayer();

        playerCapital = player.playerCapital;
        currentPopularity = player.currentPopularity;
        currentSatisfaction = player.currentSatisfaction;
        mangoLeft = player.mangoLeft;
        grahamLeft = player.grahamLeft;
        milkLeft = player.milkLeft;
        iceCubesLeft = player.iceCubesLeft;
        cupsLeft = player.cupsLeft;
        currentTemperature = player.currentTemperature;
        mangoPerServe = player.mangoPerServe;
        grahamPerServe = player.grahamPerServe;
        milkPerServe = player.milkPerServe;
        iceCubesPerServe = player.iceCubesPerServe;
        playerName = player.playerName;

    }

}
