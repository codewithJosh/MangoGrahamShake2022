using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InGamePreparationPhase : MonoBehaviour
{

    public Text playerCapital;
    public Text resourceMango;
    public Text resourceGraham;
    public Text resourceMilk;
    public Text resourceIceCubes;
    public Text resourceCups;
    public Text currentTemperature;
    public Image fillCurrentPopularity;
    public Image fillCurrentSatisfaction;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Player>().LoadPlayer();

        playerCapital.text = "Php " + FindObjectOfType<Player>().playerCapital.ToString("0.00");
        resourceMango.text = handleResourceMango(FindObjectOfType<Player>().resourceMango).ToString();
        resourceGraham.text = FindObjectOfType<Player>().resourceGraham.ToString();
        resourceMilk.text = FindObjectOfType<Player>().resourceMilk.ToString();
        resourceIceCubes.text = FindObjectOfType<Player>().resourceIceCubes.ToString();
        resourceCups.text = FindObjectOfType<Player>().resourceCups.ToString();
        currentTemperature.text = "Temp " + FindObjectOfType<Player>().currentTemperature.ToString() + " °C";
        fillCurrentPopularity.fillAmount = FindObjectOfType<Player>().currentPopularity;
        fillCurrentSatisfaction.fillAmount = FindObjectOfType<Player>().currentSatisfaction;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int handleResourceMango(Dictionary<DateTime, int> _resourceMango)
    {

        int mangoes = 0;

        foreach(var x in _resourceMango)
        {

            mangoes += x.Value;

        }

        return mangoes;

    }

}
