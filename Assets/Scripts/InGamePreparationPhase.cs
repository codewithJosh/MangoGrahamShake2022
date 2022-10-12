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
    [SerializeField] private TextMeshProUGUI smallPriceUIText;
    [SerializeField] private TextMeshProUGUI smallQuantityUIText;
    [SerializeField] private TextMeshProUGUI mediumPriceUIText;
    [SerializeField] private TextMeshProUGUI mediumQuantityUIText;
    [SerializeField] private TextMeshProUGUI largePriceUIText;
    [SerializeField] private TextMeshProUGUI largeQuantityUIText;
    [SerializeField] private Image popularityFillHUD;
    [SerializeField] private Image satisfactionFillHUD;
    [SerializeField] private Image smallSupplyHUD;
    [SerializeField] private Image mediumSupplyHUD;
    [SerializeField] private Image largeSupplyHUD;
    [SerializeField] private ToggleGroup navigationPanel;
    [SerializeField] private ToggleGroup suppliesNavigationPanel;
    [SerializeField] private Sprite[] resources;

    private enum NavigationToRightStates { results, upgrades, staff, marketing, recipe, supplies };
    private NavigationToRightStates navigationToRightState;
    private enum NavigationToLeftStates { results, upgrades, staff, marketing, recipe, supplies };
    private NavigationToLeftStates navigationToLeftState;
    private enum ResultsStates { yesterdaysResults, charts, profitAndLoss, balanceSheet };
    private ResultsStates resultsState;
    private NavigationToRightStates lastNavigationToRightState;

    private int[,,] suppliesInt;
    private double[,] suppliesDouble;
    private int suppliesState;

    void Start()
    {

        FindObjectOfType<Player>().LoadPlayer();

        capitalUIText.text = "₱ " + FindObjectOfType<Player>().playerCapital.ToString("0.00");
        mangoUIText.text = HandleResourceMango(FindObjectOfType<Player>().mangoLeft).ToString();
        grahamUIText.text = FindObjectOfType<Player>().grahamLeft.ToString();
        milkUIText.text = FindObjectOfType<Player>().milkLeft.ToString();
        iceCubesUIText.text = FindObjectOfType<Player>().iceCubesLeft.ToString();
        cupsUIText.text = FindObjectOfType<Player>().cupsLeft.ToString();
        temperatureUIText.text = "Temp " + FindObjectOfType<Player>().currentTemperature.ToString() + " °C";
        popularityFillHUD.fillAmount = FindObjectOfType<Player>().currentPopularity;
        satisfactionFillHUD.fillAmount = FindObjectOfType<Player>().currentSatisfaction;

        navigationToRightState = NavigationToRightStates.results;
        navigationToLeftState = NavigationToLeftStates.results;
        lastNavigationToRightState = NavigationToRightStates.results;

        OnQuantityClear();

        //suppliesState, section, unit
        suppliesInt[0, 1, 0] = 12;
        suppliesInt[0, 1, 1] = 24;
        suppliesInt[0, 1, 2] = 48;
        suppliesInt[1, 1, 0] = 12;
        suppliesInt[1, 1, 1] = 20;
        suppliesInt[1, 1, 2] = 50;
        suppliesInt[2, 1, 0] = 12;
        suppliesInt[2, 1, 1] = 20;
        suppliesInt[2, 1, 2] = 50;
        suppliesInt[3, 1, 0] = 50;
        suppliesInt[3, 1, 1] = 200;
        suppliesInt[3, 1, 2] = 500;
        suppliesInt[4, 1, 0] = 75;
        suppliesInt[4, 1, 1] = 225;
        suppliesInt[4, 1, 2] = 400;

        suppliesDouble[0, 0] = 216.00;
        suppliesDouble[0, 1] = 324.00;
        suppliesDouble[0, 2] = 432.00;
        suppliesDouble[1, 0] = 216.00;
        suppliesDouble[1, 1] = 315.00;
        suppliesDouble[1, 2] = 675.00;
        suppliesDouble[2, 0] = 216.00;
        suppliesDouble[2, 1] = 315.00;
        suppliesDouble[2, 2] = 675.00;
        suppliesDouble[3, 0] = 45.00;
        suppliesDouble[3, 1] = 135.00;
        suppliesDouble[3, 2] = 225.00;
        suppliesDouble[4, 0] = 45.00;
        suppliesDouble[4, 1] = 105.75;
        suppliesDouble[4, 2] = 168.75;

        suppliesState = 0;

        OnSuppliesNavigation(0);

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
            OnQuantityClear();

        }
        else if (lastNavigationToRightState > navigationToRightState)
        {

            FindObjectOfType<GameManager>().GetAnimator.SetInteger("navigationToLeftState", (int) navigationToLeftState);
            lastNavigationToRightState = navigationToRightState;
            OnQuantityClear();

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

    public void OnSuppliesNavigation(int _supply)
    {

        suppliesState = _supply;

        smallSupplyHUD.sprite = resources[_supply];
        mediumSupplyHUD.sprite = resources[_supply];
        largeSupplyHUD.sprite = resources[_supply];

        smallPriceUIText.text = String.Format("{0} {1} {2}", suppliesInt[_supply, 1, 0].ToString(), GetConjuctions(_supply), suppliesDouble[_supply, 0].ToString("0.00"));
        mediumPriceUIText.text = String.Format("{0} {1} {2}", suppliesInt[_supply, 1, 1].ToString(), GetConjuctions(_supply), suppliesDouble[_supply, 1].ToString("0.00"));
        largePriceUIText.text = String.Format("{0} {1} {2}", suppliesInt[_supply, 1, 2].ToString(), GetConjuctions(_supply), suppliesDouble[_supply, 2].ToString("0.00"));

        UpdateUIText();

    }

    public void OnDecrement(int _scale)
    {

        int counter = suppliesInt[suppliesState, 0, _scale];
        int quantity = suppliesInt[suppliesState, 1, _scale];

        if (counter - quantity >= 0)
        {

            suppliesInt[suppliesState, 0, _scale] -= quantity;
            UpdateUIText();

        }

    }

    private void UpdateUIText()
    {

        smallQuantityUIText.text = suppliesInt[suppliesState, 0, 0].ToString();
        mediumQuantityUIText.text = suppliesInt[suppliesState, 0, 1].ToString();
        largeQuantityUIText.text = suppliesInt[suppliesState, 0, 2].ToString();

    }

    public void OnIncrement(int _scale)
    {

        int quantity = suppliesInt[suppliesState, 1, _scale];

        suppliesInt[suppliesState, 0, _scale] += quantity;
        UpdateUIText();
    }

    private void OnQuantityClear()
    {

        suppliesInt = new int[5, 2, 3] { { { 0, 0, 0 }, { 0, 0, 0 } }, { { 0, 0, 0 }, { 0, 0, 0 } }, { { 0, 0, 0 }, { 0, 0, 0 } }, { { 0, 0, 0 }, { 0, 0, 0 } }, { { 0, 0, 0 }, { 0, 0, 0 } } };
        suppliesDouble = new double[5, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

    }

    public string GetConjuctions(int _supply)
    {

        switch (_supply)
        {

            case 0: return "mangoes = ₱";
            case 1: return "cups = ₱";
            case 2: return "cups = ₱";
            case 3: return "cubes = ₱";

        }
        return "cups = ₱";

    }

}
