using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

public class InGamePreparationPhase : MonoBehaviour
{

    [SerializeField] private Button smallDecrementUIButton;
    [SerializeField] private Button mediumDecrementUIButton;
    [SerializeField] private Button largeDecrementUIButton;
    [SerializeField] private Button smallIncrementUIButton;
    [SerializeField] private Button mediumIncrementUIButton;
    [SerializeField] private Button largeIncrementUIButton;
    [SerializeField] private Image popularityFillHUD;
    [SerializeField] private Image satisfactionFillHUD;
    [SerializeField] private Image smallSupplyHUD;
    [SerializeField] private Image mediumSupplyHUD;
    [SerializeField] private Image largeSupplyHUD;
    [SerializeField] private Sprite[] resources;
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
    [SerializeField] private Toggle resultsUINavButton;
    [SerializeField] private Toggle mangoUINavButton;
    [SerializeField] private ToggleGroup navigationPanel;
    [SerializeField] private ToggleGroup suppliesNavigationPanel;
    

    private enum NavigationToRightStates { results, upgrades, staff, marketing, recipe, supplies };
    private NavigationToRightStates navigationToRightState;
    // private enum NavigationToLeftStates { results, upgrades, staff, marketing, recipe, supplies };
    // private NavigationToLeftStates navigationToLeftState;
    private enum ResultsStates { yesterdaysResults, charts, profitAndLoss, balanceSheet };
    private ResultsStates resultsState;
    private NavigationToRightStates lastNavigationToRightState;

    private int[,,] SUPPLIES_INT;
    private float[,] SUPPLIES_FLOAT;
    private int suppliesState;
    private float capital;

    void Start()
    {

        SUPPLIES_INT = new int[5, 2, 3]
        {
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } }

        };

        SUPPLIES_FLOAT = new float[5, 3]
        {

            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 }

        };

        // CONSTANTS
        SUPPLIES_INT[0, 1, 0] = 12;
        SUPPLIES_INT[0, 1, 1] = 24;
        SUPPLIES_INT[0, 1, 2] = 48;
        SUPPLIES_INT[1, 1, 0] = 12;
        SUPPLIES_INT[1, 1, 1] = 20;
        SUPPLIES_INT[1, 1, 2] = 50;
        SUPPLIES_INT[2, 1, 0] = 12;
        SUPPLIES_INT[2, 1, 1] = 20;
        SUPPLIES_INT[2, 1, 2] = 50;
        SUPPLIES_INT[3, 1, 0] = 50;
        SUPPLIES_INT[3, 1, 1] = 200;
        SUPPLIES_INT[3, 1, 2] = 500;
        SUPPLIES_INT[4, 1, 0] = 75;
        SUPPLIES_INT[4, 1, 1] = 225;
        SUPPLIES_INT[4, 1, 2] = 400;

        SUPPLIES_FLOAT[0, 0] = 216.00f;
        SUPPLIES_FLOAT[0, 1] = 324.00f;
        SUPPLIES_FLOAT[0, 2] = 432.00f;
        SUPPLIES_FLOAT[1, 0] = 216.00f;
        SUPPLIES_FLOAT[1, 1] = 315.00f;
        SUPPLIES_FLOAT[1, 2] = 675.00f;
        SUPPLIES_FLOAT[2, 0] = 216.00f;
        SUPPLIES_FLOAT[2, 1] = 315.00f;
        SUPPLIES_FLOAT[2, 2] = 675.00f;
        SUPPLIES_FLOAT[3, 0] = 45.00f;
        SUPPLIES_FLOAT[3, 1] = 135.00f;
        SUPPLIES_FLOAT[3, 2] = 225.00f;
        SUPPLIES_FLOAT[4, 0] = 45.00f;
        SUPPLIES_FLOAT[4, 1] = 105.75f;
        SUPPLIES_FLOAT[4, 2] = 168.75f;

        FindObjectOfType<Player>().LoadPlayer();

        capital = FindObjectOfType<Player>().playerCapital;

        mangoUIText.text = HandleResourceMango(FindObjectOfType<Player>().mangoLeft).ToString();
        grahamUIText.text = FindObjectOfType<Player>().grahamLeft.ToString();
        milkUIText.text = FindObjectOfType<Player>().milkLeft.ToString();
        iceCubesUIText.text = FindObjectOfType<Player>().iceCubesLeft.ToString();
        cupsUIText.text = FindObjectOfType<Player>().cupsLeft.ToString();
        temperatureUIText.text = "Temp " + FindObjectOfType<Player>().currentTemperature.ToString() + " °C";
        popularityFillHUD.fillAmount = FindObjectOfType<Player>().currentPopularity;
        satisfactionFillHUD.fillAmount = FindObjectOfType<Player>().currentSatisfaction;

        navigationToRightState = NavigationToRightStates.results;
        // navigationToLeftState = NavigationToLeftStates.results;
        lastNavigationToRightState = NavigationToRightStates.results;

        resultsUINavButton.isOn = true;
        mangoUINavButton.isOn = true;
        suppliesState = 0;

    }

    // Update is called once per frame
    void Update()
    {

        capitalUIText.text = "₱ " + capital.ToString("0.00");

        if (SimpleInput.GetButtonUp("OnNavigation"))
        {

            OnNavigation();

        }

        if (SimpleInput.GetButtonUp("MangoUINavButton"))
        {

            OnSuppliesNavigation(0);

        }

        if (SimpleInput.GetButtonUp("GrahamUINavButton"))
        {

            OnSuppliesNavigation(1);

        }

        if (SimpleInput.GetButtonUp("MilkUINavButton"))
        {

            OnSuppliesNavigation(2);

        }

        if (SimpleInput.GetButtonUp("IceCubesUINavButton"))
        {

            OnSuppliesNavigation(3);

        }

        if (SimpleInput.GetButtonUp("CupsUINavButton"))
        {

            OnSuppliesNavigation(4);

        }

        if (SimpleInput.GetButtonDown("SmallIncrementUIButton"))
        {

            OnIncrement(0);

        }

        if (SimpleInput.GetButtonDown("MediumIncrementUIButton"))
        {

            OnIncrement(1);

        }

        if (SimpleInput.GetButtonDown("LargeIncrementUIButton"))
        {

            OnIncrement(2);

        }

        if (SimpleInput.GetButtonDown("SmallDecrementUIButton"))
        {

            OnDecrement(0);

        }

        if (SimpleInput.GetButtonDown("MediumDecrementUIButton"))
        {

            OnDecrement(1);

        }

        if (SimpleInput.GetButtonDown("LargeDecrementUIButton"))
        {

            OnDecrement(2);

        }

        if (SimpleInput.GetButtonDown("CancelUIButton"))
        {

            OnQuantityClear();
            capital = FindObjectOfType<Player>().playerCapital;

        }    

        if (navigationToRightState == NavigationToRightStates.supplies)
        {     

            smallQuantityUIText.text = SUPPLIES_INT[suppliesState, 0, 0].ToString();
            mediumQuantityUIText.text = SUPPLIES_INT[suppliesState, 0, 1].ToString();
            largeQuantityUIText.text = SUPPLIES_INT[suppliesState, 0, 2].ToString();

            if (SUPPLIES_INT[suppliesState, 0, 0] == 0)
            {

                smallDecrementUIButton.interactable = false;  

            }
            else
            {

                smallDecrementUIButton.interactable = true;

            }

            if (SUPPLIES_INT[suppliesState, 0, 1] == 0)
            {

                mediumDecrementUIButton.interactable = false;

            }
            else 
            {

                mediumDecrementUIButton.interactable = true;

            }

            if (SUPPLIES_INT[suppliesState, 0, 2] == 0)
            {

                largeDecrementUIButton.interactable = false;

            }
            else
            {

                largeDecrementUIButton.interactable = true;

            }

            if (capital - SUPPLIES_FLOAT[suppliesState, 0] >= 0)
            {
                
                smallIncrementUIButton.interactable = true;

            }
            else
            {

                smallIncrementUIButton.interactable = false;

            }

            if (capital - SUPPLIES_FLOAT[suppliesState, 1] >= 0)
            {

                mediumIncrementUIButton.interactable = true;

            }
            else
            {

                mediumIncrementUIButton.interactable = false;

            }

            if (capital - SUPPLIES_FLOAT[suppliesState, 2] >= 0)
            {

                largeIncrementUIButton.interactable = true;

            }
            else
            {

                largeIncrementUIButton.interactable = false;

            }

        }

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
        // navigationToLeftState = GetNavigationToLeft(navigation);

        // if (lastNavigationToRightState < navigationToRightState)
        if (lastNavigationToRightState != navigationToRightState)
        {

            FindObjectOfType<GameManager>().GetAnimator.SetInteger("navigationToRightState", (int) navigationToRightState);
            lastNavigationToRightState = navigationToRightState;
            mangoUINavButton.isOn = true;
            OnQuantityClear();
            OnSuppliesNavigation(0);
            capital = FindObjectOfType<Player>().playerCapital;

        }
        /*else if (lastNavigationToRightState > navigationToRightState)
        {

            FindObjectOfType<GameManager>().GetAnimator.SetInteger("navigationToLeftState", (int) navigationToLeftState);
            lastNavigationToRightState = navigationToRightState;
            OnNavigateToSupplies();

        }*/

    }

    private NavigationToRightStates GetNavigationToRight(string _navigation)
    {

        if (_navigation.Equals("UpgradesUINavButton"))
        {

            bottomNavigationStateUIText.text = "Upgrades";
            return NavigationToRightStates.upgrades;

        }
        else if (_navigation.Equals("StaffUINavButton"))
        {

            bottomNavigationStateUIText.text = "Staff";
            return NavigationToRightStates.staff;

        }
        else if (_navigation.Equals("MarketingUINavButton"))
        {

            bottomNavigationStateUIText.text = "Marketing";
            return NavigationToRightStates.marketing;

        }
        else if (_navigation.Equals("RecipeUINavButton"))
        {

            bottomNavigationStateUIText.text = "Recipe";
            return NavigationToRightStates.recipe;

        }
        else if (_navigation.Equals("SuppliesUINavButton"))
        {

            bottomNavigationStateUIText.text = "Supplies";
            return NavigationToRightStates.supplies;

        }
        else
        {

            bottomNavigationStateUIText.text = "";
            return NavigationToRightStates.results;

        }

    }
    
    /*private NavigationToLeftStates GetNavigationToLeft(string _navigation)
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

    }*/

    public string GetNavigation(ToggleGroup _toggleGroup)
    {

        Toggle navigation = _toggleGroup.ActiveToggles().FirstOrDefault();
        return navigation.name.ToString();

    }

    public void OnSuppliesNavigation(int _suppliesNavigationState)
    {

        suppliesState = _suppliesNavigationState;

        smallSupplyHUD.sprite = resources[_suppliesNavigationState];
        mediumSupplyHUD.sprite = resources[_suppliesNavigationState];
        largeSupplyHUD.sprite = resources[_suppliesNavigationState];

        smallPriceUIText.text = string.Format("{0} {1} {2}", SUPPLIES_INT[_suppliesNavigationState, 1, 0].ToString(), GetConjuctions(_suppliesNavigationState), SUPPLIES_FLOAT[_suppliesNavigationState, 0].ToString("0.00"));
        mediumPriceUIText.text = string.Format("{0} {1} {2}", SUPPLIES_INT[_suppliesNavigationState, 1, 1].ToString(), GetConjuctions(_suppliesNavigationState), SUPPLIES_FLOAT[_suppliesNavigationState, 1].ToString("0.00"));
        largePriceUIText.text = string.Format("{0} {1} {2}", SUPPLIES_INT[_suppliesNavigationState, 1, 2].ToString(), GetConjuctions(_suppliesNavigationState), SUPPLIES_FLOAT[_suppliesNavigationState, 2].ToString("0.00"));

    }

    private int GetSuppliesNavigation(string _navigation) => _navigation switch
    {

        "GrahamUINavButton" => 1,

        "MilkUINavButton" => 2,

        "IceCubesUINavButton" => 3,

        "CupsUINavButton" => 4,

        _ => 0,

    };

    public void OnDecrement(int _scale)
    {

        int quantityPerPrice = SUPPLIES_INT[suppliesState, 1, _scale];
        float price = SUPPLIES_FLOAT[suppliesState, _scale];

        if (SUPPLIES_INT[suppliesState, 0, _scale] - quantityPerPrice >= 0)
        {

            SUPPLIES_INT[suppliesState, 0, _scale] -= quantityPerPrice;
            capital += price;

        }

    }

    public void OnIncrement(int _scale)
    {

        int quantityPerPrice = SUPPLIES_INT[suppliesState, 1, _scale];
        float price = SUPPLIES_FLOAT[suppliesState, _scale];

        if (capital - price >= 0)
        {

            SUPPLIES_INT[suppliesState, 0, _scale] += quantityPerPrice;
            capital -= price;

        }

    }

    private void OnQuantityClear()
    {

        SUPPLIES_INT[0, 0, 0] = 0;
        SUPPLIES_INT[0, 0, 1] = 0;
        SUPPLIES_INT[0, 0, 2] = 0;
        SUPPLIES_INT[1, 0, 0] = 0;
        SUPPLIES_INT[1, 0, 1] = 0;
        SUPPLIES_INT[1, 0, 2] = 0;
        SUPPLIES_INT[2, 0, 0] = 0;
        SUPPLIES_INT[2, 0, 1] = 0;
        SUPPLIES_INT[2, 0, 2] = 0;
        SUPPLIES_INT[3, 0, 0] = 0;
        SUPPLIES_INT[3, 0, 1] = 0;
        SUPPLIES_INT[3, 0, 2] = 0;
        SUPPLIES_INT[4, 0, 0] = 0;
        SUPPLIES_INT[4, 0, 1] = 0;
        SUPPLIES_INT[4, 0, 2] = 0;

    }

    public string GetConjuctions(int _supply)
    {

        return _supply switch
        {

            0 => "mangoes = ₱",

            1 => "cups = ₱",

            2 => "cups = ₱",

            3 => "cubes = ₱",

            _ => "cups = ₱",

        };

    }

}
