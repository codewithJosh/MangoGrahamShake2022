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

    public Dictionary<DateTime, int> resourceMango;
    public int resourceGraham;
    public int resourceMilk;
    public int resourceIceCubes;
    public int resourceCups;

    public int currentTemperature;
    public float currentPopularity;
    public float currentSatisfaction;

    public void OnBackFromNewCareer()
    {

        int countdown = 1;
        StartCoroutine(AnimateToStart(countdown));

    }

    IEnumerator AnimateToStart(int _countdown)
    {

        FindObjectOfType<GameManager>().OnAnimate(0);

        while (_countdown > 0)
        {

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        playerNameHUD.GetComponent<TMP_InputField>().text = "";

    }

    public void NewPlayer()
    {

        resourceMango = new();
        playerCapital = 2000.00f;
        resourceGraham = 0;
        resourceMilk = 0;
        resourceIceCubes = 0;
        resourceCups = 0;
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

        /*playerName = inputField.GetComponent<InputField>().text;

        if (playerName.Equals(""))
        {

            //FindObjectOfType<GameManager>().animator.SetTrigger("RequiredPlayerName");

        }
        else
        {

            FindObjectOfType<GameManager>().OnStartNewCareer();

        }*/

    }

}
