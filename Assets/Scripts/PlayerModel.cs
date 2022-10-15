[System.Serializable]
public class PlayerModel
{

    public float currentPopularity;
    public float currentSatisfaction;
    public float playerCapital;
    public float price;
    public float advertisement;
    public int mangoLeft;
    public int grahamLeft;
    public int milkLeft;
    public int iceCubesLeft;
    public int cupsLeft;
    public int currentTemperature;
    public int mangoPerServe;
    public int grahamPerServe;
    public int milkPerServe;
    public int iceCubesPerServe;
    public string playerName;

    public PlayerModel(Player _player)
    {

        playerCapital = _player.playerCapital;
        currentPopularity = _player.currentPopularity;
        currentSatisfaction = _player.currentSatisfaction;
        price = _player.price;
        advertisement = _player.advertisement;
        mangoLeft = _player.mangoLeft;
        grahamLeft = _player.grahamLeft;
        milkLeft = _player.milkLeft;
        iceCubesLeft = _player.iceCubesLeft;
        cupsLeft = _player.cupsLeft;
        currentTemperature = _player.currentTemperature;
        mangoPerServe = _player.mangoPerServe;
        grahamPerServe = _player.grahamPerServe;
        milkPerServe = _player.milkPerServe;
        iceCubesPerServe = _player.iceCubesPerServe;
        playerName = _player.playerName;

    }

}
