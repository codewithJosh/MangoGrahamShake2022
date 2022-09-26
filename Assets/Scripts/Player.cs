using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public GameObject inputField;

    public string playerName = "";
    public float playerCapital = 2000;

    public Dictionary<DateTime, int> resourceMango = new();
    public int resourceGraham = 0;
    public int resourceMilk = 0;
    public int resourceIceCubes = 0;
    public int resourceCups = 0;

    public int currentTemperature = 0;
    public float currentPopularity = 0.1f;
    public float currentSatisfaction = 1f;

    public void NewPlayer()
    {

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

        resourceMango = player.resourceMango;
        resourceGraham = player.resourceGraham;
        resourceMilk = player.resourceMilk;
        resourceIceCubes = player.resourceIceCubes;
        resourceCups = player.resourceCups;

        currentTemperature = player.currentTemperature;
        currentPopularity = player.currentPopularity;
        currentSatisfaction = player.currentSatisfaction;

    }

    public void OnStorePlayerName()
    {

        playerName = inputField.GetComponent<InputField>().text;

        if (playerName.Equals(""))
        {

            FindObjectOfType<GameManager>().animator.SetTrigger("Required");

        }
        else
        {

            FindObjectOfType<GameManager>().OnStartNewCareer();

        }

    }

}
