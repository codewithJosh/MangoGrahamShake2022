using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class InGamePreparationPhase : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI mangoUIText;
    [SerializeField] private TextMeshProUGUI grahamUIText;
    [SerializeField] private TextMeshProUGUI milkUIText;
    [SerializeField] private TextMeshProUGUI iceCubesUIText;
    [SerializeField] private TextMeshProUGUI cupsUIText;
    [SerializeField] private TextMeshProUGUI dateUIText;
    [SerializeField] private TextMeshProUGUI temperatureUIText;
    [SerializeField] private TextMeshProUGUI capitalUIText;
    [SerializeField] private Image popularityFillHUD;
    [SerializeField] private Image satisfactionFillHUD;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Player>().LoadPlayer();

        capitalUIText.text = "Php " + FindObjectOfType<Player>().playerCapital.ToString("0.00");
        mangoUIText.text = handleResourceMango(FindObjectOfType<Player>().resourceMango).ToString();
        grahamUIText.text = FindObjectOfType<Player>().resourceGraham.ToString();
        milkUIText.text = FindObjectOfType<Player>().resourceMilk.ToString();
        iceCubesUIText.text = FindObjectOfType<Player>().resourceIceCubes.ToString();
        cupsUIText.text = FindObjectOfType<Player>().resourceCups.ToString();
        temperatureUIText.text = "Temp " + FindObjectOfType<Player>().currentTemperature.ToString() + " °C";
        popularityFillHUD.fillAmount = FindObjectOfType<Player>().currentPopularity;
        satisfactionFillHUD.fillAmount = FindObjectOfType<Player>().currentSatisfaction;

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
