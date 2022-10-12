using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

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
    [SerializeField] private TextMeshProUGUI bottomNavigationStateUIText;
    [SerializeField] private Image popularityFillHUD;
    [SerializeField] private Image satisfactionFillHUD;
    [SerializeField] private ToggleGroup navigationPanel;
    [SerializeField] private ToggleGroup suppliesNavigationPanel;

    private enum NavigationToRightStates { results, upgrades, staff, marketing, recipe, supplies };
    private NavigationToRightStates navigationToRightState = NavigationToRightStates.results;

    private enum NavigationToLeftStates { results, upgrades, staff, marketing, recipe, supplies };
    private NavigationToLeftStates navigationToLeftState = NavigationToLeftStates.results;

    private enum ResultsStates { yesterdaysResults, charts, profitAndLoss, balanceSheet };
    private ResultsStates resultsState = ResultsStates.yesterdaysResults;

    private enum SuppliesNavigationStates { mango, graham, milk, iceCubes, cups };
    private SuppliesNavigationStates suppliesNavigationState = SuppliesNavigationStates.mango;

    private NavigationToRightStates lastNavigationToRightState = NavigationToRightStates.results;

    void Start()
    {
        FindObjectOfType<Player>().LoadPlayer();

        capitalUIText.text = "Php " + FindObjectOfType<Player>().playerCapital.ToString("0.00");
        mangoUIText.text = HandleResourceMango(FindObjectOfType<Player>().resourceMango).ToString();
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

    private int HandleResourceMango(Dictionary<DateTime, int> _resourceMango)
    {

        int mangoes = 0;

        foreach(var x in _resourceMango)
        {

            mangoes += x.Value;

        }

        return mangoes;

    }

    public void OnNavigation()
    {

        string navigation = GetNavigation(navigationPanel);
        navigationToRightState = GetNavigationToRight(navigation);
        navigationToLeftState = GetNavigationToLeft(navigation);
        SetBottomNavigationState(GetNavigation(navigation));

        if (lastNavigationToRightState < navigationToRightState)
        {

            FindObjectOfType<GameManager>().GetAnimator.SetInteger("navigationToRightState", (int) navigationToRightState);
            lastNavigationToRightState = navigationToRightState;

        }
        else if (lastNavigationToRightState > navigationToRightState)
        {

            FindObjectOfType<GameManager>().GetAnimator.SetInteger("navigationToLeftState", (int) navigationToLeftState);
            lastNavigationToRightState = navigationToRightState;

        }

    }

    private NavigationToRightStates GetNavigationToRight(string _navigation)
    {

        return _navigation switch
        {

            "UpgradesUINavButton" => NavigationToRightStates.upgrades,

            "StaffUINavButton" => NavigationToRightStates.staff,

            "MarketingUINavButton" => NavigationToRightStates.marketing,

            "RecipeUINavButton" => NavigationToRightStates.recipe,

            "SuppliesUINavButton" => NavigationToRightStates.supplies,

            _ => NavigationToRightStates.results,

        };

    }
    
    private NavigationToLeftStates GetNavigationToLeft(string _navigation)
    {

        return _navigation switch
        {

            "UpgradesUINavButton" => NavigationToLeftStates.upgrades,

            "StaffUINavButton" => NavigationToLeftStates.staff,

            "MarketingUINavButton" => NavigationToLeftStates.marketing,

            "RecipeUINavButton" => NavigationToLeftStates.recipe,

            "SuppliesUINavButton" => NavigationToLeftStates.supplies,

            _ => NavigationToLeftStates.results,

        };

    }
    
    private string GetNavigation(string _navigation)
    {

        return _navigation switch
        {

            "UpgradesUINavButton" => "Upgrades",

            "StaffUINavButton" => "Staff",

            "MarketingUINavButton" => "Marketing",

            "RecipeUINavButton" => "Recipe",

            "SuppliesUINavButton" => "Supplies",

            _ => "",

        };

    }

    public string GetNavigation(ToggleGroup _toggleGroup)
    {

        Toggle navigation = _toggleGroup.ActiveToggles().FirstOrDefault();
        return navigation.name.ToString();

    }

    public void SetBottomNavigationState(string _bottomNavigationState)
    {

        bottomNavigationStateUIText.text = _bottomNavigationState;

    }

    public void OnSuppliesNavigation()
    {

        string navigation = GetNavigation(suppliesNavigationPanel);
        suppliesNavigationState = GetSuppliesNavigation(navigation);
        FindObjectOfType<GameManager>().GetAnimator.SetInteger("suppliesNavigationState", (int)suppliesNavigationState);

    }

    private SuppliesNavigationStates GetSuppliesNavigation(string _navigation)
    {

        return _navigation switch
        {

            "GrahamUINavButton" => SuppliesNavigationStates.graham,

            "MilkUINavButton" => SuppliesNavigationStates.milk,

            "IceCubesUINavButton" => SuppliesNavigationStates.iceCubes,

            "CupsUINavButton" => SuppliesNavigationStates.cups,

            _ => SuppliesNavigationStates.mango,

        };

    }

}
