using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject playerNameHUD;

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

    public int[] mangoes;
    public int[] graham;
    public int[] milk;
    public int[] iceCubes;
    public int[] cups;

    public void OnBackFromNewCareer()
    {

        int countdown = 1;
        StartCoroutine(AnimateToStart(countdown));

    }

    IEnumerator AnimateToStart(int _countdown)
    {

        FindObjectOfType<GameManager>().OnAnimateFromStartMenu(0);

        while (_countdown > 0)
        {

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        playerNameHUD.GetComponent<TMP_InputField>().text = "";

    }

    public void OnStartFromNewCareer()
    {

        playerName = playerNameHUD.GetComponent<TMP_InputField>().text;
        PlayerModel player = Database.LoadPlayer();

        if (playerName.Equals(""))
        {

            FindObjectOfType<GameManager>().OnAnimateFromNewCareer("requiredPlayerName");

        }
        else if (player != null)
        {

            FindObjectOfType<GameManager>().OnAnimateFromNewCareer("warningOverwrite");

        }
        else
        {

            NewPlayer();
            FindObjectOfType<GameManager>().OnNewCareer();

        }

    }

    public void NewPlayer()
    {

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
